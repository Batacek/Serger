using System.Threading.Tasks;

namespace SRG;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static async Task Main()
    {
        Thread coreThread = new Thread (() =>
        {
            SergerCore.Run().GetAwaiter().GetResult();
        });
        Thread updateThread = new Thread (() =>
        {
            while (true)
            {
                Thread.Sleep(1000);
            }
        });
        coreThread.Start();
        Application.Run(new MainMenu());
        coreThread.Join();
    }
}