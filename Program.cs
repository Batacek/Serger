using System;
using System.IO;
using System.Text.Json;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Collections.Generic;

class Program
{
    private static string log_file = $"{DateTime.Now.ToString("dd_MM_yyyy")}.log";

    public static void Log(string message)
    {
        using (StreamWriter logging = new StreamWriter(log_file, true))
        {
            logging.WriteLine(message);
        }
    }
    static async Task Main(string[] args)
    {
        Console.WindowHeight = 20;          // Window height
        Console.WindowWidth = 40;           // Window width
        
        string jsonConfig = File.ReadAllText("Config.json");            // Config file name
        var Config = JsonSerializer.Deserialize<Config>(jsonConfig);    // Config deserialize

        string langFile = $"{Config.LANG}.json";
        
        // Check if language in config exists, exit if it does not
        if (!File.Exists(langFile))
        {
            Console.WriteLine($"Language does not exist. Change LANG in Config.json.");
            Environment.Exit(0);
        }

        string jsonLang = File.ReadAllText($"{Config.LANG}.json");      // Gets language from Config.json
        var langOption = LoadLang(jsonLang);                            // Uses selected language

                                                // X = ignore Y = check for differences
        if (Config.JSON_VER != Config.CS_VER)   // Version check (y.y.X)
        {
            Console.WriteLine($"!    {langOption.VersionMis1}    !");
            Console.WriteLine($"JSON {langOption.Version}: {Config.JSON_Version}");
            Console.WriteLine($"Program {langOption.Version}: {Config.CS_Version}");
            Console.WriteLine($"\n{langOption.InsertCorr1}.");
            Console.WriteLine($"{langOption.PressKey}...");
            Console.ReadKey();
        }        
        else if (Config.JSON_Version != Config.CS_Version)   // Version check (X.X.y)
        {
            Console.WriteLine($"!    {langOption.VersionMis1}    !");
            Console.WriteLine($"JSON {langOption.Version}: {Config.JSON_Version}");
            Console.WriteLine($"Program {langOption.Version}: {Config.CS_Version}");
            Console.WriteLine($"\n{langOption.InsertCorr1}. {langOption.InsertCorr2}.");
            Console.WriteLine($"{langOption.PressKey}...");
            Console.ReadKey();
        }

        while (true)        // Program loop
        {
            Console.Clear();
            
            Ping pingSender = new Ping();

            try
            {
                PingReply reply = new Ping().Send(Config.URL);      // Ping
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}");   // Date and time
                if (reply.Status == IPStatus.Success)       // if condition for success case
                {
                    // Printing the info into console window
                    Console.WriteLine($"{langOption.PingText} {Config.URL} {langOption.Success}.");
                    Console.WriteLine($"{langOption.Address}: {reply.Address}");
                    Console.WriteLine($"{langOption.PingTime}: {reply.RoundtripTime} ms");
                    Console.WriteLine($"TTL: {reply.Options.Ttl}\n");

                    // Printing the info into .log file
                    Log($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")}"); 
                    Log($"{langOption.PingText} {Config.URL} {langOption.Success}.");
                    Log($"{langOption.Address} {reply.Address}");
                    Log($"{langOption.PingTime} {reply.RoundtripTime} ms");
                    Log($"TTL: {reply.Options.Ttl}\n");
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                    Console.WriteLine($"{langOption.PingText} {Config.URL} {langOption.Fail}: {reply.Status}");
                    Console.Beep();     // tohle snad nemyslíš vážně??
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{langOption.PingText} {Config.URL} {langOption.Fail}. \n{langOption.Error}: {ex.Message}");       // Printout the error message
                Console.WriteLine();
            }

            await Task.Delay(Config.PING_DELAY);        // Pause between pings
        }
    }
    public static LangDictionary LoadLang(string json)
    {
        return JsonSerializer.Deserialize<LangDictionary>(json);
    }
}