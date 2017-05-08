using System.Collections.Generic;
using Tnf.Architecture.Dto.Registration;
using Tnf.AutoMapper;
using Tnf.Domain.Entities;

namespace Tnf.Architecture.EntityFrameworkCore.Entities
{
    [AutoMap(typeof(SpecialtyDto))]
    public class SpecialtyPoco : Entity
    {
        public string Description { get; set; }

        public List<ProfessionalSpecialtiesPoco> ProfessionalSpecialties { get; set; }
    }
}
