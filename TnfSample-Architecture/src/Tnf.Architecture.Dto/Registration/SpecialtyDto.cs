using Tnf.App.Dto.Response;

namespace Tnf.Architecture.Dto.Registration
{
    public class SpecialtyDto : DtoBase
    {
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
