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
            Unexpected = 0,
            InvalidId = 1,
            PresidentNameMustHaveValue = 2,
            PresidentZipCodeMustHaveValue = 3,
            PresidentAddressMustHaveValue = 4,
            PresidentAddressComplementMustHaveValue = 5,
            PresidentAddressNumberMustHaveValue = 6,
            CouldNotFindPresident = 7
        }
    }
}
