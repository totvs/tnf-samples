using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace Tnf.Architecture.Domain.WhiteHouse.Specifications
{
    public class PresidentShouldHaveNameSpecification : Specification<President>
    {
        public override Expression<Func<President, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Name);
        }
    }
}
