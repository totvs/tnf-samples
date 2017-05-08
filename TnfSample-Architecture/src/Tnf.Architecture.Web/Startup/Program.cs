using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Tnf.Architecture.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls("http://ec2-35-165-157-186.us-west-2.compute.amazonaws.com:5000",
                         "http://localhost:5050")
                .Build();

            host.Run();
        }
    }
}
