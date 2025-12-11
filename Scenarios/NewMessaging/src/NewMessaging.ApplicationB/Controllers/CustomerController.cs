using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewMessaging.ApplicationB.Models;
using Tnf.Messaging;

namespace NewMessaging.ApplicationB.Controllers
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
        public async Task<IActionResult> Post([FromBody] ResolveNewCustomerModel resolveNewCustomerModel)
        {
            if (_cache.TryGetValue<NewCustomerModel>(resolveNewCustomerModel.TransactionId.ToString(), out var cacheEntry))
            {
                cacheEntry.CompanyCode = resolveNewCustomerModel.CompanyCode;

                var message = new CloudEvent<NewCustomerModel>
                {
                    TransactionId = resolveNewCustomerModel.TransactionId.ToString(),
                    Data = cacheEntry
                };

                await _messageSender.SendAsync(message);

                _cache.Remove(resolveNewCustomerModel.TransactionId.ToString());
            }

            return CreateResponseOnPost(cacheEntry);
        }
    }
}
