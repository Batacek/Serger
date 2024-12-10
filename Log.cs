namespace Serger;

public static class Log
{
    private static string logFile = $"{DateTime.Now:dd_MM_yyyy}.log";

    public static void Write(string message)
    {
        using (StreamWriter logging = new StreamWriter(logFile, true))
        {
            logging.WriteLine(message);
        }
    }
}