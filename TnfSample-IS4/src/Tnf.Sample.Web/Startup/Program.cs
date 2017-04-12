using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Tnf.Sample.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls(Constants.Authority, Constants.SampleApi)
                //.UseUrls(Constants.Authority)
                //.UseUrls(Constants.SampleApi)
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
