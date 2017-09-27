using System;
using System.Collections.Generic;
using Tnf.App.Builder;
using Tnf.App.Bus.Notifications.Interfaces;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Registration.Specifications;

namespace Tnf.Architecture.Domain.Registration
{
    public class ProfessionalBuilder : Builder<Professional>
    {
        public ProfessionalBuilder(INotificationHandler notification)
            : base(notification)
        {
        }

        public ProfessionalBuilder(INotificationHandler notification, Professional instance)
            : base(notification, instance)
        {
        }

        public ProfessionalBuilder WithIds(ComposeKey<Guid, decimal> keys)
        {
            Instance.Code = keys.PrimaryKey;
            Instance.ProfessionalId = keys.SecundaryKey;
            return this;
        }

        public ProfessionalBuilder WithProfessionalId(decimal id)
        {
            Instance.ProfessionalId = id;
            return this;
        }

        public ProfessionalBuilder WithCode(Guid code)
        {
            Instance.Code = code;
            return this;
        }

        public ProfessionalBuilder WithName(string name)
        {
            Instance.Name = name;
            return this;
        }

        public ProfessionalBuilder WithAddress(Address address)
        {
            Instance.Address = address;
            return this;
        }

        public ProfessionalBuilder WithAddress(string street, string number, string complement, string zipCode)
        {
            Instance.Address = new Address(street, number, complement, new ZipCode(zipCode));
            return this;
        }

        public ProfessionalBuilder WithAddress(string street, string number, string complement, ZipCode zipCode)
        {
            Instance.Address = new Address(street, number, complement, zipCode);
            return this;
        }

        public ProfessionalBuilder WithPhone(string phone)
        {
            Instance.Phone = phone;
            return this;
        }

        public ProfessionalBuilder WithEmail(string email)
        {
            Instance.Email = email;
            return this;
        }

        public ProfessionalBuilder WithSpecialties(IList<Specialty> specialties)
        {
            Instance.Specialties = specialties;
            return this;
        }

        protected override void Specifications()
        {
            AddSpecification(new ProfessionalShouldHaveNameSpecification());
            AddSpecification(new ProfessionalShouldHaveAddressSpecification());
            AddSpecification(new ProfessionalShouldHaveAddressNumberSpecification());
            AddSpecification(new ProfessionalShouldHaveAddressComplementSpecification());
            AddSpecification(new ProfessionalShouldHaveZipCodeSpecification());
            AddSpecification(new ProfessionalShouldHavePhoneSpecification());
            AddSpecification(new ProfessionalShouldHaveEmailSpecification());
        }
    }
}
