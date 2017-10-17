using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Tnf.Architecture.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("https://*:5050")
                .Build();
    }
}
