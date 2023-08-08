using System.Reflection.Emit;

namespace Dap.Api;

internal static class Roles
{
    public const string DapProductConfigurationFeatureFormat = "Dap.Configuration.{0}";
    public const string DapProductRoleAdmin = "Dap.Configuration.Admin";

    public static string GetProductFeature(string productKey)
        => string.Format(DapProductConfigurationFeatureFormat, productKey);
}
