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
            InvalidPresident = 0,
            Unexpected = 1,
            InvalidId = 2,
            PresidentNameMustHaveValue = 3,
            PresidentZipCodeMustHaveValue = 4,
            PresidentAddressMustHaveValue = 5,
            PresidentAddressComplementMustHaveValue = 6,
            PresidentAddressNumberMustHaveValue = 7,
            CouldNotFindPresident = 8
        }
    }
}
