namespace SRG;

public class Log : SergerCore
{
    private static readonly string LogFile = $"{DateTime.Now:dd_MM_yyyy}.log";

    public static void Write(string message)
    {
        Console.WriteLine(message);
        using (StreamWriter logging = new StreamWriter(LogFile, true))
        {
            logging.WriteLine(message);
        }
    }
}