using System;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Domain.Registration
{
    internal class Professional
    {
        public decimal ProfessionalId { get; internal set; }
        public string Name { get; internal set; }
        public Guid Code { get; internal set; }
        public Address Address { get; internal set; }
        public string Phone { get; internal set; }
        public string Email { get; internal set; }

        public enum Error
        {
            Unexpected = 0,
            InvalidId = 1,
            ProfessionalNameMustHaveValue = 2,
            ProfessionalZipCodeMustHaveValue = 3,
            ProfessionalAddressMustHaveValue = 4,
            ProfessionalAddressComplementMustHaveValue = 5,
            ProfessionalAddressNumberMustHaveValue = 6,
            ProfessionalEmailMustHaveValue = 7,
            ProfessionalPhoneMustHaveValue = 8
        }
    }
}
