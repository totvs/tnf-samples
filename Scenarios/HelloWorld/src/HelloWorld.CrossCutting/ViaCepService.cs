using HelloWorld.SharedKernel;
using HelloWorld.SharedKernel.External;
using HelloWorld.SharedKernel.ValueObjects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tnf.Notifications;

namespace HelloWorld.CrossCutting
{
    /// <summary>
    /// https://viacep.com.br/
    /// </summary>
    public class ViaCepService : ILocationService
    {
        private static HttpClient Client = new HttpClient();

        private const string SearchByZipCodeEndpoint = @"https://viacep.com.br/ws/{0}/json/unicode/";
        private const string SearchByStreetEndpoint = @"https://viacep.com.br/ws/{0}/{1}/{2}/json/";

        private readonly ILogger<ViaCepService> logger;
        private readonly INotificationHandler notificationHandler;

        public ViaCepService(ILogger<ViaCepService> logger, INotificationHandler notificationHandler)
        {
            this.logger = logger;
            this.notificationHandler = notificationHandler;
        }

        public async Task<SearchLocationResponse> Search(ZipCode zipCode)
        {
            logger.LogInformation($"Search zipcode {zipCode}");

            SearchLocationResponse result = null;

            try
            {
                var response = await Client.GetAsync(string.Format(SearchByZipCodeEndpoint, zipCode.Number));

                logger.LogInformation($"Result search {response.StatusCode}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    notificationHandler
                        .DefaultBuilder
                        .WithNotFoundStatus()
                        .WithMessage(Constants.LocalizationSourceName, LocalizationServiceError.LocalizationInvalidZipCode)
                        .Raise();

                    return null;
                }

                var viaCepResponse = JsonConvert.DeserializeObject<ViaCepResponse>(await response.Content.ReadAsStringAsync());

                result = new SearchLocationResponse()
                {
                    City = viaCepResponse.localidade,
                    State = viaCepResponse.uf,
                    Street = viaCepResponse.logradouro,
                    ZipCode = new ZipCode(viaCepResponse.cep)
                };
            }
            catch (Exception ex)
            {
                notificationHandler
                   .DefaultBuilder
                   .FromException(ex, Constants.LocalizationSourceName, LocalizationServiceError.LocalizationServiceUnavailable)
                   .Raise();

                logger.LogError(ex, $"Error in get {SearchByZipCodeEndpoint}");
            }

            return result;
        }

        public async Task<IEnumerable<SearchLocationResponse>> Search(string state, string city, string street)
        {
            var result = Enumerable.Empty<SearchLocationResponse>();

            logger.LogInformation($"Search state {state}, city {city} and street {street}");

            try
            {
                var response = await Client.GetAsync(string.Format(SearchByStreetEndpoint, state, city, street));

                logger.LogInformation($"Result search {response.StatusCode}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    notificationHandler
                        .DefaultBuilder
                        .WithNotFoundStatus()
                        .WithMessage(Constants.LocalizationSourceName, LocalizationServiceError.LocalizationInvalidCityOrStreet)
                        .Raise();

                    return null;
                }

                var viaCepResponse = JsonConvert.DeserializeObject<List<ViaCepResponse>>(await response.Content.ReadAsStringAsync());

                result = viaCepResponse.Select(item => new SearchLocationResponse()
                {
                    City = item.localidade,
                    State = item.uf,
                    Street = item.logradouro,
                    ZipCode = new ZipCode(item.cep)
                });
            }
            catch (Exception ex)
            {
                notificationHandler
                   .DefaultBuilder
                   .FromException(ex, Constants.LocalizationSourceName, LocalizationServiceError.LocalizationServiceUnavailable)
                   .Raise();

                logger.LogError(ex, $"Error in get {SearchByZipCodeEndpoint}");
            }

            return result;
        }
    }
}
