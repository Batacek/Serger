using System.Text.Json;

public class Config // Class for configuration variables and values
{
    public static string CS_Version = "0.1.0"; // CS Program file version (X.X.y)
    public static string CS_VER = "0.1"; // CS Program file version (X.y.X)

    // X = ignore Y = check for differences
    public static string JSON_Version { get; set; } // JSON config file version (X.X.y)
    public string URL { get; set; } // URL from JSON config file
    public static string JSON_VER { get; set; } // JSON config file version (X.y.X)
    public int PING_DELAY { get; set; } // Pause lenght between pings (in ms)
    public string LANG { get; set; } // Language (currently: en/cz)
    public static bool BETA { get; set; }


    public static Config Load(string filePath = "Config.json")
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Config file could not be found.");
        }

        string jsonConfig = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Config>(jsonConfig);
    }

    public void CheckVer(LangDictionary _langDictionary)
    {
        Console.WriteLine("THIS IS BETA VERSION!");
        // X = ignore Y = check for differences
        if (JSON_VER != CS_VER) // Version check (y.y.X)
        {
            Console.WriteLine($"!    {_langDictionary.VersionMis1}    !");
            Console.WriteLine($"JSON {_langDictionary.Version}: {JSON_Version}");
            Console.WriteLine($"Program {_langDictionary.Version}: {CS_Version}");
            Console.WriteLine($"\n{_langDictionary.InsertCorr1}.");
            Console.WriteLine($"{_langDictionary.PressKey}...");
            Console.ReadKey();
        }
        else if (JSON_Version != CS_Version) // Version check (X.X.y)
        {
            Console.WriteLine($"!    {_langDictionary.VersionMis1}    !");
            Console.WriteLine($"JSON {_langDictionary.Version}: {JSON_Version}");
            Console.WriteLine($"Program {_langDictionary.Version}: {CS_Version}");
            Console.WriteLine($"\n{_langDictionary.InsertCorr1}. {_langDictionary.InsertCorr2}.");
            Console.WriteLine($"{_langDictionary.PressKey}...");
            Console.ReadKey();
        }
    }
}