using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.App.AspNetCore;
using Tnf.App.Bus.Client;
using Tnf.App.Bus.Client.Configuration.Startup;
using Tnf.App.Bus.Queue;
using Tnf.App.Bus.Queue.Enums;
using Tnf.App.Bus.Queue.RabbitMQ;
using Tnf.App.Security.Identity;
using Tnf.Architecture.Application;
using Tnf.Architecture.Application.Commands;
using Tnf.Architecture.Application.Events;
using Tnf.Architecture.Common;
using Tnf.Architecture.Domain.Configuration;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Web.Startup
{
    [DependsOn(
        typeof(AppModule),
        typeof(TnfAppSecurityIdentityModule),
        typeof(TnfAppBusQueueModule), // <- Módulo para Fila
        typeof(TnfAppBusClientModule), // <- Módulo para manipulação da Fila
        typeof(TnfAppAspNetCoreModule))]
    public class WebModule : TnfModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public WebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(AppConsts.ConnectionStringName);

            #region Setup Mensageria via Builder - Veja documentação TDN

            // Cria um Tópico da mensagem SpecialtyCreateCommand
            var specialtyCreateCommandTopic = TopicSetup.Builder
                .Factory()
                .Message(typeof(SpecialtyCreateCommand))
                .AddKey("Specialty.Create.Command")
                .Build();

            // Cria um Tópico da mensagem SpecialtyCreatedEvent
            var specialtyCreatedEventTopic = TopicSetup.Builder
                .Factory()
                .Message(typeof(SpecialtyCreatedEvent))
                .AddKey("Specialty.Created.Event")
                .Build();

            // Cria uma Fila
            var queue = QueueSetup.Builder
               .Factory()
               .QueueName("General")
               .QueueReliabilitySetup(r => r
                   .AckIsMandatory(true)
                   .AutoDeleteQueue(true)
                   .MaxMessageSize(256)
                   .PersistMessage(false)
                   .Build())
               .AddTopics(specialtyCreateCommandTopic)
               .AddTopics(specialtyCreatedEventTopic)
               .Build();

            // Cria um Exchange Router
            var exchangeRouter = ExchangeRouter
                .Builder
                .Factory()
                .Name("ExchangeForTesting")
                .ServerAddress("127.0.0.1")
                .Type(ExchangeType.topic)
                .QueueChannel(QueueChannel.Amqp)
                .Reliability(isDurable: false, isAutoDelete: false, isPersistent: false)
                .AddQueue(queue)
                .SetExclusive(false)
                .Build();

            #endregion

            // Configura Módulo Mensageria
            Configuration.BusClientSetup()
                .SetIocManager(IocManager)
                .SetExchangeRouter("default", e => exchangeRouter)
                .AddPublishers(() => new PublisherListener(Configuration.BusClientSetup().GetExchangeRouterInstance("default")))
                .AddSubscribers(() => new SubscriberListener(Configuration.BusClientSetup().GetExchangeRouterInstance("default")))
                .Verify(this)
                .Run();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WebModule).GetAssembly());
        }
    }
}