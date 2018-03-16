using System.Threading.Tasks;
using Tnf.Dependency;

namespace Messaging.Infra1.Services
{
    public interface INotifierService : ITransientDependency
    {
        Task Notify(string message);
    }
}