using System.Text.Json;

namespace SRG_Core;

public class Config : Main
{
    public string CsVer; // CS Program file version (X.y.X)
    public string CsVersion; // CS Program file version (X.X.y)

    // X = ignore Y = check for differences
    public string JSON_Version;// JSON config file version (X.X.y)
    public string URL;// URL from JSON config file
    public string JSON_VER;// JSON config file version (X.y.X)
    public int PING_DELAY;// Pause lenght between pings (in ms)
    public string LANG;// Language (currently: en/cz)
    public bool BETA;

    public Config LoadedConfig;

    public Config(string csVer = null, string csVersion = null, string jsonVersion = null, string url = null, string jsonVer = null, int pingDelay = default, string lang = null, bool beta = default, Config loadedConfig = null)
    {
        CsVer = csVer;
        CsVersion = csVersion;
        JSON_Version = jsonVersion;
        URL = url;
        JSON_VER = jsonVer;
        PING_DELAY = pingDelay;
        LANG = lang;
        BETA = beta;
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
            JSON_Version = loadedConfig.JSON_Version;
            URL = loadedConfig.URL;
            JSON_VER = loadedConfig.JSON_VER;
            PING_DELAY = loadedConfig.PING_DELAY;
            LANG = loadedConfig.LANG;
            BETA = loadedConfig.BETA;
        }
    }

    public string[] GetConfig()
    {
        Config load = new Config();
        string[] config = {load.CsVer, load.CsVersion, load.JSON_Version, load.URL, load.JSON_VER, load.PING_DELAY.ToString(), load.LANG, load.BETA.ToString()};

        return config;
    }
}