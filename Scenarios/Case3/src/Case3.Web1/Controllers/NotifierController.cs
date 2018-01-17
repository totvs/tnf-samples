using Case3.Infra1.Services;
using Case3.Web1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Case2.Web.Controllers
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
        public async Task<IActionResult> Post([FromBody]NotificationMessage content)
        {
            await _customerService.Notify(content?.Message);

            return CreateResponseOnPost();
        }
    }
}
