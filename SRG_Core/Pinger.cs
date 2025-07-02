using System.Net.NetworkInformation;

namespace Serger.SRG_Core;

public class Pinger
{
    private readonly Config _config;
    private readonly Lang _lang;

    public Pinger(Config config, Lang lang)
    {
        this._config = config;
        this._lang = lang;
    }

    public async Task PingAddr()
    {
        try
        {
            PingReply reply = new Ping().Send(_config.Url); // Ping
            Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}"); // Date and time
            if (reply.Status == IPStatus.Success) // if condition for success cases
            {
                // Printing the info into the console window and log file
                Log.PrintLog($"{_lang.PingText} {_config.Url} {_lang.Success}.");
                Log.PrintLog($"{_lang.Address}: {reply.Address}");
                Log.PrintLog($"{_lang.PingTime}: {reply.RoundtripTime} ms");
                Log.PrintLog($"TTL: {reply.Options.Ttl}\n");
            }
            else
            {
                Log.PrintLog($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                Log.PrintLog($"{_lang.PingText} {_config.Url} {_lang.Fail}: {reply.Status}");
            }
        }
        catch (Exception ex)
        {
            Log.PrintLog($"{_lang.PingText} {_config.Url} {_lang.Fail}. \n{_lang.Error}: {ex.Message}"); // Printout the error message
        }

        await Task.Delay(_config.PingDelay); // Pause between pings
    }
}
