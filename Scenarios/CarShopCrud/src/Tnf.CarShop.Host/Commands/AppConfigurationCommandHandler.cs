using CarShop.Commands;
using Dap.Api.ChangeAudit;
using Dap.Api.Events;
using Dap.Domain;
using Dap.Domain.Events;
using Dap.Domain.Repositories;

using Tnf.Commands;

using RacConfiguration = Dap.Domain.RacConfiguration;

namespace Tnf.CarShop.Host.Commands;

public class AppConfigurationCommandHandler : CommandHandler<AppConfigurationCommand>
{
    private readonly IAppConfigurationRepository _appConfigurationRepository;
    private readonly IEventPublisher _eventPublisher;

    public AppConfigurationCommandHandler(
        IAppConfigurationRepository appConfigurationRepository,
        IEventPublisher eventPublisher)
    {
        _appConfigurationRepository = appConfigurationRepository;
        _eventPublisher = eventPublisher;
    }

    public override async Task ExecuteAsync(AppConfigurationCommand command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(command?.AppCode))
            return;

        var appCode = command.AppCode;

        var appConfiguration = await _appConfigurationRepository.GetAsync(appCode, cancellationToken);
        if (appConfiguration == null)
        {
            appConfiguration = BuildAppConfiguration(command);
            await _appConfigurationRepository.InsertAppConfigurationAsync(appConfiguration, cancellationToken);

            var createEvent = new AppConfigurationCreated(appConfiguration);
            await _eventPublisher.PublishAsync(createEvent, cancellationToken);
        }
        else
        {
            var reportBuilder = new EntityChangeReportBuilder<AppConfiguration>(appConfiguration);

            appConfiguration.IsEnabled = command.IsEnabled;
            appConfiguration.CompletedCallback = command.CompletedCallBack;
            appConfiguration.StatusCallback = command.StatusCallBack;
            appConfiguration.RacConfiguration.GenerateSecret = command.GenerateSecret;
            appConfiguration.RacConfiguration.SendCredentialsEmail = command.SendCredentialsEmail;

            UpdateCarolConfiguration(appConfiguration, command);

            await _appConfigurationRepository.UpdateAppConfigurationAsync(appConfiguration, cancellationToken);

            reportBuilder.Update(appConfiguration);

            var changeEvent = new AppConfigurationChanged(appConfiguration.Id, reportBuilder.Build());
            await _eventPublisher.PublishAsync(changeEvent, cancellationToken);
        }
    }

    private static void UpdateCarolConfiguration(AppConfiguration appConfiguration, AppConfigurationCommand command)
    {
        if (command.UseCarol)
        {
            if (!appConfiguration.UseCarol())
            {
                appConfiguration.SetCarolConfiguration(new CarolConfiguration());
            }

            var carolConfiguration = appConfiguration.CarolConfiguration;

            if (command.CarolConfiguration.UseErpIntegration && command.CarolConfiguration.UserDefinedErp)
            {
                carolConfiguration.Connector = null;
            }
            else
            {
                carolConfiguration.Connector ??= new CarolConnector();
                carolConfiguration.Connector.Name = command.CarolConfiguration.ConnectorName;
                carolConfiguration.Connector.Group = command.CarolConfiguration.ConnectorGroup;
                carolConfiguration.Connector.SelectionMode = command.CarolConfiguration.GetConnectorSelectionMode();
            }

            carolConfiguration.UserDefinedErp = command.CarolConfiguration.UserDefinedErp;

            foreach (var app in command.CarolConfiguration.Apps)
            {
                carolConfiguration.TryAddApp(app);
            }

            foreach (var app in carolConfiguration.Apps)
            {
                if (!command.CarolConfiguration.HasApp(app.AppName))
                {
                    carolConfiguration.RemoveApp(app);
                }
            }
        }
        else if (appConfiguration.UseCarol())
        {
            appConfiguration.SetCarolConfiguration(null);
        }
    }

    private static AppConfiguration BuildAppConfiguration(AppConfigurationCommand command)
    {
        var appConfiguration = new AppConfiguration(command.AppCode, command.CompletedCallBack, command.StatusCallBack, command.IsEnabled);

        BuildCarolConfiguration(command, appConfiguration);
        BuildRacConfiguration(command.GenerateSecret, command.SendCredentialsEmail, appConfiguration);

        return appConfiguration;
    }

    private static void BuildCarolConfiguration(AppConfigurationCommand command, AppConfiguration appConfiguration)
    {
        if (command.UseCarol)
        {
            var carolConfigurationDto = command.CarolConfiguration;

            var carolConfiguration = new CarolConfiguration
            {
                UserDefinedErp = carolConfigurationDto.UserDefinedErp
            };

            if (!command.CarolConfiguration.UseErpIntegration || !command.CarolConfiguration.UserDefinedErp)
            {
                carolConfiguration.Connector = new CarolConnector()
                {
                    Name = carolConfigurationDto.ConnectorName,
                    Group = carolConfigurationDto.ConnectorGroup,
                    SelectionMode = carolConfigurationDto.GetConnectorSelectionMode()
                };
            }

            appConfiguration.SetCarolConfiguration(carolConfiguration);

            foreach (var appName in carolConfigurationDto.Apps)
            {
                appConfiguration.CarolConfiguration.TryAddApp(appName);
            }
        }
    }

    private static void BuildRacConfiguration(bool generateSecret, bool sendCredentialsEmail, AppConfiguration appConfiguration)
    {
        appConfiguration.SetRacConfiguration(new RacConfiguration
        {
            GenerateSecret = generateSecret,
            SendCredentialsEmail = sendCredentialsEmail
        });
    }
}
