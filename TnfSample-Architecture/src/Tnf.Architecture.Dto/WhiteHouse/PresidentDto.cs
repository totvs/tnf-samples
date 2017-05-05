using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Dto.WhiteHouse
{
    public class PresidentDto
    {
        public PresidentDto()
        {
        }

        public PresidentDto(string id, string name, string zipCode)
        {
            Id = id;
            Name = name;
            ZipCode = new ZipCode(zipCode);
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ZipCode ZipCode { get; set; }
    }
}