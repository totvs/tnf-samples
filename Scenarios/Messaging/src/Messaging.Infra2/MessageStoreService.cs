using Messaging.Infra2.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Bus.Client;
using Tnf.Bus.Queue.Interfaces;
using Tnf.Caching;

namespace Messaging.Infra2
{
    public class MessageStoreService :
        IMessageStoreService,
        ISubscribe<NotificationMessage>
    {
        private const string MESSAGE_STORE_CACHE_KEY = "MessageStoreValues";

        private readonly ICache _cache;

        public MessageStoreService(ICache cache)
        {
            _cache = cache;
        }

        public async Task<List<string>> GetAllMessages()
        {
            var valuesInCache = await _cache.GetAsync<List<string>>(MESSAGE_STORE_CACHE_KEY);
            return valuesInCache;
        }

        public async Task Handle(NotificationMessage message)
        {
            var valuesInCache = await _cache.GetAsync<List<string>>(MESSAGE_STORE_CACHE_KEY);
            if (valuesInCache == null)
                valuesInCache = new List<string>();

            valuesInCache.Add(message.Value);

            _cache.Add(MESSAGE_STORE_CACHE_KEY, valuesInCache, TimeSpan.FromMinutes(10));

            message.DoAck();
        }

        public Task Flush()
        {
            return _cache.DeleteKeyAsync(MESSAGE_STORE_CACHE_KEY);
        }
    }
}
