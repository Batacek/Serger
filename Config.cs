namespace Serger;

public class Config
{
    // Class for configuration variables and values
    // X = ignore Y = check for differences
    public string JsonVersion { get; set; } // JSON config file version (X.X.y)
    public string Url { get; set; } // URL from JSON config file
    public string JsonVer { get; set; } // JSON config file version (X.y.X)
    public int PingDelay { get; set; } // Pause lenght between pings (in ms)
    public string Lang { get; set; } // Language (currently: en/cz)
    public string CsVersion = "0.0.5"; // CS Program file version (X.X.y)
    public string CsVer = "0.0"; // CS Program file version (X.y.X)
}