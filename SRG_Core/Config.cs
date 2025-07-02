using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serger.SRG_Core;

public class Config
{
    [JsonIgnore]
    public string CsVer { get; set; } // CS Program file version (X.y.X)
    [JsonIgnore]
    public string CsVersion { get; set; } // CS Program file version (X.X.y)

    // X = ignore Y = check for differences
    [JsonPropertyName("JSON_Version")]
    public string JsonVersion { get; set; } // JSON config file version (X.X.y)

    [JsonPropertyName("URL")]
    public string Url { get; set; } // URL from the JSON config file

    [JsonPropertyName("JSON_VER")]
    public string JsonVer { get; set; } // JSON config file version (X.y.X)

    [JsonPropertyName("PING_DELAY")]
    public int PingDelay { get; set; } // Pause length between pings (in ms)

    [JsonPropertyName("LANG")]
    public string Lang { get; set; } // Language (currently: en/cs)

    [JsonPropertyName("DEV_MODE")]
    public bool Beta { get; set; }

    [JsonPropertyName("CLI_EN")]
    public bool CliEnabled { get; set; }

    [JsonPropertyName("MULTITASKING")]
    public bool Multitasking { get; set; }

    public Config()
    {
        // Default values
        CsVer = "0.2";
        CsVersion = "0.2.2";
        JsonVer = "0.2";
        JsonVersion = "0.2.2";
        Url = "127.0.0.1";
        PingDelay = 10000;
        Lang = "en";
        Beta = false;
        CliEnabled = false;
        Multitasking = false;
    }

    public void LoadConfig()
    {
        if (!File.Exists("Config.json"))
        {
            // Create a default config file
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true 
            };

            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText("Config.json", jsonString);
            Console.WriteLine("Created new Config.json with default values.");
            return;
        }

        try
        {
            string jsonConfig = File.ReadAllText("Config.json");
            var loadedConfig = JsonSerializer.Deserialize<Config>(jsonConfig);

            if (loadedConfig != null)
            {
                JsonVer = loadedConfig.JsonVer;
                JsonVersion = loadedConfig.JsonVersion;
                Url = loadedConfig.Url;
                PingDelay = loadedConfig.PingDelay;
                Lang = loadedConfig.Lang;
                Beta = loadedConfig.Beta;
                CliEnabled = loadedConfig.CliEnabled;
                Multitasking = loadedConfig.Multitasking;

                Console.WriteLine("Config loaded successfully from Config.json.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading configuration: {ex.Message}");
            Console.WriteLine("Using default values.");
        }
    }

    public void ReadConfig()
    {
        Console.WriteLine("Current Configuration:");
        Console.WriteLine($"CS Version: {CsVersion} ({CsVer})");
        Console.WriteLine($"JSON Version: {JsonVersion} ({JsonVer})");
        Console.WriteLine($"URL: {Url}");
        Console.WriteLine($"Ping Delay: {PingDelay}ms");
        Console.WriteLine($"Language: {Lang}");
        Console.WriteLine($"Developer Mode: {Beta}");
        Console.WriteLine($"CLI Enabled: {CliEnabled}");
        Console.WriteLine($"Multitasking: {Multitasking}");
    }
}
