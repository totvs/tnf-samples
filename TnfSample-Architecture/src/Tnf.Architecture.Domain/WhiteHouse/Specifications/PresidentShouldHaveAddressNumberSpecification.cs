using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Dto;

namespace Tnf.Architecture.Domain.WhiteHouse.Specifications
{
    internal class PresidentShouldHaveAddressNumberSpecification : Specification<President>
    {
        public override string LocalizationSource => AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey => President.Error.PresidentAddressNumberMustHaveValue;

        public override Expression<Func<President, bool>> ToExpression()
        {
            return (p) => p.Address != null && !string.IsNullOrWhiteSpace(p.Address.Number);
        }
    }
}
