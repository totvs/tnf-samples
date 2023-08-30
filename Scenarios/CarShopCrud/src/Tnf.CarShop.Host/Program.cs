using Serilog;
using Tnf.CarShop.Application.DependencyInjection;
using Tnf.CarShop.Application.Localization;
using Tnf.CarShop.EntityFrameworkCore.PostgreSql;
using Tnf.CarShop.Host.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Configure logs
builder.Host.UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCarShopApiVersioning();

builder.Services.AddTnfAspNetCore(tnf =>
{
    tnf.DefaultConnectionString(builder.Configuration.GetConnectionString("PostgreSql"));

    tnf.MultiTenancy(multiTenancy => { multiTenancy.IsEnabled = true; });
});

builder.Services.AddTnfAspNetCoreSecurity(builder.Configuration);

//builder.Services.AddTnfCommands(commands => { commands.AddCommandHandlersFromAssembly(typeof(Program).Assembly); });
builder.Services.AddCommands();

builder.Services.AddEFCorePostgreSql();

builder.Services.ConfigureCarShopLocalization();

builder.Services.AddMessaging(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseCarShopApiVersioning();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapTnfHealthChecks();

app.Run();
