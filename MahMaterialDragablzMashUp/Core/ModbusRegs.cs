namespace MainFrom
{
    /// <summary>
    /// Modbus寄存器地址映射表
    /// </summary>
    public  class ModbusRegs
    {
        /// <summary>
        /// 编码器当前累计数(高16位)
        /// </summary>
        public static byte EncoderHigh => _EncoderHigh;
        private const byte _EncoderHigh = 0x00;

        /// <summary>
        /// 编码器当前累计数(低16位)
        /// </summary>
        public const byte EncoderLow = 0x01;

        /// <summary>
        /// 1、张力器当前实际位置(范围：0-100，百分比表示)；2、双线检测值；
        /// </summary>
        public byte TensionerMode => _TensionerMode;
        private const byte _TensionerMode = 0x02;

        /// <summary>
        /// 进布牵引伺服电机状态 0:停止 1:运行中 2:故障
        /// </summary>
        public const byte MasterMotorState = 0x03;

        /// <summary>
        /// 贴标报警状态
        /// </summary>
        public const byte TicketAlarm = 0x04;

        /// <summary>
        /// 电机状态
        /// BIT15： 保留 （V10.7）
        /// BIT14：保留 （V10.6)
        /// BIT13： 保留 （V10.5)
        /// BIT12：保留 （V10.4)
        /// BIT11： 保留 （V10.3)
        /// BIT10： 保留 （V10.2)
        /// BIT9： 保留 （V10.1)
        /// BIT8： 吸标失败 （V10.0)
        /// BIT7=1 放卷变频器故障（V11.7） 
        /// BIT6=1 放卷变频器运行（V11.6）
        /// BIT5=1 摆布变频器故障（V11.5）
        /// BIT4=1 摆布变频器运行（V11.4）
        /// BIT3=1 张力变频器故障（V11.3）
        /// BIT2=1 张力变频器运行（V11.2）
        /// BIT1=1 牵引变频器故障（V11.1）
        /// BIT0=1 牵引变频器运行（V11.0）     
        /// </summary>
        public const byte MotorState = 0x05;

        /// <summary>
        /// 工作状态 0:自动 1:手动
        /// </summary>
        public const byte WorkState = 0x06;

        /// <summary>
        /// 准备完成  =0 系统未准备完成   =1 系统准备完成，可以启动
        /// </summary>
        public const byte DeviceReady = 0x07;

        /// <summary>
        /// 报警 故障输出 0：无报警  1：有报警 
        ///BIT15=1 V16.7	报警输出-叠布站叠布轴
        ///BIT14 = 1 V16.6	报警输出-放卷站放卷轴
        ///BIT13 = 1 V16.5	报警输出-收卷站分丝轴
        ///BIT12 = 1 V16.4	报警输出-收卷站收卷轴
        ///BIT11 = 1 V16.3	报警输出-进布站分丝轴
        ///BIT10 = 1 V16.2	报警输出-进布站进布轴
        ///BIT9 = 1 V16.1	报警输出-验布站张力轴
        ///BIT8 = 1 V16.0	报警输出-验布站主动轴
        ///BIT7 = 1 保留（V17.7） 
        ///BIT6 = 1 V17.6	报警输出-收卷纠偏伺服
        ///BIT5 = 1 V17.5	报警输出-放卷纠偏伺服
        ///BIT4=1 变频器故障（V17.4）
        ///BIT3=1 卷标失败（V17.3）
        ///BIT2=1 上位机急停输入（V17.2）
        ///BIT1=1 放卷检测到无布（V17.1）
        ///BIT0=1 急停输入（V17.0）
        /// </summary>
        public const byte Alarm = 0x08;

        /// <summary>
        /// 贴标完成  1：完成
        /// </summary>
        public const byte TicketFinish = 0x09;

        /// <summary>
        /// 系统启动/停止状态
        /// </summary>
        public const byte DeviceRunning = 0x0A;

        /// <summary>
        /// 称重传感器重量(实时)
        /// </summary>
        public const byte RealtimeWeight = 0x0B;

        /// <summary>
        /// 称重传感器最后称重值
        /// </summary>
        public const byte FinalWeight = 0x0D;

        /// <summary>
        /// 收卷轴
        /// </summary>
        public const byte PickAxles = 0x10;

        /// <summary>
        /// 1、普通机型，从动电机工作方式 "0:跟随主动电机速度方式  1:PID方式"；
        /// 2、皮革机型，0贝斯，1布
        /// </summary>
        public const byte SlaveMotorWorkMode = 0x11;

        /// <summary>
        /// 设定牵引电机速度（连续两个寄存器，32位浮点， 范围：0.0-0.5 米/S）
        /// </summary>
        public const byte MasterMotorSpeed = 0x12;

        /// <summary>
        /// 设定张力电机速度系数（连续两个寄存器，32位浮点， 范围：0.0-2.0）
        /// </summary>
        public const byte SlaveMotorRatio = 0x14;

        /// <summary>
        /// 设定张力位置
        /// </summary>
        public const byte SetTensionerPosion = 0x16;

        /// <summary>
        /// 设定摆布电机速度系数（连续两个寄存器，32位浮点， 范围：0.0-2.0）
        /// </summary>
        public const byte SwingMotorRatio = 0x17;

        /// <summary>
        /// 1、PID参数：X轴当前位置（连续两个寄存器，32位浮点)
        /// 2、皮革新机：废料收卷的编码器值
        /// </summary>
        public const byte CurrentPositionX = 0x19;

        /// <summary>
        /// PID参数：Y轴当前位置（连续两个寄存器，32位浮点)
        /// </summary>
        public const byte CurrentPositionZ = 0x1B;

        /// <summary>
        /// PID参数：Z轴当前位置（连续两个寄存器，32位浮点)
        /// </summary>
        public const byte CurrentPositionY = 0x1D;

        /// <summary>
        /// 急停  1：急停（计算机让系统急停）
        /// </summary>
        public const byte SetScram = 0x1F;

        /// <summary>
        /// 复位  1：复位作业，复位故障
        /// </summary>
        public const byte ResetPLC = 0x20;

        /// <summary>
        /// 给编码器累计器清零 1：清零
        /// </summary>
        public const byte ResetEncoder = 0x21;

        /// <summary>
        /// PLC启动/停止 "0：停止 1：启动"
        /// </summary>
        public const byte PLCStart_stop = 0x22;

        /// <summary>
        /// 通知贴标 1： 通知贴标
        /// </summary>
        public const byte Ticket = 0x23;

        /// <summary>
        /// 贴标X坐标值
        /// </summary>
        public const byte TicketPositionX = 0x24;

        /// <summary>
        /// 
        /// </summary>
        public const byte TicketAlarmCount = 0x25;

        /// <summary>
        /// 裁剪信号
        /// </summary>
        public const byte Cut = 0x25;

        /// <summary>
        /// 贴标Y坐标值
        /// </summary>
        public const byte TicketPositionY = 0x26;

        /// <summary>
        /// PC开关蜂鸣器  0:停止 1:启动
        /// </summary>
        public const byte Buzzer = 0x28;

        /// <summary>
        /// 进卷/退卷模式 0:进卷 1:退卷
        /// </summary>
        public const byte ForwardBackMode = 0x29;

        /// <summary>
        /// 放卷轴控制 0：A轴 1：B轴
        /// </summary>
        public const byte ReleaseAxles = 0x2A;

        /// <summary>
        /// 准备贴标
        /// </summary>
        public const byte ReadyForTicket = 0x2B;

        /// <summary>
        /// 设置PLC的控制模式  0:自动 1:手动 手动模式用于上位机控制
        /// </summary>
        public const byte SetManualMode = 0x2C;

        /// <summary>
        /// 手动模式下控制(PLC)
        /// BIT15： 保留 （V90.7）
        /// BIT14： 保留 （V90.6)
        /// BIT13： 保留 （V90.5)
        /// BIT12：保留 （V90.4)
        /// BIT11： 保留 （V90.3)
        /// BIT10：=0/1 卷标电机停止/启动（V90.2)
        /// BIT9：=0/1 真空阀关/开（V90.1）)
        /// BIT8：=0/1 气缸伸出/后退（V90.0）)         
        /// BIT7: =0/1 直流分边电机停止（V91.7） 
        /// BIT6: =0/1 直流分边电机启动（V91.6）
        /// BIT5: =0/1 放卷电机反转后退 停止/启动（V91.5）
        /// BIT4: =0/1 放卷电机正转前进 停止/启动（V91.4）
        /// BIT3: =0/1 张力电机反转后退 停止/启动（V91.3）
        /// BIT2: =0/1 张力电机正转前进 停止/启动（V91.2）
        /// BIT1: =0/1 主 电机反转后退 停止/启动（V91.1）
        /// BIT0: =0/1 主 电机正转前进 停止/启动（V91.0）
        /// </summary>
        public const byte ManualDebugPLC = 0x2D;

        /// <summary>
        /// 手动模式下控制(贴标机)
        /// BIT15： 保留 （V92.7）
        /// BIT14： 保留 （V92.6)
        /// BIT13： 保留 （V92.5)
        /// BIT12： 保留 （V92.4)
        /// BIT11： 保留 （V92.3)
        /// BIT10：保留（V92.2)
        /// BIT9：保留（V92.1）)
        /// BIT8：保留（V92.0）)              
        /// BIT7: =1/0 按下Z轴下移 松开停止（V93.7） 
        /// BIT6: =1/0 按下Z轴上移 松开停止（V93.6）
        /// BIT5:  =1 上升沿 Z轴停止（V93.5）
        /// BIT4:  =1 上升沿 Z轴回原点（V93.4）
        /// BIT3: =1/0 按下X轴1#相机运动 松开停止（V93.3）
        /// BIT2: =1/0 按下X轴2#相机运动 松开停止（V93.2）
        /// BIT1: =1 上升沿 X轴停止（V93.1）
        /// BIT0: =1 上升沿 X轴回原点（V93.0）
        /// </summary>
        public const byte ManualDebugTicket = 0x2E;

        /// <summary>
        /// 红/绿/黄指示灯控制  
        /// BIT5：  （V95.5) 贴标机归零
        /// BIT4：  （V95.4) 全部归零
        /// BIT3：  （V95.3) 蜂鸣器
        /// BIT2：  （V95.2) 黄
        /// BIT1： （V95.1)  红
        /// BIT0：  （V95.0) 绿        
        /// </summary>
        public const byte IndicatorLight = 0x2F;

        /// <summary>
        /// 保护膜
        /// </summary>
        public const byte ProtectiveFilm = 0x31;

        /// <summary>
        /// 张力差速度，单位m/s
        /// </summary>
        public const byte TensionSpeed = 0x32;

        /// <summary>
        /// 收卷差速度，单位m/s
        /// </summary>
        public const byte WindingSpeed = 0x34;

        /// <summary>
        /// 贴标下降高度，单位mm
        /// </summary>
        public const byte TicketFallingHeight = 0x36;

        /// <summary>
        /// 主速度，单位m/s
        /// </summary>
        public const byte MainSpeed = 0x38;

        /// <summary>
        /// 屏蔽设置Bit7:屏蔽归零完成
        /// Bit6:屏蔽收卷或上布架无布检测
        /// Bit5:屏蔽无布检测
        /// </summary>
        public const byte ShieldSetting = 0x3A;

        ///// <summary>
        ///// 地址：恒张力收卷PID给定值 ,单位N
        ///// </summary>
        //public const byte WindingTensionSetValue = 0x3C;

        ///// <summary>
        ///// 地址：恒张力收卷PID反馈值 ,单位N
        ///// </summary>
        //public const byte WindingTensionGetValue = 0x3E;

        ///// <summary>
        ///// 地址：恒张力验布PID给定值 ,单位N
        ///// </summary>
        //public const byte InspectionTensionSetValue = 0x40;

        ///// <summary>
        ///// 地址：恒张力验布PID反馈值 ,单位N
        ///// </summary>
        //public const byte InspectionTensionGetValue = 0x42;

        /// <summary>
        /// 恒张力验布PID给定值 ,单位N
        /// </summary>
        public const byte InspectionTensionSetValue = 0x3C;

        /// <summary>
        /// 恒张力验布PID反馈值 ,单位N
        /// </summary>
        public const byte InspectionTensionGetValue = 0x3E;

        /// <summary>
        /// 恒张力收卷PID给定值 ,单位N
        /// </summary>
        public const byte WindingTensionSetValue = 0x40;

        /// <summary>
        /// 恒张力收卷PID反馈值 ,单位N
        /// </summary>
        public const byte WindingTensionGetValue = 0x42;

        /// <summary>
        /// 1.围巾机型：围巾计数 2.色差机型：通知色差采集命令
        /// </summary>
        public const byte WeijinCount = 0x44;

        /// <summary>
        /// 幅宽 cm
        /// </summary>
        public const byte ClothWidthGetValue = 0x46;

        /// <summary>
        /// 编码器脉冲长度
        /// </summary>
        public const byte OnePlusLength = 0x48;

        /// <summary>
        /// 传感器开始地址
        /// </summary>
        public const byte IOStatus = 0x4B;

        /// <summary>
        /// 设置编码器值地址
        /// </summary>
        public const byte SetEncoderValue = 0x52;

        /// <summary>
        /// 轴故障明细代码
        /// </summary>
        public const byte AxlesErrorCode = 0x59;

        /// <summary>
        /// 色差仪准备完成信号
        /// </summary>
        public const byte ColourDifferenceReady = 0x45;
        /// <summary>
        /// 色差仪采集X坐标值
        /// </summary>
        public const byte ColourDifferencePositionX = 0x59;

        /// <summary>
        /// 区域贴标Y坐标
        /// </summary>
        public  byte AreaTickeY =>_AreaTickeY;
        private const byte _AreaTickeY = 0x5B;

        /// <summary>
        /// 厚度测试 位移一
        /// </summary>
        public const byte ThicknessDisplacement = 0x63;
        /// <summary>
        /// 厚度测试 位移二
        /// </summary>
        public const byte ThicknessDisplacement2 = 0x65;

        /// <summary>
        ///寄存器数量
        /// </summary>
        public  static ushort Count =>_Count;
        private const ushort _Count = 120;
    }
}
