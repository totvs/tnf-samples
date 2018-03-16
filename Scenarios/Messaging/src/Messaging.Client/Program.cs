using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Messaging.Client
{
    class Program
    {
        static IConfigurationRoot Configuration;

        static HttpClient Client = new HttpClient();

        static IServiceProvider ConfigureServices()
        {
            // Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithMachineName()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            var services = new ServiceCollection()
                .AddLogging(config => config.AddSerilog(Log.Logger));

            return services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .Build();

            var provider = ConfigureServices();

            var logger = provider.GetRequiredService<ILogger<Program>>();

            int numberForAttempts = 50;
            int start = 0;
            int end = numberForAttempts;

            ConsoleKey command = ConsoleKey.N;

            while (command != ConsoleKey.Q)
            {
                logger.LogInformation($"Press 'q' to exit or other key to continue...");

                var consoleKeyInfo = Console.ReadKey();

                command = consoleKeyInfo.Key;

                var messagesToSent = CreateMessages(start, end);

                //messagesToSent.ForEach(message =>
                //{
                //    try
                //    {
                //        var serializedMessage = JsonConvert.SerializeObject(message);
                //        var content = new StringContent(serializedMessage, Encoding.UTF8, "application/json");

                //        var response = Client.PostAsync("http://localhost:5001/api/notifier", content).GetAwaiter().GetResult();

                //        response.EnsureSuccessStatusCode();

                //        logger.LogInformation($"{message.Message} sent");
                //    }
                //    catch (Exception ex)
                //    {
                //        logger.LogError(ex, $"Error to sent {message.Message}");
                //    }
                //});

                ExecuteSequenceAsync(messagesToSent, async (message) =>
                {
                    try
                    {
                        var serializedMessage = JsonConvert.SerializeObject(message);
                        var content = new StringContent(serializedMessage, Encoding.UTF8, "application/json");

                        var response = await Client.PostAsync("http://localhost:5001/api/notifier", content);

                        response.EnsureSuccessStatusCode();

                        logger.LogInformation($"{message.Message} sent");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error to sent {message.Message}");
                    }
                }).GetAwaiter().GetResult();

                start += numberForAttempts;
                end += numberForAttempts;
            }
        }

        private static List<MessageRequest> CreateMessages(int start, int end)
        {
            var messagesToSent = new List<MessageRequest>();

            for (int i = start + 1; i <= end; i++)
            {
                messagesToSent.Add(new MessageRequest($"Message {i}"));
            }

            return messagesToSent;
        }

        public static Task ExecuteSequenceAsync<T>(IEnumerable<T> sequence, Func<T, Task> action)
        {
            return Task.WhenAll(sequence.Select(action));
        }
    }
}
