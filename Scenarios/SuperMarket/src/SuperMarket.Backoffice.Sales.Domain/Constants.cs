using System;

namespace SuperMarket.Backoffice.Sales.Domain
{
    public static class Constants
    {
        public const string ConnectionStringName = "Sales";
        public const string LocalizationSourceName = "Sales";

        public const string PriceTableUriService = "PriceTableUriService";

        private const string ENVIRONMENT_VARIABLE = "ASPNETCORE_ENVIRONMENT";
        private const string DEV_ENVIRONMENT_VARIABLE = "DEVELOPMENT";

        public static bool IsDevelopment()
        {
            var environmentName = Environment.GetEnvironmentVariable(ENVIRONMENT_VARIABLE);
            if (environmentName.ToUpperInvariant() == DEV_ENVIRONMENT_VARIABLE)
                return true;

            return false;
        }
    }
}
