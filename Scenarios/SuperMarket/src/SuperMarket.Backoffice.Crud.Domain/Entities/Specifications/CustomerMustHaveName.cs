using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace SuperMarket.Backoffice.Crud.Domain.Entities.Specifications
{
    public class CustomerMustHaveName : Specification<Customer>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Customer.Error.CustomerMustHaveName;

        public override Expression<Func<Customer, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Name);
        }
    }
}
