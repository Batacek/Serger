namespace SRG_Core;

public class Program
{
    public static void Main(string[] args)
    {
        Config MainConfig = new Config();

        MainConfig.LoadConfig();
    }
}