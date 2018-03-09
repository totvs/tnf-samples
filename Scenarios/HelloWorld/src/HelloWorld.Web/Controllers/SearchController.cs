using Microsoft.AspNetCore.Mvc;
using HelloWorld.Application;
using HelloWorld.SharedKernel.ValueObjects;
using System.Threading.Tasks;

namespace HelloWorld.Web.Controllers
{
    [Route("api/search")]
    public class SearchController : TnfController
    {
        private readonly ISearchAppService searchAppService;

        public SearchController(ISearchAppService searchAppService)
        {
            this.searchAppService = searchAppService;
        }

        [HttpGet("{zipCode}")]
        public async Task<IActionResult> Get(string zipCode)
        {
            var response = await searchAppService.SearchLocationFromZipCode(new ZipCode(zipCode));

            return CreateResponseOnGet(response);
        }

        [HttpGet("{state}/{city}/{street}")]
        public async Task<IActionResult> SearchLocation(string state, string city, string street)
        {
            var response = await searchAppService.SearchLocation(state, city, street);

            return CreateResponseOnGet(response);
        }
    }
}
