using Tnf.App.Dto.Response;
using Tnf.Architecture.Common.ValueObjects;

namespace Tnf.Architecture.Dto.WhiteHouse
{
    public class PresidentDto : DtoBase<string>
    {
        public static PresidentDto NullInstance = new PresidentDto().AsNullable<PresidentDto, string>();

        public PresidentDto()
        {
        }

        public PresidentDto(string id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public string Name { get; set; }
        public Address Address { get; set; }

        public enum Error
        {
            GetAllPresident = 1,
            GetPresident = 2,
            PostPresident = 3,
            PutPresident = 4,
            DeletePresident = 5
        }
    }
}