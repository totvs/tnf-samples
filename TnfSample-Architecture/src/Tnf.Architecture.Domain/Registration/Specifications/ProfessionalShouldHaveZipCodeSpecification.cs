using System;
using System.Linq.Expressions;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Specifications;

namespace Tnf.Architecture.Domain.Registration.Specifications
{
    internal class ProfessionalShouldHaveZipCodeSpecification : Specification<Professional>
    {
        public override Expression<Func<Professional, bool>> ToExpression()
        {
            return (p) =>
                p.ZipCode != null &&
                !string.IsNullOrWhiteSpace(p.ZipCode.Number) &&
                p.ZipCode.Number.Length == ZipCode.Length;
        }
    }
}
