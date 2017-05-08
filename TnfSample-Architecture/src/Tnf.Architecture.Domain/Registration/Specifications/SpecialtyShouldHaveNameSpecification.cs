using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace Tnf.Architecture.Domain.Registration.Specifications
{
    internal class SpecialtyShouldHaveNameSpecification : Specification<Specialty>
    {
        public override Expression<Func<Specialty, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Description);
        }
    }
}
