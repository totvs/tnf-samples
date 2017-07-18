using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Domain.Registration.Specifications
{
    internal class ProfessionalShouldHavePhoneSpecification : Specification<Professional>
    {
        public override string LocalizationSource { get; protected set; } = AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Professional.Error.ProfessionalPhoneMustHaveValue;

        public override Expression<Func<Professional, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Phone);
        }
    }
}
