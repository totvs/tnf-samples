using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Dto.Registration
{
    public class ProfessionalCreateDto
    {
        public decimal ProfessionalId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public ZipCode ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
