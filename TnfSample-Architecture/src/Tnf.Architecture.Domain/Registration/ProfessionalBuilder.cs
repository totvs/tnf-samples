using System;
using Tnf.Architecture.Domain.Registration.Specifications;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Helpers;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Builder;
using Tnf.Localization;

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

        public ProfessionalBuilder WithName(string name)
        {
            Instance.Name = name;
            return this;
        }

        public ProfessionalBuilder GenerateProfessionalCode()
        {
            Instance.Code = Guid.NewGuid();
            return this;
        }

        public ProfessionalBuilder WithAddress(string address, string number, string complement, ZipCode zipCode)
        {
            Instance.Address = TextHelper.ToTitleCase(address);
            Instance.AddressNumber = number;
            Instance.AddressComplement = TextHelper.ToTitleCase(complement);
            Instance.ZipCode = zipCode;
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

        private void AddNotification(Professional.Error error)
        {
            var notificationMessage = LocalizationHelper.GetString(AppConsts.LocalizationSourceName,error);
            Response.AddNotification(error, notificationMessage);
        }

        public override BuilderResponse<Professional> Build()
        {
            var shouldHaveName = new ProfessionalShouldHaveNameSpecification();
            var shouldHaveAddress = new ProfessionalShouldHaveAddressSpecification();
            var shouldHaveAddressNumber = new ProfessionalShouldHaveAddressNumberSpecification();
            var shouldHaveAddressComplement = new ProfessionalShouldHaveAddressComplementSpecification();
            var shouldHaveZipCode = new ProfessionalShouldHaveZipCodeSpecification();
            var shouldHavePhone = new ProfessionalShouldHavePhoneSpecification();
            var shouldHaveEmail = new ProfessionalShouldHaveEmailSpecification();

            if (!shouldHaveName.IsSatisfiedBy(Instance))
                AddNotification(Professional.Error.ProfessionalNameMustHaveValue);

            if (!shouldHaveAddress.IsSatisfiedBy(Instance))
                AddNotification(Professional.Error.ProfessionalAddressMustHaveValue);

            if (!shouldHaveAddressNumber.IsSatisfiedBy(Instance))
                AddNotification(Professional.Error.ProfessionalAddressNumberMustHaveValue);

            if (!shouldHaveAddressComplement.IsSatisfiedBy(Instance))
                AddNotification(Professional.Error.ProfessionalAddressComplementMustHaveValue);

            if (!shouldHaveZipCode.IsSatisfiedBy(Instance))
                AddNotification(Professional.Error.ProfessionalZipCodeMustHaveValue);

            if (!shouldHaveZipCode.IsSatisfiedBy(Instance))
                AddNotification(Professional.Error.ProfessionalZipCodeMustHaveValue);

            if (!shouldHavePhone.IsSatisfiedBy(Instance))
                AddNotification(Professional.Error.ProfessionalPhoneMustHaveValue);

            if (!shouldHaveEmail.IsSatisfiedBy(Instance))
                AddNotification(Professional.Error.ProfessionalEmailMustHaveValue);

            return base.Build();
        }
    }
}
