using System.Text.Json;

namespace Serger;

class Program
{
    static async Task Main()
    {
        Console.WindowHeight = 20; // Window height
        Console.WindowWidth = 40; // Window width

        var config = Config.Load();

        string langFile = $"{config.LANG}.json";

        // Check if language in config exists, exit if it does not
        if (!File.Exists(langFile))
        {
            Console.WriteLine($"Language does not exist. Change LANG in Config.json. {langFile}");
            Environment.Exit(0);
        }

        string jsonLang = File.ReadAllText($"{config.LANG}.json"); // Gets language from Config.json
        var langOption = LoadLang(jsonLang); // Uses selected language

        config.CheckVer(langOption);
        while (true)
        {
            var pinger = new Pinger(Config.Load(), langOption);
            await pinger.PingAddr();
        }
    }

    public static LangDictionary LoadLang(string json)
    {
        return JsonSerializer.Deserialize<LangDictionary>(json) ?? throw new InvalidOperationException();
    }
}