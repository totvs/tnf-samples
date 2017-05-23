using System;
using Tnf.Architecture.Domain.Registration.Specifications;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Builder;
using Tnf.Dto.Interfaces;

namespace Tnf.Architecture.Domain.Registration
{
    internal class ProfessionalBuilder : Builder<Professional>
    {
        public ProfessionalBuilder()
            : base()
        {
        }

        public ProfessionalBuilder(Professional instance)
            : base(instance)
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

        public override IResponseDto Build()
        {
            var shouldHaveName = new ProfessionalShouldHaveNameSpecification();
            var shouldHaveAddress = new ProfessionalShouldHaveAddressSpecification();
            var shouldHaveAddressNumber = new ProfessionalShouldHaveAddressNumberSpecification();
            var shouldHaveAddressComplement = new ProfessionalShouldHaveAddressComplementSpecification();
            var shouldHaveZipCode = new ProfessionalShouldHaveZipCodeSpecification();
            var shouldHavePhone = new ProfessionalShouldHavePhoneSpecification();
            var shouldHaveEmail = new ProfessionalShouldHaveEmailSpecification();

            if (!shouldHaveName.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, Professional.Error.ProfessionalNameMustHaveValue);

            if (!shouldHaveAddress.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, Professional.Error.ProfessionalAddressMustHaveValue);

            if (!shouldHaveAddressNumber.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, Professional.Error.ProfessionalAddressNumberMustHaveValue);

            if (!shouldHaveAddressComplement.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, Professional.Error.ProfessionalAddressComplementMustHaveValue);

            if (!shouldHaveZipCode.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, Professional.Error.ProfessionalZipCodeMustHaveValue);

            if (!shouldHavePhone.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, Professional.Error.ProfessionalPhoneMustHaveValue);

            if (!shouldHaveEmail.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, Professional.Error.ProfessionalEmailMustHaveValue);

            return base.Build();
        }
    }
}
