using System.Collections.Generic;
using Tnf.App.Dto.Response;

namespace Tnf.Architecture.Dto.Registration
{
    public class SpecialtyDto : IDto
    {
        public IList<string> _expandables { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }

        public enum Error
        {
            GetAllSpecialty = 1,
            GetSpecialty = 2,
            PostSpecialty = 3,
            PutSpecialty = 4,
            DeleteSpecialty = 5
        }
    }
}
