using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using ImageStudio;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows.Markup;
using System.Net.Http;

namespace MainFrom
{
    public class TcpCommunication : SignalCommunication
    {
        private static readonly Lazy<TcpCommunication> _Lazy = new Lazy<TcpCommunication>(() => new TcpCommunication(new TcpClient()));
        ILogger<TcpCommunication> _logger;
        private NetworkStream StreamToServer { get; set; }

        public event CommunicationEventHandler Sended;

        public event CommunicationEventHandler Received;

        #region 单例
        public static TcpCommunication Instance => _Lazy.Value;


        private static object locker = new object();
        private TcpCommunication(TcpClient tcpClient) : base(tcpClient)
        {
            var logger = App.Current.Services.GetService<ILogger<TcpCommunication>>();
            _logger = logger;
            ReceiveQueue = new ConcurrentQueue<byte[]>();
        }

        #endregion

        private Thread ReceiveThread;
        public ConcurrentQueue<byte[]> ReceiveQueue { get; }

        private ushort ReadAllRegister_ReadNumEach => 120;

        private ushort ReadAllRegister_ReadStartAddress { get; set; } = 0;
        private Regex RegWriteInt16 => new Regex(@"^vw\d*[02468]=\d+$", RegexOptions.IgnoreCase);
        private Regex RegWriteInt32 => new Regex(@"^vd\d*[02468]=\d+$", RegexOptions.IgnoreCase);
        private Regex RegWriteFloat => new Regex(@"^vd\d*[02468]=\d+\.\d+$", RegexOptions.IgnoreCase);
        private Regex RegWriteBit =>new Regex(@"^v\d+\.[0-7]=[01]$", RegexOptions.IgnoreCase);
        private Regex RegNum => new Regex(@"\d+(\.\d+)?");

        public override bool Connect(string host, int port)
        {
            _logger.LogInformation($"连接到：{host}:{port}");
            if (!IsConnected)
            {
                TcpClient.Connect(host, port);
                StreamToServer = TcpClient.GetStream();
                ReceiveThread.Start();
            }
            return IsConnected;
        }
        public override void Disconnect()
        {
            IsClosing = true;
            if (IsConnected)
            {
                StreamToServer.Close();
                StreamToServer.Dispose();
                TcpClient.Close();
            }

            if (ReceiveThread.ThreadState != ThreadState.Unstarted)
            {
                ReceiveThread.Abort();
            }

        }
        public override void Init(string host, int port)
        {
            IsClosing = false;
            IsCanRunning = true;
            ReceiveThread = new Thread(ReceiveMethod);
            Connect(host, port);
        }

        private void SendMethod(byte[] data)
        {
            byte[] ReadAllBytes = ReadAllRegisteCommand();
            CommunicationEventArgs e = new CommunicationEventArgs { Time = DateTime.Now, Data = data };
            _logger.LogInformation($"发送数据：Length={e.Data.Length}\r\n{e}:");
            StreamToServer.Write(data, 0, data.Length);
            StreamToServer.Write(ReadAllBytes, 0, ReadAllBytes.Length);
            Sended.Invoke(e);
        }
        private void ReceiveMethod()
        {
            while (IsConnected)
            {
                int available = TcpClient.Available;
                if (available > 0)
                {
                    byte[] buffer = new byte[available];
                    StreamToServer.Read(buffer, 0, available);
                    CommunicationEventArgs e = new CommunicationEventArgs { Time = DateTime.Now, Data = buffer };
                    _logger.LogInformation($"收到数据：Length={e.Data.Length} \r\n {e}");
                    AnalysisData(e);
                }
            }
        }

        private void AnalysisData(CommunicationEventArgs args)
        {
            Received.Invoke(args);
            if (args.Data[7] == 3)
            {
                int index = (args.Data[1] - 1) * 120;
                ushort[] readbuffer = ConvertBytes(args.Data);
                _logger.LogInformation($"解析数据 Length：{readbuffer}");
            }
        }

        public void WriteRegister<T>(ushort registerAddress, T value) where T : struct
        {
            _logger.LogInformation($"寄存器地址:{registerAddress} \t值:{value}");
            byte[] bytes = GetWriteRegisterCommand(registerAddress, value);

            NetworkStream stream = TcpClient.GetStream();
            stream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 获取写寄存器命令
        /// </summary>
        /// <typeparam name="T">要写入的数据类型</typeparam>
        /// <param name="registerAddress">开始的寄存器地址</param>
        /// <param name="value">要写入的数据</param>
        /// <returns></returns>
        public byte[] GetWriteRegisterCommand<T>(ushort registerAddress, T value) where T : struct
        {
            byte[] returnBytes = null;

            byte[] reisterAddressBytes = BitConverter.GetBytes(registerAddress).Reverse();
            byte[] valueBytes = StructToBytes(value).Reverse();
            switch (valueBytes.Length)
            {
                case 2:
                    returnBytes = new byte[12] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x00, 0x00, 0x00, 0x00 };
                    reisterAddressBytes.CopyTo(returnBytes, 8);
                    valueBytes.CopyTo(returnBytes, 10);
                    break;
                case 4:
                    returnBytes = new byte[17] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x0B, 0x01, 0x10, 0x00, 0x00, 0x00, 0x02, 0x04, 0x00, 0x00, 0x00, 0x00 };
                    reisterAddressBytes.CopyTo(returnBytes, 8);
                    valueBytes.CopyTo(returnBytes, 13);
                    break;
                default:
                    break;
            }
            return returnBytes;
        }

