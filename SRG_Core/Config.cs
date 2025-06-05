using System.Text.Json;

namespace SRG_Core;

public class Config : Main
{
    public string CsVer; // CS Program file version (X.y.X)
    public string CsVersion; // CS Program file version (X.X.y)

    // X = ignore Y = check for differences
    public string JsonVersion;// JSON config file version (X.X.y)
    public string Url;// URL from JSON config file
    public string JsonVer;// JSON config file version (X.y.X)
    public int PingDelay;// Pause lenght between pings (in ms)
    public string Lang;// Language (currently: en/cz)
    public bool Beta;

    public Config LoadedConfig;

    public Config(string csVer = null, string csVersion = null, string jsonVersion = null, string url = null, string jsonVer = null, int pingDelay = default, string lang = null, bool beta = default, Config loadedConfig = null)
    {
        CsVer = csVer;
        CsVersion = csVersion;
        JsonVersion = jsonVersion;
        Url = url;
        JsonVer = jsonVer;
        PingDelay = pingDelay;
        Lang = lang;
        Beta = beta;
        LoadedConfig = loadedConfig;
    }

    public void LoadConfig()
    {
        if (!File.Exists("Config.json"))
        {
            // ToDo: code to create a default config file
        }

        string jsonConfig = File.ReadAllText("Config.json");
        var loadedConfig = JsonSerializer.Deserialize<Config>(jsonConfig);

        if (loadedConfig != null)
        {
            CsVer = "0.2";
            CsVersion = "0.2.0";
            JsonVersion = loadedConfig.JsonVersion;
            Url = loadedConfig.Url;
            JsonVer = loadedConfig.JsonVer;
            PingDelay = loadedConfig.PingDelay;
            Lang = loadedConfig.Lang;
            Beta = loadedConfig.Beta;
        }
    }
    
}