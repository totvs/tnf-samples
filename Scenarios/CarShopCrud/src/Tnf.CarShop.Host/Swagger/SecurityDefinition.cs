using Microsoft.OpenApi.Models;

namespace Tnf.CarShop.Host.Swagger
{
    public class SecurityDefinition
    {
        public string Name { get; }
        public OpenApiSecurityScheme OpenApiSecurityScheme { get; }

        public SecurityDefinition(string name, OpenApiSecurityScheme openApiSecurityScheme)
        {
            Name = name;
            OpenApiSecurityScheme = openApiSecurityScheme;
        }
    }
}