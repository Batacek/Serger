using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serger.SRG_Core.Config;

public class Config
{
    
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };
    
    [JsonIgnore]
    public string CsVer { get; set; } = "0.2"; // CS Program file version (X.y.X)
    [JsonIgnore]
    public string CsVersion { get; set; } = "0.2.2"; // CS Program file version (X.X.y)

    // X = ignore Y = check for differences
    [JsonPropertyName("JSON_Version")]
    public string JsonVersion { get; set; } = "0.2.2"; // JSON config file version (X.X.y)
    
    [JsonPropertyName("JSON_VER")]
    public string JsonVer { get; set; } = "0.2"; // JSON config file version (X.y.X)
    
    [JsonPropertyName("LANG")]
    public string Lang { get; set; } = "en"; // Language (currently: en/cs)

    [JsonPropertyName("DEV_MODE")]
    public bool Beta { get; set; }

    [JsonPropertyName("CLI_EN")]
    public bool CliEnabled { get; set; }

    [JsonPropertyName("MULTITASKING")]
    public bool Multitasking { get; set; }

    [JsonPropertyName("URL")]
    public string Url { get; set; } = "127.0.0.1"; // URL from the JSON config file

    [JsonPropertyName("PING_DELAY")]
    public int PingDelay { get; set; } = 10000; // Pause length between pings (in ms)
    
    [JsonPropertyName("monitors")]
    public List<Monitor> Monitors { get; set; } = [];
    
    public void LoadConfig(Lang? lang = null)
    {
        if (!File.Exists("Config.json"))
        {
            var jsonString = JsonSerializer.Serialize(this, Options);
            File.WriteAllText("Config.json", jsonString);
            var message = lang?.ConfigNotFound ?? "Created new Config.json with default values.";
            Console.WriteLine(message);
            return;
        }

        try
        {
            var jsonConfig = File.ReadAllText("Config.json");
            var loadedConfig = JsonSerializer.Deserialize<Config>(jsonConfig);

            if (loadedConfig == null) return;
            
            JsonVer = loadedConfig.JsonVer;
            JsonVersion = loadedConfig.JsonVersion;
            Url = loadedConfig.Url;
            PingDelay = loadedConfig.PingDelay;
            Lang = loadedConfig.Lang;
            Beta = loadedConfig.Beta;
            CliEnabled = loadedConfig.CliEnabled;
            Multitasking = loadedConfig.Multitasking;
            Monitors = loadedConfig.Monitors;

            var message = lang?.ConfigLoaded ?? "Config loaded successfully from Config.json.";
            Console.WriteLine(message);
        }
        catch (Exception ex)
        {
            var errorMessage = lang?.ConfigLoadError ?? "Error loading configuration";
            Console.WriteLine($"{errorMessage}: {ex.Message}");
            Console.WriteLine("Using default values.");
        }
    }

    public void SaveConfig(Lang? lang = null)
    {
        try
        {
            var jsonString = JsonSerializer.Serialize(this, Options);
            File.WriteAllText("Config.json", jsonString);
            var message = lang?.ConfigSaved ?? "Config saved successfully to Config.json";
            Console.WriteLine(message);
        }
        catch (Exception ex)
        {
            var errorMessage = lang?.ConfigSaveError ?? "Error saving configuration";
            Console.WriteLine($"{errorMessage}: {ex.Message}");
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
        Console.WriteLine($"Monitors: {Monitors.Count}");
    }
    
}
