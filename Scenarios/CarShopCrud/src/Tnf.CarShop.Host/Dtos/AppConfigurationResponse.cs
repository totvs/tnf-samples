using System.Diagnostics.CodeAnalysis;
using Dap.Domain;
using Dap.Domain.Enums;

namespace Dap.Api.Dtos;

[ExcludeFromCodeCoverage]
public class AppConfigurationResponse
{
    public string AppCode { get; set; }
    public bool UseCarol { get; set; }
    public bool GenerateSecret { get; set; }
    public bool SendCredentialsEmail { get; set; }
    public CarolConfiguration CarolConfiguration { get; set; }
    public string CompletedCallBack { get; set; }
    public string StatusCallBack { get; set; }
    public bool IsEnabled { get; set; }

    public AppConfigurationResponse BuildAppConfigurationResponse(AppConfiguration appConfiguration)
    {
        AppCode = appConfiguration.AppCode;
        UseCarol = appConfiguration.CarolConfiguration != null;
        CompletedCallBack = appConfiguration.CompletedCallback;
        StatusCallBack = appConfiguration.StatusCallback;
        IsEnabled = appConfiguration.IsEnabled;

        if (appConfiguration.RacConfiguration != null)
        {
            GenerateSecret = appConfiguration.RacConfiguration.GenerateSecret;
            SendCredentialsEmail = appConfiguration.RacConfiguration.SendCredentialsEmail;
        }

        return this;
    }

    public AppConfigurationResponse BuildCarolConfiguration(AppConfiguration appConfiguration)
    {
        if (appConfiguration.CarolConfiguration != null)
        {
            CarolConfiguration = new CarolConfiguration
            {
                UseErpIntegration = IsErpIntegration(appConfiguration.CarolConfiguration),
                ConnectorName = appConfiguration.CarolConfiguration.Connector?.Name,
                ConnectorGroup = appConfiguration.CarolConfiguration.Connector?.Group,
                UserDefinedErp = appConfiguration.CarolConfiguration.UserDefinedErp
            };

            var carolConfigurationApps = appConfiguration.CarolConfiguration.Apps.OrderBy(x => x.AppName);
            foreach (var carolConfigurationApp in carolConfigurationApps)
            {
                var appName = carolConfigurationApp.AppName;
                CarolConfiguration.Apps.Add(appName);
            }
        }

        return this;
    }

    private bool IsErpIntegration(Domain.CarolConfiguration configuration)
    {
        return configuration.UserDefinedErp && configuration.Connector is null ||
            configuration.Connector is not null && configuration.Connector.SelectionMode == ConnectorSelectionMode.ErpInferred;
    }
}
