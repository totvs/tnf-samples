﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Tnf.SmartX.EntityFramework.Migrator;

using Serilog;
using Tnf.SmartX.EntityFramework.PostgreSql;

const string CodeFirstConnectionStringName = "CodeFirst";

var builder = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration(configuration =>
    {
        configuration.AddJsonFile("appsettings.Development.json", false);
        configuration.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.ConfigureTnf(tnf =>
        {
            tnf.DefaultConnectionString(context.Configuration.GetConnectionString(CodeFirstConnectionStringName));
        });

        services.AddEFCorePostgreSql(context.Configuration);

        services.AddHostedService<MigrationHostedService>();
    })
    .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

await builder.Build()
    .RunAsync();
