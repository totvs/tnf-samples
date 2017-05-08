using System;
using Tnf.Domain.Entities;

namespace Tnf.Architecture.EntityFrameworkCore.Entities
{
    public class ProfessionalSpecialtiesPoco : Entity
    {
        public decimal ProfessionalId { get; set; }
        public Guid Code { get; set; }
        public virtual ProfessionalPoco Professional { get; set; }
        public int SpecialtyId { get; set; }
        public virtual SpecialtyPoco Specialty { get; set; }
    }
}
