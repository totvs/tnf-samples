using Tnf.Architecture.Domain.Registration.Specifications;
using Tnf.App.Builder;

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

        protected override void Specifications()
        {
            AddSpecification(new SpecialtyShouldHaveDescriptionSpecification());
        }

        public override Specialty Build()
        {
            base.Validate();
            return base.Build();
        }
    }
}
