using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ── Serilog ───────────────────────────────────────────────────────────────────
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// ── TNF core ──────────────────────────────────────────────────────────────────
builder.Services.AddTnfAspNetCore(b =>
{
    b.ApplicationName("Job Scheduler Client Sample");
    b.MultiTenancy(c => c.IsEnabled = true);
});

// ── Security – Fluig Identity ─────────────────────────────────────────────────
builder.Services.AddFluigIdentitySecurity(builder.Configuration);

// ── TNF Jobs ──────────────────────────────────────────────────────────────────
builder.Services.AddTnfJobs(builder.Configuration);

// ── MVC / Swagger ─────────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Job Scheduler Client API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseTnfAspNetCoreSecurity();
app.MapControllers();
app.MapTnfHealthChecks();

await app.RunAsync();

Log.CloseAndFlush();
