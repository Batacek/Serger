using Serger.SRG_Core;
using Serger.SRG_Core.Config;

namespace Serger;

public abstract class Program
{
    private static Lang LoadLanguage(Config config)
    {
        // Load language based on config setting
        var lang = Lang.LoadLang(config.Lang);

        // Log the language loading
        Log.PrintLog($"{lang.LanguageLoaded}: {config.Lang}");

        return lang;
    }

    private static void Init()
    {
        // Initialize the configuration
        var config = new Config();
        
        // Load config first without language (will use fallback messages)
        config.LoadConfig();
        
        // Load language
        var lang = LoadLanguage(config);
        
        // Initialize the logger
        Log.PrintLog($"{lang.SergerStarted} - CS Version: {config.CsVersion}, JSON Version: {config.JsonVersion}");

        // Display current configuration
        config.ReadConfig();

        // Initialize the monitor scheduler
        var scheduler = new MonitorScheduler(lang);
        
        // Subscribe to monitor results (they are already logged by scheduler)
        scheduler.MonitorResult += (_, _) => {
            // Additional processing can be added here if needed
        };

        // Add some example monitors to config if none exist
        if (config.Monitors.Count == 0)
        {
            config.Monitors.Add(new PingMonitor("8.8.8.8", 30000, 5000));
            config.Monitors.Add(new HttpMonitor(60000, 10000, "https://www.batacek.eu", [200], null));
            config.Monitors.Add(new SocketMonitor("8.8.8.8", 45000, 5000, 53));
            
            // Save the updated config
            config.SaveConfig(lang);
        }
        
        // Start monitoring
        scheduler.UpdateMonitors(config.Monitors);
        
        Log.PrintLog("Monitor scheduler started. Press Ctrl+C to exit.");

        // Keep the application running
        while (true)
        {
            Thread.Sleep(1000);
        }
    }

    public static void Main(string[] args)
    {
        try
        {
            Init();
        }
        catch (Exception ex)
        {
            Log.WriteLog($"Fatal error: {ex.Message}");
            Console.WriteLine($"Fatal error occurred. Check logs for details.");
        }
    }
}
