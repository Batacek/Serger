namespace Serger.SRG_Core;

public class Log
{
    private static readonly string LogFile = $"{DateTime.Now:dd_MM_yyyy}.log";
    private static readonly string LogAllFile = $"{DateTime.Now:dd_MM_yyyy}_ALL.log";

    public static void WriteLog(string message)     // Provides logging functionality for writing messages to daily log files without console output
    {                                               // Use this method for error messages and important information only
        using (StreamWriter logging = new StreamWriter(LogFile, true))
        {
            logging.WriteLine(message);
        }
    }

    public static void PrintLog(string message)     // Provides logging functionality for writing messages to daily log files and console output
    {                                               // Use this method for messages for everything that isn't using WriteLog that you want to see in the console as well
        using (StreamWriter logging = new StreamWriter(LogAllFile, true))
        {
            logging.WriteLine(message);
            Console.WriteLine(message);
        }
    }

    public static void LogAll(string mesage)        // Provides logging functionality for writing messages to daily log files and console output
    {                                               // Use this method for messages for everything that isn't using WriteLog that you don't want to see in the console
        using (StreamWriter logging = new StreamWriter(LogAllFile, true))
        {
            logging.WriteLine(mesage);
        }
    }
}