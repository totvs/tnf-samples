using Microsoft.AspNetCore.Hosting;
using System.IO;

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
                .UseStartup<Startup>();

#if DEBUG
            host.UseUrls("http://10.51.4.36:1010");
#endif

            host.Build().Run();
        }
    }
}
