using Tnf.Builder;
using Tnf.Architecture.Domain.WhiteHouse.Specifications;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Dto;
using Tnf.Dto.Interfaces;

namespace Tnf.Architecture.Domain.WhiteHouse
{
    internal class PresidentBuilder : Builder<President>
    {
        public PresidentBuilder()
            : base()
        {
        }

        public PresidentBuilder(President instance)
            : base(instance)
        {
        }

        public PresidentBuilder WithInvalidPresident()
        {
            AddEnum(AppConsts.LocalizationSourceName, President.Error.InvalidPresident);
            return this;
        }

        public PresidentBuilder WithId(string id)
        {
            Instance.Id = id;
            return this;
        }

        public PresidentBuilder WithName(string name)
        {
            Instance.Name = name;
            return this;
        }

        public PresidentBuilder WithAddress(Address address)
        {
            Instance.Address = address;
            return this;
        }

        public PresidentBuilder WithAddress(string street, string number, string complement, string zipCode)
        {
            Instance.Address = new Address(street, number, complement, new ZipCode(zipCode));
            return this;
        }

        public PresidentBuilder WithAddress(string street, string number, string complement, ZipCode zipCode)
        {
            Instance.Address = new Address(street, number, complement, zipCode);
            return this;
        }

        public override IResponseDto Build()
        {
            var shouldHaveName = new PresidentShouldHaveNameSpecification();
            var shouldHaveAddress = new PresidentShouldHaveAddressSpecification();
            var shouldHaveAddressNumber = new PresidentShouldHaveAddressNumberSpecification();
            var shouldHaveAddressComplement = new PresidentShouldHaveAddressComplementSpecification();
            var shouldHaveZipCode = new PresidentShouldHaveZipCodeSpecification();

            if (!shouldHaveName.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, President.Error.PresidentNameMustHaveValue);

            if (!shouldHaveAddress.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, President.Error.PresidentAddressMustHaveValue);

            if (!shouldHaveAddressNumber.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, President.Error.PresidentAddressNumberMustHaveValue);

            if (!shouldHaveAddressComplement.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, President.Error.PresidentAddressComplementMustHaveValue);

            if (!shouldHaveZipCode.IsSatisfiedBy(Instance))
                AddNotification(AppConsts.LocalizationSourceName, President.Error.PresidentZipCodeMustHaveValue);

            return base.Build();
        }
    }
}