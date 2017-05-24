using System.Collections.Generic;
using Tnf.Domain.Entities;

namespace Tnf.Architecture.EntityFrameworkCore.Entities
{
    public class SpecialtyPoco : Entity
    {
        public string Description { get; set; }
        public virtual List<ProfessionalSpecialtiesPoco> ProfessionalSpecialties { get; set; }
    }
}
