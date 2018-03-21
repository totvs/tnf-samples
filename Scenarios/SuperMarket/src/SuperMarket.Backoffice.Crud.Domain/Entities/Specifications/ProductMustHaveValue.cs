using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace SuperMarket.Backoffice.Crud.Domain.Entities.Specifications
{
    public class ProductMustHaveValue : Specification<Product>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Product.Error.ProductMustHaveValue;

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return (p) => p.Value > 0;
        }
    }
}
