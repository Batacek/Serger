using Serger.SRG_Core;

namespace Serger;

public class Program
{
    private static Lang LoadLanguage(Config config)
    {
        // Load language based on config setting
        Lang lang = Lang.LoadLang(config.Lang);

        // Log the language loading
        Log.PrintLog($"Loaded language: {config.Lang}");

        return lang;
    }

    private static void Init()
    {
        // Initialize the configuration
        Config config = new Config();
        config.LoadConfig();

        // Initialize the logger
        Log.PrintLog($"Serger started with CS Version: {config.CsVersion}, JSON Version: {config.JsonVersion}");

        // Display current configuration
        config.ReadConfig();

        // Load language
        Lang lang = LoadLanguage(config);

        // Initialize the pinger
        Pinger pinger = new Pinger(config, lang);

        // Start pinging
        while (true)
        {
            pinger.PingAddr().GetAwaiter().GetResult();
        }
    }

    public static void Main(string[] args)
    {
        Init();
    }
}
