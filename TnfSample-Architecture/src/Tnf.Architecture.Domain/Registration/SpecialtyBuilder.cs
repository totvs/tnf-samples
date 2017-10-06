using Tnf.App.Builder;
using Tnf.App.Bus.Notifications;
using Tnf.Architecture.Domain.Registration.Specifications;

namespace Tnf.Architecture.Domain.Registration
{
    public class SpecialtyBuilder : Builder<Specialty>
    {
        public SpecialtyBuilder(INotificationHandler notification)
            : base(notification)
        {
        }

        public SpecialtyBuilder(INotificationHandler notification, Specialty instance)
            : base(notification, instance)
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
    }
}
