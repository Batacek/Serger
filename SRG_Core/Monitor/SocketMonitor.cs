using System.Net.Sockets;
using System.Text.Json.Serialization;

namespace Serger.SRG_Core.Config;

public class SocketMonitor : Monitor
{
    
    [JsonPropertyName("port")]
    public int Port { get; set; }
    
    public SocketMonitor() { }
    
    public SocketMonitor(string host, int checkInterval, int timeout, int port) 
        : base(host, checkInterval, timeout)
    {
        Port = port;
    }
    
    public override bool IsUp()
    {
        try
        {
            using var client = new TcpClient();
            
            var task = client.ConnectAsync(Host, Port);
            
            return task.Wait(Timeout) && client.Connected;
        }
        catch
        {
            return false;
        }
    }
    
}