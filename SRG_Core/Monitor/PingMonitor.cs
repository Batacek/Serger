using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace Serger.SRG_Core.Config;

public class PingMonitor : Monitor
{
    [JsonPropertyName("host")]
    public string Host { get; set; } = "";
    
    public PingMonitor() { }
    
    public PingMonitor(string host, int checkInterval, int timeout) 
        : base(checkInterval, timeout) 
    {
        Host = host;
    }
    
    public override bool IsUp()
    {
        try
        {
            using var ping = new Ping();
            
            var reply = ping.Send(Host, Timeout);
            
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }
    
    public override string GetDisplayName()
    {
        return Host;
    }
}