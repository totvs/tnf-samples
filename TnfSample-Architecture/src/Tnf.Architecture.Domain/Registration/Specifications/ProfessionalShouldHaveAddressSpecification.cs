using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Domain.Registration.Specifications
{
    internal class ProfessionalShouldHaveAddressSpecification : Specification<Professional>
    {
        public override string LocalizationSource => AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey => Professional.Error.ProfessionalAddressMustHaveValue;

        public override Expression<Func<Professional, bool>> ToExpression()
        {
            return (p) => p.Address != null && !string.IsNullOrWhiteSpace(p.Address.Street);
        }
    }
}
