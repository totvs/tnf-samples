using Messaging.Infra1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Messaging.Web1.Controllers
{
    [Route("api/notifier")]
    public class NotifierController : TnfController
    {
        private readonly INotifierService _customerService;

        public NotifierController(INotifierService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MessageRequest content)
        {
            await _customerService.Notify(content?.Message);

            return CreateResponseOnPost();
        }
    }
}
