using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tnf.SmartX.Sample.Swagger;

public static class VersioningExtensions
{
    public static IServiceCollection AddSmartXApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);

                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";

                options.SubstituteApiVersionInUrl = true;
            });
        ;

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }

    public static IApplicationBuilder UseOptInApiVersioning(this IApplicationBuilder app)
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "smartx/swagger/{documentname}/swagger.json";
        });

        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "smartx/swagger";

            var apiVersionDescriptionProvider =
                app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            // Inclui para cada versão a documentação
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}
