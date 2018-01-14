using Case3.Infra1.Services;
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

        [HttpGet]
        public async Task<IActionResult> Post(string message)
        {
            await _customerService.Notify(message);

            return Ok();
        }
    }
}
