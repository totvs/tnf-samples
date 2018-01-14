using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Dependency;

namespace Case3.Infra2
{
    public interface IMessageStoreService : ITransientDependency
    {
        Task<List<string>> GetAllMessages();
    }
}