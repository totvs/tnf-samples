using Tnf.Builder;
using Tnf.Sample.Core.WhiteHouse.Specifications;
using Tnf.Sample.Dto.ValueObjects;

namespace Tnf.Sample.Core.WhiteHouse
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
                Response.AddNotificationFormatted(
                    President.Error.NameMustHaveValue,
                    nameof(President.Name));
            }

            if (!shouldHaveZipCode.IsSatisfiedBy(Instance))
            {
                Response.AddNotificationFormatted(
                    President.Error.ZipCodeMustHaveValue,
                    nameof(President.ZipCode));
            }

            return base.Build();
        }
    }
}
