using System.Net.NetworkInformation;

namespace SRG;

public class Pinger : SergerCore
{
    public static string status = "Null";
    public static async Task PingAddr()
    {
        try
        {
            PingReply reply = new Ping().Send(Config.URL); // Ping
            Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.ffff")}"); // Date and time
            if (reply.Status == IPStatus.Success) // if condition for success case
            {
                /* Printing the info into console window
                Console.WriteLine($"{LangDictionary.PingText} {Config.URL} {LangDictionary.Success}.");
                Console.WriteLine($"{LangDictionary.Address}: {reply.Address}");
                Console.WriteLine($"{LangDictionary.PingTime}: {reply.RoundtripTime} ms");
                Console.WriteLine($"TTL: {reply.Options.Ttl}\n");
                */

                // Printing the info into .log file
                Log.Write($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")}");
                Log.Write($"{LangDictionary.PingText} {Config.URL} {LangDictionary.Success}.");
                Log.Write($"{LangDictionary.Address} {reply.Address}");
                Log.Write($"{LangDictionary.PingTime} {reply.RoundtripTime} ms");
                Log.Write($"TTL: {reply.Options.Ttl}\n");
                
                status = "Success";
            }
            else
            {
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                Console.WriteLine($"{LangDictionary.PingText} {Config.URL} {LangDictionary.Fail}: {reply.Status}");
                
                status = "Fail";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"{LangDictionary.PingText} {Config.URL} {LangDictionary.Fail}. \n{LangDictionary.Error}: {ex.Message}"); // Printout the error message
            Console.WriteLine();
        }

        await Task.Delay(Config.PING_DELAY); // Pause between pings
    }   
}