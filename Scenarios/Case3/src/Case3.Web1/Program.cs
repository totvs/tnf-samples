using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Tnf;

namespace Case2.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.SetMinimumLevel(LogLevel.Trace);
                    logging.AddConsole();
                    logging.AddDebug();

                    logging.AddFilter("Microsoft", LogLevel.Warning);
                    logging.AddFilter("System", LogLevel.Warning);
                    logging.AddFilter("Engine", LogLevel.Warning);

                    logging.AddFilter(LoggingEvents.Localization.ToString(), (level) => true);
                    logging.AddFilter(LoggingEvents.Settings.ToString(), (level) => true);
                    logging.AddFilter(LoggingEvents.Tenant.ToString(), (level) => true);
                })
                .UseStartup<Startup>()
                .Build();
    }
}
