using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Tnf.Architecture.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // When executing the command 'dotnet run' it will use this json to set the url
                .AddJsonFile("hosting.json", optional: true)
                // When passing the command 'dotnet run --urls "http://*:5052"' it will use this url
                .AddCommandLine(args)
                .Build();

            var host = WebHost.CreateDefaultBuilder(args)
             .UseConfiguration(config)
             .UseStartup<Startup>()
             .Build();

            host.Run();
        }
    }
}
