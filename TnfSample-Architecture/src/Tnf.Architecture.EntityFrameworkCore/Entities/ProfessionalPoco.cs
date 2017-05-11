using System;
using System.Collections.Generic;
using Tnf.Architecture.Dto.Registration;
using Tnf.AutoMapper;
using Tnf.Domain.Entities;

namespace Tnf.Architecture.EntityFrameworkCore.Entities
{
    [AutoMap(typeof(ProfessionalDto))]
    public class ProfessionalPoco : Entity
    {
        public ProfessionalPoco()
        {
        }

        public decimal ProfessionalId { get; set; }
        public string Name { get; set; }
        public Guid Code { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<ProfessionalSpecialtiesPoco> ProfessionalSpecialties { get; set; }
    }
}
