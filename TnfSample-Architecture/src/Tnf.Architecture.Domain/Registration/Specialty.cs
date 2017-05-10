using Tnf.Domain.Entities;

namespace Tnf.Architecture.Domain.Registration
{
    internal class Specialty : Entity
    {
        public string Description { get; internal set; }

        public enum Error
        {
            Unexpected = 0,
            SpecialtyDescriptionMustHaveValue = 1,
            CouldNotFindSpecialty = 2
        }
    }
}
