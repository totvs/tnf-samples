using Microsoft.OpenApi.Models;

namespace Tnf.SmartX.Sample.Swagger;

public class SecurityDefinition
{
    public SecurityDefinition(string name, OpenApiSecurityScheme openApiSecurityScheme)
    {
        Name = name;
        OpenApiSecurityScheme = openApiSecurityScheme;
    }

    public string Name { get; }
    public OpenApiSecurityScheme OpenApiSecurityScheme { get; }
}
