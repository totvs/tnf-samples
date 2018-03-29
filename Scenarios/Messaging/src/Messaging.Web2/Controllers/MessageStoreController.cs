using Messaging.Infra2;
using Messaging.Web2.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;

namespace Messaging.Web2.Controllers
{
    /// <summary>
    /// Message Store API
    /// </summary>
    [Route("api/store")]
    public class MessageStoreController : TnfController
    {
        private readonly IMessageStoreService _store;

        public MessageStoreController(IMessageStoreService store)
        {
            _store = store;
        }

        /// <summary>
        /// Get all messages stored
        /// </summary>
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

        /// <summary>
        /// Flush messages
        /// </summary>
        [HttpGet("flush")]
        public async Task<IActionResult> Flush()
        {
            await _store.Flush();

            return Ok();
        }
    }
}
