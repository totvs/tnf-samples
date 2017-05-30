using System;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Domain.Registration
{
    public class Professional
    {
        public decimal ProfessionalId { get; internal set; }
        public string Name { get; internal set; }
        public Guid Code { get; internal set; }
        public Address Address { get; internal set; }
        public string Phone { get; internal set; }
        public string Email { get; internal set; }

        public enum Error
        {
            InvalidProfessional = 0,
            Unexpected = 1,
            InvalidId = 2,
            ProfessionalNameMustHaveValue = 3,
            ProfessionalZipCodeMustHaveValue = 4,
            ProfessionalAddressMustHaveValue = 5,
            ProfessionalAddressComplementMustHaveValue = 6,
            ProfessionalAddressNumberMustHaveValue = 7,
            ProfessionalEmailMustHaveValue = 8,
            ProfessionalPhoneMustHaveValue = 9,
            CouldNotFindProfessional = 10
        }
    }
}
