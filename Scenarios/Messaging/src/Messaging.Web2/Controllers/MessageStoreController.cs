using Messaging.Infra2;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Messaging.Web2.Controllers
{
    [Route("api/store")]
    public class MessageStoreController : TnfController
    {
        private readonly IMessageStoreService _store;

        public MessageStoreController(IMessageStoreService store)
        {
            _store = store;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _store.GetAllMessages();

            return Json(new
            {
                Total = response?.Count,
                Messages = response
            });
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh()
        {
            await _store.Refresh();

            return Ok();
        }
    }
}
