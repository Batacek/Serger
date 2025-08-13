using System.Text.Json;

namespace Serger.SRG_Core;

public class Lang
{

    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    // System messages
    public string Error { get; set; } = "Error";
    public string Success { get; set; } = "Success";
    public string LanguageLoaded { get; set; } = "Language loaded";
    public string SergerStarted { get; set; } = "Serger started";

    // Config messages
    public string ConfigLoaded { get; set; } = "Config loaded successfully";
    public string ConfigSaved { get; set; } = "Config saved successfully";
    public string ConfigNotFound { get; set; } = "Config file not found, created default";
    public string ConfigLoadError { get; set; } = "Error loading configuration";
    public string ConfigSaveError { get; set; } = "Error saving configuration";

    // Monitor messages
    public string MonitorUp { get; set; } = "UP";
    public string MonitorDown { get; set; } = "DOWN";
    public string MonitorExecuteError { get; set; } = "Error executing monitor";
    public string MonitorsLoaded { get; set; } = "monitors loaded from";
    public string MonitorsSaved { get; set; } = "monitors saved to";
    public string MonitorLoadError { get; set; } = "Error loading monitor configuration";
    public string MonitorSaveError { get; set; } = "Error saving monitor configuration";
    public string MonitorConfigNotFound { get; set; } = "Monitor config file not found";

    // Monitor types
    public string PingMonitor { get; set; } = "Ping";
    public string HttpMonitor { get; set; } = "HTTP";
    public string SocketMonitor { get; set; } = "Socket";

    public static Lang LoadLang(string langCode)
    {
        var langFile = $"Langs/{langCode}.json";

        // Check if the language file exists
        if (!File.Exists(langFile))
        {
            Console.WriteLine($"Language file not found: {langFile}. Using default language.");
            return new Lang();
        }

        try
        {
            var jsonLang = File.ReadAllText(langFile);

            var loadedLang = JsonSerializer.Deserialize<Lang>(jsonLang, Options);

            if (loadedLang != null)
            {
                return loadedLang;
            }

            Console.WriteLine("Failed to deserialize language file. Using default language.");
            return new Lang();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading language file: {ex.Message}. Using default language.");
            return new Lang();
        }
    }
    
}
