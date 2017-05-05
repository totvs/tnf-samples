using System;

namespace Tnf.Architecture.Dto.Registration
{
    public class ProfessionalKeysDto
    {
        public ProfessionalKeysDto(decimal professionalId, Guid code)
        {
            ProfessionalId = professionalId;
            Code = code;
        }

        public ProfessionalKeysDto()
        {
        }

        public decimal ProfessionalId { get; set; }
        public Guid Code { get; set; }
    }
}
