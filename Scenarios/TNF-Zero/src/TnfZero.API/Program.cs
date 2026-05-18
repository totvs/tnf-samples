using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Tnf.Repositories;
using TnfZero.Application.Commands.CreateDog;
using TnfZero.EntityFramework;
using TnfZero.EntityFramework.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// ── Npgsql legacy timestamp behavior ─────────────────────────────────────────
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// ── Serilog ───────────────────────────────────────────────────────────────────
builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/tnfzero-.log", rollingInterval: RollingInterval.Day);
});

// ── TNF core ──────────────────────────────────────────────────────────────────
builder.Services.AddTnfAspNetCore(b =>
{
    b.ApplicationName("TnfZero");
    b.DefaultConnectionString(builder.Configuration.GetConnectionString("Default"));
    b.MultiTenancy(c => c.IsEnabled = true);
});

// ── Fluig Identity security ──────────────────────────────────────────────────
// Registers JWT bearer authentication backed by the [FluigIdentity] appsettings section.
builder.Services.AddFluigIdentitySecurity(builder.Configuration);

// ── Persistence ───────────────────────────────────────────────────────────────
builder.Services.AddTnfDbContext<TnfZeroDbContext, PostgreSqlTnfZeroDbContext>(c =>
{
    c.DbContextOptions.UseNpgsql(c.ConnectionString);
});

// ── Repositories ──────────────────────────────────────────────────────────────
var repositoryTypes = typeof(TnfZeroDbContext).Assembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>)))
    .ToList();

foreach (var implType in repositoryTypes)
{
    var interfaceType = implType.GetInterfaces()
        .FirstOrDefault(i => i.Namespace?.StartsWith("TnfZero.Domain.Repositories") == true);

    if (interfaceType != null) builder.Services.AddScoped(interfaceType, implType);
}

// ── CQRS ──────────────────────────────────────────────────────────────────────
builder.Services.AddTnfCommands(commands =>
{
    // Scans the Application assembly for all CommandHandler<,> implementations
    commands.AddCommandHandlersFromAssembly(typeof(CreateDogCommandHandler).Assembly);
});

// ── MVC / Swagger ─────────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TnfZero API", Version = "v1" });

    // JWT bearer security – adds the "Authorize" button to Swagger UI
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Description = "Enter your Fluig Identity JWT bearer token."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        [new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
        }] = new List<string>()
    });
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// ── Serilog request logging ───────────────────────────────────────────────────
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

// Fluig Identity security middleware – must follow UseRouting/UseAuthorization
app.UseTnfAspNetCoreSecurity();

app.MapControllers();
app.MapTnfHealthChecks();
app.Run();

// Make the implicit Program class accessible for WebApplicationFactory<T> in integration tests
public partial class Program
{
}