        public List<byte[]> ReadAllRegisteCommandList()
        {
            List<byte[]> list = new List<byte[]>();
            for (int i = 0; i <= (ModbusRegs.Count - 1) / ReadAllRegister_ReadNumEach; i++)
            {
                byte[] cmd = ReadAllRegisteCommand();
                list.Add(cmd);
            }
            return list;
        }

        private byte[] ReadAllRegisteCommand()
        {
            byte[] allBytes = { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00 };

            //当前组号
            byte GroupNo = (byte)(ReadAllRegister_ReadStartAddress / ReadAllRegister_ReadNumEach + 1);
            allBytes[1] = GroupNo;

            //读取的起始地址
            byte[] ReadStartAddress = BitConverter.GetBytes(ReadAllRegister_ReadStartAddress).Reverse();
            ReadStartAddress.CopyTo(allBytes, 8);

            //读取的数量
            ushort ReadNum;
            byte[] ReadNumBytes;
            if (ReadAllRegister_ReadStartAddress + ReadAllRegister_ReadNumEach > ModbusRegs.Count)
            {
                ReadNum = (ushort)(ModbusRegs.Count - ReadAllRegister_ReadStartAddress);
                ReadAllRegister_ReadStartAddress = 0;
            }
            else
            {
                ReadNum = ReadAllRegister_ReadNumEach;
                ReadAllRegister_ReadStartAddress += ReadAllRegister_ReadNumEach;
            }
            ReadNumBytes = BitConverter.GetBytes(ReadNum).Reverse();
            ReadNumBytes.CopyTo(allBytes, 10);

            return allBytes;
        }

        public static byte[] StructToBytes<T>(T structObj) where T : struct
        {
            int bytesNum = Marshal.SizeOf(structObj.GetType()); //得到结构体大小
            IntPtr ipObject = Marshal.AllocHGlobal(bytesNum);     //开辟内存空间
            Marshal.StructureToPtr(structObj, ipObject, false);   //填充到指针空间

            byte[] objectBytes = new byte[bytesNum];
            Marshal.Copy(ipObject, objectBytes, 0, bytesNum);     //复制到字节数组
            Marshal.FreeHGlobal(ipObject);                        //释放指针内存

            return objectBytes;
        }

        public bool CheckCommand(string s)
        {
            return RegWriteInt16.IsMatch(s) || RegWriteInt32.IsMatch(s) || RegWriteFloat.IsMatch(s) || RegWriteBit.IsMatch(s);
        }
        public byte[] GetCommand(string cmd)
        {
            if (RegWriteInt16.IsMatch(cmd))
            {
                MatchCollection matches = RegNum.Matches(cmd);
                ushort registeraddress = (ushort)(Convert.ToUInt16(matches[0].Value) >> 1);
                ushort value = Convert.ToUInt16(matches[1].Value);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else if (RegWriteInt32.IsMatch(cmd))
            {
                MatchCollection matches = RegNum.Matches(cmd);
                ushort registeraddress = (ushort)(Convert.ToUInt16(matches[0].Value) >> 1);
                uint value = Convert.ToUInt32(matches[1].Value);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else if (RegWriteFloat.IsMatch(cmd))
            {
                MatchCollection matches = RegNum.Matches(cmd);
                ushort registeraddress = (ushort)(Convert.ToUInt16(matches[0].Value) >> 1);
                float value = Convert.ToSingle(matches[1].Value);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else if (RegWriteBit.IsMatch(cmd))
            {
                MatchCollection matches = RegNum.Matches(cmd);
                string[] a = matches[0].Value.Split('.');
                ushort registeraddress = Convert.ToUInt16(cmd[0]);
                int bit = Convert.ToInt32(cmd[1]);
                if ((registeraddress & 1) == 0)
                    bit += 8;
                registeraddress >>= 1;

                ushort value = 2;
                if (matches[1].Value == "1")
                    value |= (ushort)(1 << bit);
                else
                    value &= (ushort)(1 << bit);
                return GetWriteRegisterCommand(registeraddress, value);
            }
            else
            {
                return new byte[0];
            }
        }

        public void TryReconnect()
        {

        }

        private ushort[] ConvertBytes(byte[] bytes)
        {
            if (bytes.Length == 0)
            {
                return new ushort[0];
            }
            int len = bytes[8] / 2;
            ushort[] result = new ushort[len];
            for (int i = 9, j = 0; i < bytes.Length; i = i + 2, j++)
            {
                result[j] = (ushort)(bytes[i] * (1 << 8) + bytes[i + 1]);
            }
            return result;
        }
    }

    #region 事件定义
    public delegate void CommunicationEventHandler(CommunicationEventArgs e);

    public class CommunicationEventArgs : EventArgs
    {
        public DateTime Time { get; set; }
        public byte[] Data { get; set; }
        public override string ToString()
        {
            string txt = string.Empty;
            foreach (var d in Data)
            {
                txt += d.ToString();
            }
            return txt;
        }
    }
    #endregion
}
