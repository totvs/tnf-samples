using Case3.Infra2;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Case2.Web.Controllers
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

            return Json(response);
        }
    }
}
