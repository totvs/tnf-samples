using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Domain.WhiteHouse.Specifications
{
    internal class PresidentShouldHaveNameSpecification : Specification<President>
    {
        public override string LocalizationSource => AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey => President.Error.PresidentNameMustHaveValue;

        public override Expression<Func<President, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Name);
        }
    }
}
