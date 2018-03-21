using System;
using System.Linq;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace SuperMarket.Backoffice.Sales.Domain.Entities.Specifications
{
    public class ProductsThatAreNotInThePriceTableSpecification : Specification<PurchaseOrder>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = PurchaseOrder.Error.ProductsThatAreNotInThePriceTable;

        public override Expression<Func<PurchaseOrder, bool>> ToExpression()
        {
            return (a) => a.GetProductsThatAreNotInThePriceTable().Count() == 0;
        }
    }
}
