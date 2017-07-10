using System;
using Tnf.App.Builder;
using Tnf.App.Bus.Notifications.Interfaces;
using Tnf.Architecture.Domain.Registration.Specifications;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Domain.Registration
{
    internal class ProfessionalBuilder : Builder<Professional>
    {
        public ProfessionalBuilder(INotificationHandler notification)
            : base(notification)
        {
        }

        public ProfessionalBuilder(Professional instance, INotificationHandler notification)
            : base(instance, notification)
        {
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

        public override Professional Build()
        {
            Validate();
            return base.Build();
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
