using Tnf.Domain.Entities;

namespace Tnf.Architecture.Domain.Registration
{
    public class Specialty : Entity
    {
        public string Description { get; internal set; }

        public enum Error
        {
            InvalidSpecialty = 0,
            Unexpected = 1,
            SpecialtyDescriptionMustHaveValue = 2,
            CouldNotFindSpecialty = 3
        }
    }
}
