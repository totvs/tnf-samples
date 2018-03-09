using HelloWorld.SharedKernel.Conventionals;
using HelloWorld.SharedKernel.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloWorld.SharedKernel.External
{
    /// Essa classe irá ser registrada no DI por convenção! <see cref="KernelServiceCollectionExtensions"/>"
    public interface ILocationService : IRegisterTransientDependency
    {
        Task<SearchLocationResponse> Search(ZipCode zipCode);
        Task<IEnumerable<SearchLocationResponse>> Search(string state, string city, string street);
    }
}
