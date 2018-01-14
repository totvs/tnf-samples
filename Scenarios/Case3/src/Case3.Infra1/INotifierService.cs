using System.Threading.Tasks;
using Tnf.Dependency;

namespace Case3.Infra1.Services
{
    public interface INotifierService : ITransientDependency
    {
        Task Notify(string message);
    }
}