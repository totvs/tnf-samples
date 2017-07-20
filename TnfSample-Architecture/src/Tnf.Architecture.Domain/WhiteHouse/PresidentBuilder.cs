using Tnf.App.Builder;
using Tnf.App.Bus.Notifications.Interfaces;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.WhiteHouse.Specifications;

namespace Tnf.Architecture.Domain.WhiteHouse
{
    public class PresidentBuilder : Builder<President>
    {
        public PresidentBuilder(INotificationHandler notification)
            : base(notification)
        {
        }

        public PresidentBuilder(President instance, INotificationHandler notification)
            : base(instance, notification)
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

        protected override void Specifications()
        {
            AddSpecification(new PresidentShouldHaveNameSpecification());
            AddSpecification(new PresidentShouldHaveAddressSpecification());
            AddSpecification(new PresidentShouldHaveAddressNumberSpecification());
            AddSpecification(new PresidentShouldHaveAddressComplementSpecification());
            AddSpecification(new PresidentShouldHaveZipCodeSpecification());
        }

        public override President Build()
        {
            Validate();
            return base.Build();
        }
    }
}