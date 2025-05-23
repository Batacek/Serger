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

    public Config(string csVer = null, string csVersion = null, string jsonVersion = null, string url = null,
        string jsonVer = null, int pingDelay = default, string lang = null, bool beta = default)
    {
        CsVer = csVer ?? throw new ArgumentNullException(nameof(csVer));
        CsVersion = csVersion ?? throw new ArgumentNullException(nameof(csVersion));
        JSON_Version = jsonVersion ?? throw new ArgumentNullException(nameof(jsonVersion));
        URL = url ?? throw new ArgumentNullException(nameof(url));
        JSON_VER = jsonVer ?? throw new ArgumentNullException(nameof(jsonVer));
        PING_DELAY = pingDelay;
        LANG = lang ?? throw new ArgumentNullException(nameof(lang));
        BETA = beta;
    }

    private Config LoadConfig()
    {
        if (!File.Exists("Config.json"))
        {
            // Add code to create a default config file
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

        Config LoadedConfiguration = new Config(
            CsVer,
            CsVersion,
            JSON_Version,
            URL,
            JSON_VER,
            PING_DELAY,
            LANG,
            BETA
        );

        return LoadedConfiguration;

    }
}