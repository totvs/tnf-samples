using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Common;

namespace Tnf.Architecture.Domain.Registration.Specifications
{
    internal class SpecialtyShouldHaveDescriptionSpecification : Specification<Specialty>
    {
        public override string LocalizationSource { get; protected set; } = AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Specialty.Error.SpecialtyDescriptionMustHaveValue;

        public override Expression<Func<Specialty, bool>> ToExpression()
        {
            return (p) => !p.Description.IsNullOrWhiteSpace();
        }
    }
}
