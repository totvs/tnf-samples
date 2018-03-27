using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace SuperMarket.FiscalService.Domain.Entities.Specifications
{
    public class TaxMovimentMustHaveOrderDiscount : Specification<TaxMoviment>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = TaxMoviment.Error.TaxMovimentMustHaveOrderDiscount;

        public override Expression<Func<TaxMoviment, bool>> ToExpression()
        {
            return (o) => o.PurchaseOrderDiscount >= 0;
        }
    }
}
