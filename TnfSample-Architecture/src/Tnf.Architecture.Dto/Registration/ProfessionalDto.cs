using System;
using System.Collections.Generic;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Dto.Response;

namespace Tnf.Architecture.Dto.Registration
{
    public class ProfessionalDto : SuccessResponseDto
    {
        public ProfessionalDto()
            :base(new List<string>() { "professionalSpecialties.specialty" })
        {

        }

        public decimal ProfessionalId { get; set; }
        public Guid Code { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<SpecialtyDto> Specialties { get; set; } = new List<SpecialtyDto>();
    }
}
