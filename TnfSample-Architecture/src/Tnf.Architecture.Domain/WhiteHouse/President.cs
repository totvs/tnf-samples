using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.AutoMapper;
using Tnf.Domain.Entities;

namespace Tnf.Architecture.Domain.WhiteHouse
{
    [AutoMap(typeof(PresidentDto))]
    internal class President : AggregateRoot<string>
    {
        public const int MaxNameLength = 256;

        public string Name { get; internal set; }
        public ZipCode ZipCode { get; internal set; }

        public enum Error
        {
            Unexpected = 0,
            InvalidId = 1,
            PresidentNameMustHaveValue = 2,
            PresidentZipCodeMustHaveValue = 3,
            CouldNotDeletePresident = 4
        }
    }
}
