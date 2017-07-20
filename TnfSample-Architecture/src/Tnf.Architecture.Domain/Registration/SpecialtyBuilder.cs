using Tnf.App.Builder;
using Tnf.App.Bus.Notifications.Interfaces;
using Tnf.Architecture.Domain.Registration.Specifications;

namespace Tnf.Architecture.Domain.Registration
{
    public class SpecialtyBuilder : Builder<Specialty>
    {
        public SpecialtyBuilder(INotificationHandler notification)
            : base(notification)
        {
        }

        public SpecialtyBuilder(Specialty instance, INotificationHandler notification)
            : base(instance, notification)
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

        protected override void Specifications()
        {
            AddSpecification(new SpecialtyShouldHaveDescriptionSpecification());
        }

        public override Specialty Build()
        {
            Validate();
            return base.Build();
        }
    }
}
