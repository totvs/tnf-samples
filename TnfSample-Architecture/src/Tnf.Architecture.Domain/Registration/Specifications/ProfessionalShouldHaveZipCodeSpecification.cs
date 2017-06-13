using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Domain.Registration.Specifications
{
    internal class ProfessionalShouldHaveZipCodeSpecification : Specification<Professional>
    {
        public override string LocalizationSource => AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey => Professional.Error.ProfessionalZipCodeMustHaveValue;

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
