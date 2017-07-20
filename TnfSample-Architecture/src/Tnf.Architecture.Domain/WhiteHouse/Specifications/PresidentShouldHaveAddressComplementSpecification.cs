using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Common;

namespace Tnf.Architecture.Domain.WhiteHouse.Specifications
{
    internal class PresidentShouldHaveAddressComplementSpecification : Specification<President>
    {
        public override string LocalizationSource { get; protected set; } = AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = President.Error.PresidentAddressComplementMustHaveValue;

        public override Expression<Func<President, bool>> ToExpression()
        {
            return (p) => p.Address != null && !string.IsNullOrWhiteSpace(p.Address.Complement);
        }
    }
}
