using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Dependency;

namespace Messaging.Infra2
{
    public interface IMessageStoreService : ITransientDependency
    {
        Task<List<string>> GetAllMessages();
        Task Flush();
    }
}