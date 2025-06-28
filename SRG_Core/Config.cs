using System.Text.Json;

namespace SRG_Core;

public class Config
{
    public string CsVer; // CS Program file version (X.y.X)
    public string CsVersion; // CS Program file version (X.X.y)

    // X = ignore Y = check for differences
    public string JsonVersion;// JSON config file version (X.X.y)
    public string Url;// URL from the JSON config file
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
            this.CsVer = "0.2";
            this.CsVersion = "0.2.1";
            this.JsonVer = "0.2";
            this.JsonVersion = "0.2.1";
            this.Url = "127.0.0.1";
            this.PingDelay = 10000;
            this.Lang = "en";
            this.Beta = false;
        }

        if (File.Exists("Config.json"))
        {
            string jsonConfig = File.ReadAllText("Config.json");
            var loadedConfig = JsonSerializer.Deserialize<Config>(jsonConfig);

            if (loadedConfig != null)
            {
                this.CsVer = "0.2";
                this.CsVersion = "0.2.1";
                this.JsonVer = loadedConfig.JsonVer;
                this.JsonVersion = loadedConfig.JsonVersion;
                this.Url = loadedConfig.Url;
                this.PingDelay = loadedConfig.PingDelay;
                this.Lang = loadedConfig.Lang;
                this.Beta = loadedConfig.Beta;
            }
            else
            {
                this.CsVer = "0.2";
                this.CsVersion = "0.2.1";
                this.JsonVer = "0.2";
                this.JsonVersion = "0.2.1";
                this.Url = "127.0.0.1";
                this.PingDelay = 10000;
                this.Lang = "en";
                this.Beta = false;
            }
        }
    }
    
}