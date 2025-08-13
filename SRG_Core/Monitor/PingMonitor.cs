using System.Net.NetworkInformation;

namespace Serger.SRG_Core.Config;

public class PingMonitor : Monitor
{
    
    public PingMonitor() { }
    
    public PingMonitor(string host, int checkInterval, int timeout) 
        : base(host, checkInterval, timeout) { }
    
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
    
}