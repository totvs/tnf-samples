using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dap.Api;

internal static class VersioningExtensions
{
    public static IServiceCollection AddDapApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            // UseApiBehavior está true para que somente os controller que utilizam o attributo ApiControllerAttribute
            // sejam considerados como API versionadas.
            options.UseApiBehavior = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);

            // Apenas versionamento pela Url habilitado.
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services.AddVersionedApiExplorer(options =>
        {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }

    public static IApplicationBuilder UseProvisioningApiVersioning(this IApplicationBuilder app)
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "dap/swagger/{documentname}/swagger.json";
        });

        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "dap/swagger";

            var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            // Inclui para cada versão a documentação
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}
