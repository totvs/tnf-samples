using Devart.Data.Oracle.Entity.Configuration;

namespace Security.Infra.Oracle
{
    public static class DevartOracleSettings
    {
        public static void SetDefaultSettings()
        {
            OracleEntityProviderConfig.Instance.Workarounds.DisableQuoting = true;
            OracleEntityProviderConfig.Instance.DmlOptions.ReuseParameters = true;
            OracleEntityProviderConfig.Instance.DmlOptions.ParametersAsLiterals = true;
            OracleEntityProviderConfig.Instance.QueryOptions.UseCSharpNullComparisonBehavior = true;
        }
    }
}
