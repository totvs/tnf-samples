using System;
using System.Linq.Expressions;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Specifications;

namespace Tnf.Architecture.Domain.WhiteHouse.Specifications
{
    internal class PresidentShouldHaveAddressSpecification : Specification<President>
    {
        public override Expression<Func<President, bool>> ToExpression()
        {
            return (p) => p.Address != null && !string.IsNullOrWhiteSpace(p.Address.Street);
        }
    }
}
