using System.Collections.Generic;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Dto.WhiteHouse
{
    public class PresidentDto : SuccessResponseDto
    {
        public PresidentDto()
            : base(new List<string>() { "address" })
        {
        }

        public PresidentDto(string id, string name, Address address)
            : this()
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}