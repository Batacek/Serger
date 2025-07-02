using System.Text.Json;

namespace Serger.SRG_Core;

public class Lang
{
    // Language strings for the application
    public string PingText { get; set; }
    public string Success { get; set; }
    public string Address { get; set; }
    public string PingTime { get; set; }
    public string Fail { get; set; }
    public string Error { get; set; }
    public string VersionMis1 { get; set; }
    public string Version { get; set; }
    public string InsertCorr1 { get; set; }
    public string InsertCorr2 { get; set; }
    public string PressKey { get; set; }

    public Lang()
    {
        // Default values (English)
        PingText = "Pinging";
        Success = "Success";
        Address = "Address";
        PingTime = "Ping Time";
        Fail = "Fail";
        Error = "Error";
        VersionMis1 = "Version mismatch detected!";
        Version = "Version";
        InsertCorr1 = "Please insert the correct address.";
        InsertCorr2 = "Press any key to continue...";
        PressKey = "Press any key to continue...";
    }

    public static Lang LoadLang(string langCode)
    {
        string langFile = $"Langs/{langCode}.json";

        // Check if the language file exists
        if (!File.Exists(langFile))
        {
            Console.WriteLine($"Language file not found: {langFile}. Using default language.");
            return new Lang();
        }

        try
        {
            string jsonLang = File.ReadAllText(langFile);
            var loadedLang = JsonSerializer.Deserialize<Lang>(jsonLang);

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
