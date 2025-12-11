using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using NewMessaging.ApplicationA.Models;
using Tnf.Messaging;

namespace NewMessaging.ApplicationA.Consumers
{
    public class NewCustomerResolvedConsumer : IConsumer<CloudEvent<NewCustomerModel>>
    {
        private readonly IMemoryCache _cache;

        public NewCustomerResolvedConsumer(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task ConsumeAsync(IConsumeContext<CloudEvent<NewCustomerModel>> context, CancellationToken cancellationToken = default)
        {
            _cache.Set(context.Message.TransactionId, context.Message.Data, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }
    }
}
