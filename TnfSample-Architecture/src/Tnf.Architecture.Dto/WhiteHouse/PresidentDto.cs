using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Dto.WhiteHouse
{
    public class PresidentDto
    {
        public PresidentDto()
        {
        }

        public PresidentDto(string id, string name, Address address)
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