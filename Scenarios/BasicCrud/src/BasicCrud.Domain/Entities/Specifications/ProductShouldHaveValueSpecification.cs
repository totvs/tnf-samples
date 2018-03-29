using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace BasicCrud.Domain.Entities.Specifications
{
    public class ProductShouldHaveValueSpecification : Specification<Product>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Product.Error.ProductShouldHaveValue;

        public override Expression<Func<Product, bool>> ToExpression()
        {
            return (p) => p.Value != 0;
        }
    }
}
