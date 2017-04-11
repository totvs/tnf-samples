using Tnf.Builder;
using Tnf.Architecture.Domain.WhiteHouse.Specifications;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.CrossCutting;
using Tnf.Localization;

namespace Tnf.Architecture.Domain.WhiteHouse
{
    public class PresidentBuilder : Builder<President>
    {
        public PresidentBuilder()
            : base()
        {
        }

        public PresidentBuilder(President instance)
            : base(instance)
        {
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

        public PresidentBuilder WithZipCode(ZipCode zipCode)
        {
            Instance.ZipCode = zipCode;
            return this;
        }

        public PresidentBuilder WithZipCode(string zipCode)
        {
            Instance.ZipCode = new ZipCode(zipCode);
            return this;
        }

        public override BuilderResponse<President> Build()
        {
            var shouldHaveName = new PresidentShouldHaveNameSpecification();
            var shouldHaveZipCode = new PresidentShouldHaveZipCodeSpecification();

            if (!shouldHaveName.IsSatisfiedBy(Instance))
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName, 
                    President.Error.PresidentNameMustHaveValue);

                Response.AddNotification(
                    President.Error.PresidentNameMustHaveValue, notificationMessage);
            }

            if (!shouldHaveZipCode.IsSatisfiedBy(Instance))
            {
                var notificationMessage = LocalizationHelper.GetString(
                    AppConsts.LocalizationSourceName,
                    President.Error.PresidentZipCodeMustHaveValue);

                Response.AddNotification(
                   President.Error.PresidentZipCodeMustHaveValue, notificationMessage);
            }

            return base.Build();
        }
    }
}