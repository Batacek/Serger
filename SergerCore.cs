namespace SRG;

public class SergerCore
{
    private Config config;
    
    protected SergerCore()
    {
    }

    public static async Task Run()
    {
        while (true)
        {
            await Pinger.PingAddr();
        }
    }
}