using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace BasicCrud.Domain.Entities.Specifications
{
    public class CustomerShouldHaveNameSpecification : Specification<Customer>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Customer.Error.CustomerShouldHaveName;

        public override Expression<Func<Customer, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Name);
        }
    }
}
