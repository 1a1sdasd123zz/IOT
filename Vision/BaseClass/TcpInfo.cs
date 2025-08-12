namespace Vision.BaseClass;

public class TcpInfo
{
    public string IP { get; }
    public string Port { get; }

    public TcpInfo(string ip, string port)
    {
        IP = ip;
        Port = port;
    }
}