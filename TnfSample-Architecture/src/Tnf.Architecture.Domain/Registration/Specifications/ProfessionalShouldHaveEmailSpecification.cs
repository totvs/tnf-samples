using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Domain.Registration.Specifications
{
    internal class ProfessionalShouldHaveEmailSpecification : Specification<Professional>
    {
        public override string LocalizationSource => AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey => Professional.Error.ProfessionalEmailMustHaveValue;

        public override Expression<Func<Professional, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Email);
        }
    }
}
