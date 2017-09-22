using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Common;
using Tnf.Architecture.Common.ValueObjects;

namespace Tnf.Architecture.Domain.WhiteHouse.Specifications
{
    internal class PresidentShouldHaveZipCodeSpecification : Specification<President>
    {
        public override string LocalizationSource { get; protected set; } = AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = President.Error.PresidentZipCodeMustHaveValue;

        public override Expression<Func<President, bool>> ToExpression()
        {
            return (p) =>
                p.Address != null &&
                p.Address.ZipCode != null &&
                !p.Address.ZipCode.Number.IsNullOrWhiteSpace() &&
                p.Address.ZipCode.Number.Length == ZipCode.Length;
        }
    }
}
