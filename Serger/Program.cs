using System;
using System.IO;
using System.Text.Json;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

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
        Console.WindowHeight = 20;      // Window height
        Console.WindowWidth = 40;       // Window width
        string jsonString = File.ReadAllText("Config.json");    // Config file name
        var Config = JsonSerializer.Deserialize<Config>(jsonString);    // Config deserialize

        if (Config.JSON_VER != Config.CS_VER)    // Version check (y.y.X)
        {
            Console.WriteLine("!    Version mismatch    !");
            Console.WriteLine($"JSON Version: {Config.JSON_Version}");
            Console.WriteLine($"Program Version: {Config.CS_Version}");
            Console.WriteLine("\nPlease insert correct version of Config file to prevent issues.");

        }        
        if (Config.JSON_Version != Config.CS_Version)   // Version check (X.X.y)
        {
            Console.WriteLine($"Your program is running on version {Config.CS_Version} while your Config files are for {Config.JSON_Version}. \nTo prevent issues you should update it to the same version. Some functions may be unavaible because of the version differences.");
        }

        Log("Test");
        
        while (true)        // Program loop
        {
            Ping pingSender = new Ping();

            try
            {
                PingReply reply = new Ping().Send(Config.URL);      // Ping
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fffffff")}");   // Date and time
                if (reply.Status == IPStatus.Success)       // if condition for success case
                {
                    // Printing the info into console window
                    Console.WriteLine($"Ping na {Config.URL} byl úspěšný.");
                    Console.WriteLine($"Adresa: {reply.Address}");
                    Console.WriteLine($"Doba odezvy: {reply.RoundtripTime} ms");
                    Console.WriteLine($"TTL: {reply.Options.Ttl}");
                    Console.WriteLine();     
                    // Printing the info into log file
                    Log($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")}"); 
                    Log($"Ping na {Config.URL} byl úspěšný.");
                    Log($"Adresa: {reply.Address}");
                    Log($"Doba odezvy: {reply.RoundtripTime} ms");
                    Log($"TTL: {reply.Options.Ttl}");
                    Log("");
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                    Console.WriteLine($"Ping na {Config.URL} selhal: {reply.Status}");
                    Console.Beep();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ping na {Config.URL} selhal. \nChyba: {ex.Message}");       // Printout the error message
                Console.WriteLine();
            }

            await Task.Delay(Config.PING_DELAY);        // Pause between pings
        }
    }
}

public class Config         // Class for configuration variables and values
{
    public string JSON_Version { get; set; }        // JSON config file version (X.X.y)
    public string URL { get; set; }                 // URL from JSON config file
    public string JSON_VER { get; set; }            // JSON config file version (X.y.X)
    public int PING_DELAY { get; set; }             // Pause lenght between pings (in ms)
    public string CS_Version = "0.0.3";             // CS Program file version (X.X.y)
    public string CS_VER = "0.0";                   // CS Program file version (X.y.X)
}

