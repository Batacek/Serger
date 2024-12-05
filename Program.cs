using System;
using System.IO;
using System.Text.Json;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Serger;

class Program
{
    private static string _logFile = $"{DateTime.Now.ToString("dd_MM_yyyy")}.log";

    public static void Log(string message)
    {
        using (StreamWriter logging = new StreamWriter(_logFile, true))
        {
            logging.WriteLine(message);
        }
    }
    static async Task Main(string[] args)
    {
        Console.WindowHeight = 20;          // Window height
        Console.WindowWidth = 40;           // Window width
        
        string jsonConfig = File.ReadAllText("Config.json");              // Config file name
        var config = JsonSerializer.Deserialize<Config>(jsonConfig);           // Config deserialize

        string langFile = $"{config.Lang}.json";
        
        // Check if language in config exists, exit if it does not
        if (!File.Exists(langFile))
        {
            Console.WriteLine($"Language does not exist. Change LANG in Config.json.");
            Environment.Exit(0);
        }

        string jsonLang = File.ReadAllText($"{config.Lang}.json");          // Gets language from Config.json
        var langOption = LoadLang(jsonLang);                            // Uses selected language

                                                // X = ignore Y = check for differences
        if (config.JsonVer != config.CsVer)     // Version check (y.y.X)
        {
            Console.WriteLine($"!    {langOption.VersionMis1}    !");
            Console.WriteLine($"JSON {langOption.Version}: {config.JsonVersion}");
            Console.WriteLine($"Program {langOption.Version}: {config.CsVersion}");
            Console.WriteLine($"\n{langOption.InsertCorr1}.");
            Console.WriteLine($"{langOption.PressKey}...");
            Console.ReadKey();
        }        
        else if (config.JsonVersion != config.CsVersion)   // Version check (X.X.y)
        {
            Console.WriteLine($"!    {langOption.VersionMis1}    !");
            Console.WriteLine($"JSON {langOption.Version}: {config.JsonVersion}");
            Console.WriteLine($"Program {langOption.Version}: {config.CsVersion}");
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
                PingReply reply = new Ping().Send(config.Url);      // Ping
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}");   // Date and time
                if (reply.Status == IPStatus.Success)       // if condition for success case
                {
                    // Printing the info into console window
                    Console.WriteLine($"{langOption.PingText} {config.Url} {langOption.Success}.");
                    Console.WriteLine($"{langOption.Address}: {reply.Address}");
                    Console.WriteLine($"{langOption.PingTime}: {reply.RoundtripTime} ms");
                    Console.WriteLine($"TTL: {reply.Options.Ttl}\n");

                    // Printing the info into .log file
                    Log($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")}"); 
                    Log($"{langOption.PingText} {config.Url} {langOption.Success}.");
                    Log($"{langOption.Address} {reply.Address}");
                    Log($"{langOption.PingTime} {reply.RoundtripTime} ms");
                    Log($"TTL: {reply.Options.Ttl}\n");
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                    Console.WriteLine($"{langOption.PingText} {config.Url} {langOption.Fail}: {reply.Status}");
                    Console.Beep();     // tohle snad nemyslíš vážně??
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{langOption.PingText} {config.Url} {langOption.Fail}. \n{langOption.Error}: {ex.Message}");       // Printout the error message
                Console.WriteLine();
            }

            await Task.Delay(config.PingDelay);        // Pause between pings
        }
    }
    public static LangDict LoadLang(string json)
    {
        return JsonSerializer.Deserialize<LangDict>(json);
    }
}

