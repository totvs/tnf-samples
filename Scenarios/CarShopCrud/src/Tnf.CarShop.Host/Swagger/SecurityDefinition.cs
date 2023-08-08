using Microsoft.OpenApi.Models;

namespace Dap.Api.Swagger;

internal sealed record SecurityDefinition
{
    public string Name { get; }
    public OpenApiSecurityScheme OpenApiSecurityScheme { get; }

    public SecurityDefinition(string name, OpenApiSecurityScheme openApiSecurityScheme)
    {
        Name = name;
        OpenApiSecurityScheme = openApiSecurityScheme;
    }
}
