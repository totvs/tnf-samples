using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace SuperMarket.Backoffice.Crud.Domain.Entities.Specifications
{
    public class ProductMustHaveDescription : Specification<Product>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Product.Error.ProductMustHaveDescription;

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrWhiteSpace(p.Description);
        }
    }
}
