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
                p.Address != null &&
                p.Address.ZipCode != null &&
                !string.IsNullOrWhiteSpace(p.Address.ZipCode.Number) &&
                p.Address.ZipCode.Number.Length == ZipCode.Length;
        }
    }
}
