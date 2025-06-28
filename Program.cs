using SRG_Core;

namespace Serger;

public class Program
{
    private static void init()
    {
        // Initialize the configuration
        Config config = new Config();
        config.LoadConfig();

        // Initialize the logger
        Log.PrintLog($"Serger started with CS Version: {config.CsVersion}, JSON Version: {config.JsonVersion}");

        // Initialize the pinger
        Pinger pinger = new Pinger(config);

        // Start pinging
        while (true)
        {
            pinger.PingAddr().GetAwaiter().GetResult();
        }
    }
    public static void Main(string[] args)
    {
        init();
    }
}