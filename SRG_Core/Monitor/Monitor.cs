using System.Text.Json.Serialization;

namespace Serger.SRG_Core.Config;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PingMonitor), "ping")]
[JsonDerivedType(typeof(SocketMonitor), "socket")]
[JsonDerivedType(typeof(HttpMonitor), "http")]
public abstract class Monitor
{
    [JsonPropertyName("host")]
    public string Host { get; set; } = "";

    [JsonPropertyName("check_interval")]
    public int CheckInterval { get; set; }
    
    [JsonPropertyName("timeout")]
    public int Timeout { get; set; }
    
    protected Monitor() { }
    
    protected Monitor(string host, int checkInterval, int timeout)
    {
        Host = host;
        CheckInterval = checkInterval;
        Timeout = timeout;
    }
    
    public abstract bool IsUp();
    
}