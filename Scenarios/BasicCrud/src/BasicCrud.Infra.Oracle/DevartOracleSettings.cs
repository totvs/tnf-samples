using Devart.Data.Oracle.Entity.Configuration;

namespace BasicCrud.Infra.Oracle
{
    public static class DevartOracleSettings
    {
        public static void SetDefaultSettings()
        {
            OracleEntityProviderConfig.Instance.Workarounds.IgnoreDboSchemaName = true;
            OracleEntityProviderConfig.Instance.Workarounds.IgnoreSchemaName = true;
            OracleEntityProviderConfig.Instance.Workarounds.DisableQuoting = true;
            OracleEntityProviderConfig.Instance.DmlOptions.ReuseParameters = true;
            OracleEntityProviderConfig.Instance.DmlOptions.ParametersAsLiterals = true;
            OracleEntityProviderConfig.Instance.QueryOptions.UseCSharpNullComparisonBehavior = true;

            //https://www.devart.com/dotconnect/oracle/docs/DBScriptGeneration.html
            OracleEntityProviderConfig.Instance.DatabaseScript.Schema.DeleteDatabaseBehaviour = DeleteDatabaseBehaviour.AllSchemaObjects;
        }
    }
}
