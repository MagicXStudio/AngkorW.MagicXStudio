public class PlcOptions
{

    public PlcOptions()
    {

    }

    public string IpAddress { get; set; } = "";
    public int Port { get; set; } = 0;
    public string PlcDeviceId { get; set; } = "";
    public int SlaveId { get; set; }
    public List<DataPoint> DataPoints { get; set; }

}
public class DataPoint
{
    public int ModbusAddress { get; set; }
    public string EqpId { get; set; } = "";
}
