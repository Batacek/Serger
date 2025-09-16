using System.Text.Json.Serialization;

namespace Serger.SRG_Core.Config;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PingMonitor), "ping")]
[JsonDerivedType(typeof(SocketMonitor), "socket")]
[JsonDerivedType(typeof(HttpMonitor), "http")]
public abstract class Monitor
{
    [JsonPropertyName("check_interval")]
    public int CheckInterval { get; set; }
    
    [JsonPropertyName("timeout")]
    public int Timeout { get; set; }
    
    protected Monitor() { }
    
    protected Monitor(int checkInterval, int timeout)
    {
        CheckInterval = checkInterval;
        Timeout = timeout;
    }
    
    public abstract bool IsUp();
    
    public abstract string GetDisplayName();
    
}