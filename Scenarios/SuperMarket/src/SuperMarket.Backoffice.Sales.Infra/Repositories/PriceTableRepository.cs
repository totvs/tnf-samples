using Newtonsoft.Json;
using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tnf.Settings;

namespace SuperMarket.Backoffice.Sales.Infra.Repositories
{
    public class PriceTableRepository : IPriceTableRepository
    {
        public readonly IApplicationSettingManager _applicationSettingManager;
        private readonly static HttpClient HttpClient = new HttpClient();

        public PriceTableRepository(IApplicationSettingManager applicationSettingManager)
        {
            _applicationSettingManager = applicationSettingManager;
        }

        public async Task<PriceTable> GetPriceTableAsync()
        {
            var url = await _applicationSettingManager.GetSettingValueForApplicationAsync("CrudApiUrl");

            using (var request = await HttpClient.GetAsync($"{url}api/products/pricetable"))
            {
                var jsonContent = await request.Content.ReadAsStringAsync();
                var rawPriceTable = JsonConvert.DeserializeObject<Dictionary<Guid, decimal>>(jsonContent);

                return PriceTable.CreateTable(rawPriceTable);
            }
        }
    }
}
