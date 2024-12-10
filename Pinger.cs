using System.Net.NetworkInformation;

namespace Serger;

public class Pinger
{
    private readonly Config _config;
    private readonly LangDictionary _langDictionary;

    public Pinger(Config config, LangDictionary langDictionary)
    {
        _config = config;
        _langDictionary = langDictionary;
    }

    public async Task PingAddr()
    {
        Console.Clear();

        try
        {
            PingReply reply = new Ping().Send(_config.URL); // Ping
            Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}"); // Date and time
            if (reply.Status == IPStatus.Success) // if condition for success case
            {
                // Printing the info into console window
                Console.WriteLine($"{_langDictionary.PingText} {_config.URL} {_langDictionary.Success}.");
                Console.WriteLine($"{_langDictionary.Address}: {reply.Address}");
                Console.WriteLine($"{_langDictionary.PingTime}: {reply.RoundtripTime} ms");
                Console.WriteLine($"TTL: {reply.Options.Ttl}\n");

                // Printing the info into .log file
                Log.Write($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")}");
                Log.Write($"{_langDictionary.PingText} {_config.URL} {_langDictionary.Success}.");
                Log.Write($"{_langDictionary.Address} {reply.Address}");
                Log.Write($"{_langDictionary.PingTime} {reply.RoundtripTime} ms");
                Log.Write($"TTL: {reply.Options.Ttl}\n");
            }
            else
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                Console.WriteLine($"{_langDictionary.PingText} {_config.URL} {_langDictionary.Fail}: {reply.Status}");
                Console.Beep(); // tohle snad nemyslíš vážně??
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"{_langDictionary.PingText} {_config.URL} {_langDictionary.Fail}. \n{_langDictionary.Error}: {ex.Message}"); // Printout the error message
            Console.WriteLine();
        }

        await Task.Delay(_config.PING_DELAY); // Pause between pings
    }
}