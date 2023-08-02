using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using ImageStudio.Services;
using ImageStudio;
using MainFrom;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ImageStudio
{
    public class MahViewModel : NotifyPropertyChanged
    {
        IFilesService FilesService { get; }

        TcpCommunication TcpClient => TcpCommunication.Instance;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private PlcOptions _plcOptions;

        public ObservableCollection<GridRowData> GridData { get; }

        public ICommand ConnectCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand ReadAllRegisterCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand DebugCommand { get; }

        /// <summary>
        /// 复位设备
        /// </summary>
        public ICommand ResetCommand { get; }

        /// <summary>
        /// 停止设备
        /// </summary>
        public ICommand StopCommand { get; }


        public string ConnectText
        {
            get
            {
                return TcpClient.IsConnected ? "已连接到PLC" : "已断开连接PLC";
            }
            private set
            {
                RaisePropertyChanged(() => ConnectText);
            }
        }

        private void TcpDebug()
        {
            _logger.LogInformation($"SendQueue Count={TcpClient.IsConnected}\t ReceiveQueue Count={TcpClient.ReceiveQueue.Count}");
        }

        private int _UpDownValue;
        public int UpDownValue
        {
            get
            {
                return _UpDownValue;
            }
            private set
            {
                _UpDownValue = value;
                RaisePropertyChanged(() => UpDownValue);
            }
        }

        public MahViewModel(IFilesService filesService, IConfiguration configuration, ILogger<MahViewModel> logger, IOptions<PlcOptions> plcOptions)
        {
            FilesService = filesService;
            _configuration = configuration;
            _logger = logger;
            _plcOptions = plcOptions.Value;

            TcpClient.Sended += TcpClient_Sended;
            TcpClient.Received += TcpClient_Received;

            // var connectionString = _configuration.GetConnectionString("SqlDb");  //从配置文件中读取oeeDb connectionString 
            _logger.LogInformation("MahViewModel");

            ConnectCommand = new AnotherCommandImplementation(_ => TcpCommunication.Instance.Init("192.168.1.28", 503));
            ReadAllRegisterCommand = new AnotherCommandImplementation(_ =>
            {
                _logger.LogInformation("ReadAllRegisteCommandList");
                TcpClient.ReadAllRegisteCommandList();
            });
            DebugCommand = new AnotherCommandImplementation(_ => TcpDebug());

            ResetCommand = new AnotherCommandImplementation(_ =>
            {
                Task.Run(async () =>
                {
                    _logger.LogInformation("复位设备");
                    TcpClient.WriteRegister<ushort>(ModbusRegs.ResetPLC, 1);
                    await Task.Delay(50);
                    TcpClient.WriteRegister<ushort>(ModbusRegs.ResetPLC, 0);

                });
            });

            StopCommand = new AnotherCommandImplementation(_ =>
            {
                TcpClient.WriteRegister<ushort>(ModbusRegs.PLCStart_stop, 0);
                _logger.LogInformation("停止设备");
            });

            GridData = new ObservableCollection<GridRowData> {
                new GridRowData {
                    IsChecked = false,
                    Text =FilesService.Content,
                    EnumValue = EnumValues.ValueA,
                    IntValue = 4879
                },
                new GridRowData {
                    IsChecked = false,
                    Text = "Venus",
                    EnumValue = EnumValues.ValueB,
                    IntValue = 12104
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Earth",
                    EnumValue = EnumValues.ValueC,
                    IntValue = 12742
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Merkur",
                    EnumValue = EnumValues.ValueA,
                    IntValue = 6779
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Jupiter",
                    EnumValue = EnumValues.ValueD,
                    IntValue = 139822
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Saturn",
                    EnumValue = EnumValues.ValueC,
                    IntValue = 116464
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Uranus",
                    EnumValue = EnumValues.ValueC,
                    IntValue = 50724
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Neptun",
                    EnumValue = EnumValues.ValueB,
                    IntValue = 49244
                },
                new GridRowData {
                    IsChecked = true,
                    Text = "Pluto",
                    EnumValue = EnumValues.ValueA,
                    IntValue = 2370
                }
            };
        }

        private void TcpClient_Received(CommunicationEventArgs e)
        {
            _logger.LogInformation($"收到数据:{e.Time} \t {e}");
        }

        private void TcpClient_Sended(CommunicationEventArgs e)
        {
            _logger.LogInformation($"发送数据:{e.Time} \t {e}");
        }
    }

    public class GridRowData
    {
        public bool IsChecked { get; set; }
        public string? Text { get; set; }
        public EnumValues EnumValue { get; set; }
        public int IntValue { get; set; }
    }

    public enum EnumValues
    {
        ValueA, ValueB, ValueC, ValueD
    }
}
