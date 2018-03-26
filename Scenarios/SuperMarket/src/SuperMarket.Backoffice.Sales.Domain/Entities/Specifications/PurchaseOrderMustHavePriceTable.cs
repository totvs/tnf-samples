using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace SuperMarket.Backoffice.Sales.Domain.Entities.Specifications
{
    public class PurchaseOrderMustHavePriceTable : Specification<PurchaseOrder>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = PurchaseOrder.Error.PurchaseOrderMustHavePriceTable;

        public override Expression<Func<PurchaseOrder, bool>> ToExpression()
        {
            return (o) => !o.PriceTable.IsEmpty();
        }
    }
}