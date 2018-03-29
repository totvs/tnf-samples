using Messaging.Infra2;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get()
        {
            var response = await _store.GetAllMessages();

            return Json(new
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
