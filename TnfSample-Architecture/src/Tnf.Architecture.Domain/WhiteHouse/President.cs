using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Domain.Entities;

namespace Tnf.Architecture.Domain.WhiteHouse
{
    public class President : AggregateRoot<string>
    {
        public const int MaxNameLength = 256;

        public string Name { get; internal set; }
        public Address Address { get; internal set; }

        public enum Error
        {
            PresidentNameMustHaveValue = 1,
            PresidentZipCodeMustHaveValue = 2,
            PresidentAddressMustHaveValue = 3,
            PresidentAddressComplementMustHaveValue = 4,
            PresidentAddressNumberMustHaveValue = 5,
            CouldNotFindPresident = 6
        }
    }
}
