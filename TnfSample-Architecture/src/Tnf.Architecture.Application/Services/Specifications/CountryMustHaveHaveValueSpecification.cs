using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Common;
using Tnf.Architecture.Dto.Registration;
using Tnf.Extensions;

namespace Tnf.Architecture.Application.Services.Specifications
{
    public class CountryMustHaveHaveValueSpecification : Specification<CountryDto>
    {
        public override Expression<Func<CountryDto, bool>> ToExpression()
        {
            return (c) => !c.Name.IsNullOrEmpty();
        }

        public override string LocalizationSource { get; protected set; } = AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = CountryDto.Error.CountryNameMustHaveValue;
    }
}
