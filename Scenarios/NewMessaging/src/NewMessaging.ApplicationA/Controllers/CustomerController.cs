using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewMessaging.ApplicationA.Models;
using Tnf.Messaging;

namespace NewMessaging.ApplicationA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : TnfController
    {
        private readonly IMemoryCache _cache;
        private readonly IMessageSender _messageSender;

        public CustomerController(IMemoryCache cache, IMessageSender messageSender)
        {
            _cache = cache;
            _messageSender = messageSender;
        }

        [HttpGet("{transactionId}")]
        [ProducesResponseType(typeof(NewCustomerModel), 200)]
        public Task<IActionResult> Get([FromRoute] Guid transactionId)
        {
            _ = _cache.TryGetValue<NewCustomerModel>(transactionId.ToString(), out var cacheEntry);

            return Task.FromResult(CreateResponseOnGet(cacheEntry));
        }

        [HttpPost]
        [ProducesResponseType(typeof(NewCustomerModel), 200)]
        public async Task<IActionResult> Post([FromBody] NewCustomerModel newCustomerModel)
        {
            _cache.Set(newCustomerModel.TransactionId.ToString(), newCustomerModel, TimeSpan.FromDays(1));

            var message = new CloudEvent<NewCustomerModel>
            {
                TransactionId = newCustomerModel.TransactionId.ToString(),
                Data = newCustomerModel
            };

            await _messageSender.SendAsync(message);

            return CreateResponseOnPost(newCustomerModel);
        }
    }
}
