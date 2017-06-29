using System.Collections.Generic;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Dto.WhiteHouse
{
    public class PresidentDto : IDto<string>
    {
        public IList<string> _expandables { get; set; }

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