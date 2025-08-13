namespace Serger.SRG_Core;

public static class Log
{
    
    private static readonly string LogFile = $"{DateTime.Now:dd_MM_yyyy}.log";
    private static readonly string LogAllFile = $"{DateTime.Now:dd_MM_yyyy}_ALL.log";

    // Provides logging functionality for writing messages to daily log files without console output
    // Use this method for error messages and important information only
    public static void WriteLog(string message)     
    {
        using var logging = new StreamWriter(LogFile, true);
        logging.WriteLine(message);
    }

    // Provides logging functionality for writing messages to daily log files and console output
    // Use this method for messages for everything that isn't using WriteLog that you want to see in the console as well
    public static void PrintLog(string message)
    {
        
        using var logging = new StreamWriter(LogAllFile, true);
        logging.WriteLine(message);
        Console.WriteLine(message);
    }

    // Provides logging functionality for writing messages to daily log files and console output
    // Use this method for messages for everything that isn't using WriteLog that you don't want to see in the console
    public static void LogAll(string mesage)
    {
        using var logging = new StreamWriter(LogAllFile, true);
        logging.WriteLine(mesage);
    }
    
}