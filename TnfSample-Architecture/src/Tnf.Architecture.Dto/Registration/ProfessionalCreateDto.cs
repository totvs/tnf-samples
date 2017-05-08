using System.Collections.Generic;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Dto.Registration
{
    public class ProfessionalCreateDto
    {
        public decimal ProfessionalId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<SpecialtyDto> Specialties { get; set; } = new List<SpecialtyDto>();
    }
}
