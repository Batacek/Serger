using System;
using System.IO;
using System.Text.Json;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string jsonString = File.ReadAllText("Config.json");
        var Config = JsonSerializer.Deserialize<Config>(jsonString);

        if (Config.JSON_VER != Config.CS_VER)
        {
            Console.WriteLine("!    Version mismatch    !");
            Console.WriteLine($"JSON Version: {Config.JSON_Version}");
            Console.WriteLine($"Program Version: {Config.CS_Version}");
            Console.WriteLine("\nPlease insert correct version of Config file to prevent issues.");

        }        
        if (Config.JSON_Version != Config.CS_Version)
        {
            Console.WriteLine($"Your program is running on version {Config.CS_Version} while your Config files are for {Config.JSON_Version}. \nTo prevent issues you should update it to the same version. Some functions may be unavaible because of the version differences.");
        }

        while (true)
        {
            Ping pingSender = new Ping();

            try
            {
                PingReply reply = new Ping().Send(Config.URL);
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine($"Ping na {Config.URL} byl úspěšný.");
                    Console.WriteLine($"Adresa: {reply.Address}");
                    Console.WriteLine($"Doba odezvy: {reply.RoundtripTime} ms");
                    Console.WriteLine($"TTL: {reply.Options.Ttl}");
                    Console.WriteLine($"Velikost bufferu: {reply.Buffer.Length} bajtů");
                }
                else
                {
                    Console.WriteLine($"Ping na {Config.URL} selhal: {reply.Status}");
                    Console.Beep();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ping na {Config.URL} selhal. \nChyba: {ex.Message}");
            }

            await Task.Delay(Config.PING_DELAY);
        }
    }
}

public class Config
{
    public string JSON_Version { get; set; }
    public string URL { get; set; }
    public string JSON_VER { get; set; }
    public int PING_DELAY { get; set; }
    public string CS_Version = "0.0.2";
    public string CS_VER = "0.0";
}