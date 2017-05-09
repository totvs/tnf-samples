using Tnf.Architecture.Domain.Registration.Specifications;
using Tnf.Architecture.Dto;
using Tnf.Builder;
using Tnf.Localization;

namespace Tnf.Architecture.Domain.Registration
{
    internal class SpecialtyBuilder : Builder<Specialty>
    {
        public SpecialtyBuilder()
            : base()
        {
        }

        public SpecialtyBuilder(Specialty instance)
            : base(instance)
        {
        }

        public SpecialtyBuilder WithId(int id)
        {
            Instance.Id = id;
            return this;
        }

        public SpecialtyBuilder WithDescription(string description)
        {
            Instance.Description = description;
            return this;
        }

        private void AddNotification(Specialty.Error error)
        {
            var notificationMessage = LocalizationHelper.GetString(AppConsts.LocalizationSourceName, error);
            Response.AddNotification(error, notificationMessage);
        }

        public override BuilderResponse<Specialty> Build()
        {
            var shouldHaveDescription = new SpecialtyShouldHaveDescriptionSpecification();

            if (!shouldHaveDescription.IsSatisfiedBy(Instance))
                AddNotification(Specialty.Error.SpecialtyDescriptionMustHaveValue);

            return base.Build();
        }
    }
}
