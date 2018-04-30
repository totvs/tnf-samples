using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using Tnf.Security.Rac.Configuration;
using System.Linq;
using System.IO;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class SwaggerExtensions
    {
        public static SwaggerGenOptions AddSwaagerRacSecurity(this SwaggerGenOptions swaggerGenOptions, TnfRacOptions options)
        {
            swaggerGenOptions.SwaggerDoc("v1", new Info { Title = "Security API", Version = "v1" });
            swaggerGenOptions.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Security.Web.xml"));

            swaggerGenOptions.AddSecurityDefinition("oauth2", new OAuth2Scheme
            {
                Type = "oauth2",
                Flow = "implicit",
                AuthorizationUrl = string.Concat(options.AuthorityEndpoint, "/connect/authorize"),
                TokenUrl = string.Concat(options.AuthorityEndpoint, "/connect/token"),
                Scopes = options.Scope.Split(" ").ToDictionary(k => k, v => v)
            });

            swaggerGenOptions.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
            {
                { "oauth2", new string[] { "openid", "profile", "email" } }
            });

            return swaggerGenOptions;
        }

        public static SwaggerUIOptions UseSwaggerRacSecurity(this SwaggerUIOptions swaggerUIOptions, TnfRacOptions options)
        {
            swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Security API v1");

            swaggerUIOptions.OAuthAppName(options.ApiName);
            swaggerUIOptions.OAuthClientId(options.ApiName);
            swaggerUIOptions.OAuthClientSecret(options.ClientSecret);

            // Para informar qual o tenant que irá logar
            swaggerUIOptions.OAuthAdditionalQueryStringParams(new { acr_values = "tenant:poa" });

            return swaggerUIOptions;
        }
    }
}
