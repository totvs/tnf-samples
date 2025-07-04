using Microsoft.EntityFrameworkCore;

using Tnf.SmartX;
using Tnf.SmartX.EntityFramework.PostgreSql;
using Tnf.SmartX.Sample.Swagger;

const string CodeFirstConnectionStringName = "CodeFirst";
const string DatabaseFirstConnectionStringName = "DatabaseFirst";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTnfAspNetCore(b =>
{
    b.ApplicationName("TNF SmartX Sample");
    b.DefaultConnectionString(builder.Configuration.GetConnectionString(CodeFirstConnectionStringName));
    b.MultiTenancy(c => c.IsEnabled = true);
});

builder.Services.AddFluigIdentitySecurity(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddCorsAll("AllowAll");

builder.Services.AddSmartXApiVersioning();

builder.Services.AddEFCorePostgreSql(builder.Configuration);

builder.Services.AddTnfSmartX(b =>
{
    b.ConfigureEfCodeFirst();
    b.ConfigureEfDatabaseFirst(configure => configure.UseNpgsql(builder.Configuration.GetConnectionString(DatabaseFirstConnectionStringName)));
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseTnfAspNetCoreSecurity();

app.MapTnfHealthChecks();

app.UseHttpsRedirection();

app.MapTnfSmartXControllers();

app.Run();
