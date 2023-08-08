using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tnf.CarShop.Host.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));

        options.OperationFilter<SwaggerVersioningOperationFilter>();

        var securityDefinition = BuildSecurityDefinition();
        options.AddSecurityDefinition(securityDefinition.Name, securityDefinition.OpenApiSecurityScheme);

        options.AddSecurityRequirement(BuildSecurityRequirement());
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "CarShop Web API Sample",
            Version = description.ApiVersion.ToString()
        };

        if (description.IsDeprecated) info.Description += " This API version has been deprecated.";

        return info;
    }

    private SecurityDefinition BuildSecurityDefinition()
    {
        const string Name = "bearer";
        var openApiSecurityScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Scheme = "bearer"
        };

        return new SecurityDefinition(Name, openApiSecurityScheme);
    }

    private OpenApiSecurityRequirement BuildSecurityRequirement()
    {
        var scheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
        };

        return new OpenApiSecurityRequirement
        {
            [scheme] = new List<string>()
        };
    }
}