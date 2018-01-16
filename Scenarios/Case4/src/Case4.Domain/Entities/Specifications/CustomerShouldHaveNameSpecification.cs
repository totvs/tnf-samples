using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace Case4.Domain.Specifications
{
    public class CustomerShouldHaveNameSpecification : Specification<Customer>
    {
        public override string LocalizationSource { get; protected set; } = Case4Consts.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Customer.Error.CustomerShouldHaveName;

        public override Expression<Func<Customer, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Name);
        }
    }
}
