using System;
using System.Collections.Generic;
using Tnf.Architecture.Common.ValueObjects;

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

        public IList<Specialty> Specialties { get; internal set; }

        public enum Error
        {
            ProfessionalNameMustHaveValue = 1,
            ProfessionalZipCodeMustHaveValue = 2,
            ProfessionalAddressMustHaveValue = 3,
            ProfessionalAddressComplementMustHaveValue = 4,
            ProfessionalAddressNumberMustHaveValue = 5,
            ProfessionalEmailMustHaveValue = 6,
            ProfessionalPhoneMustHaveValue = 7,
            CouldNotFindProfessional = 8
        }
    }
}
