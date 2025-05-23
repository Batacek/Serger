namespace SRG_Core;

public class Log : Main
{
    private static readonly string LogFile = $"{DateTime.Now:dd_MM_yyyy}.log";
    private static readonly string LogAllFile = $"{DateTime.Now:dd_MM_yyyy}_ALL.log";

    public static void WriteLog(string message)
    {
        using (StreamWriter logging = new StreamWriter(LogFile, true))
        {
            logging.WriteLine(message);
        }
    }

    public static void PrintLog(string message)
    {
        using (StreamWriter logging = new StreamWriter(LogAllFile, true))
        {
            logging.WriteLine(message);
            Console.WriteLine(message);
        }
    }

    public static void LogAll(string mesage)
    {
        using (StreamWriter logging = new StreamWriter(LogAllFile, true))
        {
            logging.WriteLine(mesage);
        }
    }
}