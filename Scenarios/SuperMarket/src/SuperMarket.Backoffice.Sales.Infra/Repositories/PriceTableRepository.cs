using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuperMarket.Backoffice.Sales.Domain;
using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tnf.Notifications;
using Tnf.Settings;

namespace SuperMarket.Backoffice.Sales.Infra.Repositories
{
    public class PriceTableRepository : IPriceTableRepository
    {
        private readonly ILogger<PriceTableRepository> _logger;
        private readonly INotificationHandler _notificationHandler;
        public readonly IApplicationSettingManager _applicationSettingManager;
        private readonly static HttpClient HttpClient = new HttpClient();

        public PriceTableRepository(
            ILogger<PriceTableRepository> logger,
            INotificationHandler notificationHandler,
            IApplicationSettingManager applicationSettingManager)
        {
            _logger = logger;
            _notificationHandler = notificationHandler;
            _applicationSettingManager = applicationSettingManager;
        }

        public async Task<PriceTable> GetPriceTableAsync()
        {
            var url = await _applicationSettingManager.GetSettingValueForApplicationAsync(Constants.PriceTableUriService);

            try
            {
                using (var request = await HttpClient.GetAsync(url))
                {
                    var jsonContent = await request.Content.ReadAsStringAsync();
                    var rawPriceTable = JsonConvert.DeserializeObject<Dictionary<Guid, decimal>>(jsonContent);

                    return PriceTable.CreateTable(rawPriceTable);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unavailable Price table service");
            }

            _notificationHandler.DefaultBuilder
                .AsError()
                .WithMessage(Constants.LocalizationSourceName, PriceTable.Error.UnavailablePriceTableService)
                .Throw();

            return PriceTable.Empty();
        }
    }
}
