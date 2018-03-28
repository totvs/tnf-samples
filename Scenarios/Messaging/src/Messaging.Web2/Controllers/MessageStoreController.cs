using Messaging.Infra2;
using Messaging.Web2.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;

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
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get()
        {
            var response = await _store.GetAllMessages();

            return Json(new ResponseDto
            {
                Total = response?.Count,
                Messages = response
            });
        }

        [HttpGet("refresh")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Refresh()
        {
            await _store.Refresh();

            return Ok();
        }
    }
}
