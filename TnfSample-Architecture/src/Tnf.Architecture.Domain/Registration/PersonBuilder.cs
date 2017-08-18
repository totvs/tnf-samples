using Tnf.App.Builder;
using Tnf.App.Bus.Notifications.Interfaces;
using Tnf.Architecture.Domain.Registration.Specifications;

namespace Tnf.Architecture.Domain.Registration
{
    public class PersonBuilder : Builder<Person>
    {
        public PersonBuilder(INotificationHandler notification)
            : base(notification)
        {
        }

        public PersonBuilder(Person instance, INotificationHandler notification)
            : base(instance, notification)
        {
        }

        public PersonBuilder WithId(int id)
        {
            Instance.Id = id;
            return this;
        }

        public PersonBuilder WithName(string name)
        {
            Instance.Name = name;
            return this;
        }

        protected override void Specifications()
        {
            AddSpecification(new PersonShouldHaveNameSpecification());
        }
    }
}
