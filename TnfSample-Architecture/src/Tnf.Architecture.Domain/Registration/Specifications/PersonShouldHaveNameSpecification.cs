using System;
using System.Linq.Expressions;
using Tnf.App.Specifications;
using Tnf.Architecture.Common;

namespace Tnf.Architecture.Domain.Registration.Specifications
{
    internal class PersonShouldHaveNameSpecification : Specification<Person>
    {
        public override string LocalizationSource { get; protected set; } = AppConsts.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Person.Error.PersonNameMustHaveValue;

        public override Expression<Func<Person, bool>> ToExpression()
        {
            return (p) => !p.Name.IsNullOrWhiteSpace();
        }
    }
}
