using Tnf.App.Dto.Response;

namespace Tnf.Architecture.Dto.Registration
{
    public class PersonDto : DtoBase
    {
        public string Name { get; set; }

        public enum Error
        {
            GetAllPeople = 1,
            GetPerson = 2,
            PostPerson = 3,
            PutPerson = 4,
            DeletePerson = 5
        }
    }
}
