namespace Microsoft.Extensions.Configuration
{
    public static class IConfigurationExtensions
    {
        public static bool TryGetExternalAuthorityEndpoint(this IConfiguration configuration, out string value)
        {
            var extermalAuthorityEndpoint = configuration["ExternalAuthorityEndpoint"];

            if (!string.IsNullOrWhiteSpace(extermalAuthorityEndpoint))
            {
                value = extermalAuthorityEndpoint;
                return true;
            }

            value = default(string);
            return false;
        }
    }
}
