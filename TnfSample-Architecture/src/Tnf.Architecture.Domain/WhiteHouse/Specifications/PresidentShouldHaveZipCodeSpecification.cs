using System;
using System.Linq.Expressions;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Specifications;

namespace Tnf.Architecture.Domain.WhiteHouse.Specifications
{
    internal class PresidentShouldHaveZipCodeSpecification : Specification<President>
    {
        public override Expression<Func<President, bool>> ToExpression()
        {
            return (p) => 
                p.ZipCode != null && 
                !string.IsNullOrWhiteSpace(p.ZipCode.Number) &&
                p.ZipCode.Number.Length == ZipCode.Length;
        }
    }
}
