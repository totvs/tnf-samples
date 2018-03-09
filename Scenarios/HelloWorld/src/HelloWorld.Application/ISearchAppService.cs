using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.SharedKernel.External;
using HelloWorld.SharedKernel.ValueObjects;

namespace HelloWorld.Application
{
    public interface ISearchAppService
    {
        Task<IEnumerable<SearchLocationResponse>> SearchLocation(string state, string city, string street);
        Task<SearchLocationResponse> SearchLocationFromZipCode(ZipCode zipCode);
    }
}