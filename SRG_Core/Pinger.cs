using System.Net.NetworkInformation;

namespace SRG_Core;

public class Pinger : Main
{
    private readonly Config _config;
    //private readonly LangDictionary _langDictionary;

    public Pinger(Config config)
    {
        this._config = config;
        //_langDictionary = langDictionary;
    }

    public async Task PingAddr()
    {
        try
        {
            PingReply reply = new Ping().Send(_config.Url); // Ping
            Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}"); // Date and time
            if (reply.Status == IPStatus.Success) // if condition for success case
            {
                // Printing the info into console window and log file
                Log.PrintLog($"Ping {_config.Url} Success.");
                Log.PrintLog($"Address: {reply.Address}");
                Log.PrintLog($"Ping time: {reply.RoundtripTime} ms");
                Log.PrintLog($"TTL: {reply.Options.Ttl}\n");
            }
            else
            {
                Log.PrintLog($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                Log.PrintLog($"Ping {_config.Url} Fail: {reply.Status}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Ping {_config.Url} Fail. \nError: {ex.Message}"); // Printout the error message
            Console.WriteLine();
        }

        await Task.Delay(_config.PingDelay); // Pause between pings
    }

}