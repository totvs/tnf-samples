using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Dto.Registration
{
    public class ProfessionalDto : IDto<ProfessionalKeysDto>
    {
        public IList<string> _expandables { get; set; }

        public ProfessionalDto()
        {
            _expandables = new List<string>() { "professionalSpecialties.specialty" };
        }

        public decimal ProfessionalId { get; set; }
        public Guid Code { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<SpecialtyDto> Specialties { get; set; } = new List<SpecialtyDto>();

        [JsonIgnore]
        public ProfessionalKeysDto Id { get; set; }

        public enum Error
        {
            GetAllProfessional = 1,
            GetProfessional = 2,
            PostProfessional = 3,
            PutProfessional = 4,
            DeleteProfessional = 5
        }
    }
}
