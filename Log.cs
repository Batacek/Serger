namespace Serger;

public static class Log
{
    private static readonly string LogFile = $"{DateTime.Now:dd_MM_yyyy}.log";

    public static void Write(string message)
    {
        using (StreamWriter logging = new StreamWriter(LogFile, true))
        {
            logging.WriteLine(message);
        }
    }
}