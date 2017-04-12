using System;
using System.Linq.Expressions;
using Tnf.Sample.Dto.ValueObjects;
using Tnf.Specifications;

namespace Tnf.Sample.Core.WhiteHouse.Specifications
{
    public class PresidentShouldHaveZipCodeSpecification : Specification<President>
    {
        public override Expression<Func<President, bool>> ToExpression()
        {
            return (p) => 
                p.ZipCode != null && 
                !string.IsNullOrWhiteSpace(p.ZipCode.Number) &&
                p.ZipCode.Number.Length != ZipCode.Length;
        }
    }
}
