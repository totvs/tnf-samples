using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace SuperMarket.Backoffice.FiscalService.Domain.Entities.Specifications
{
    public class TaxMovimentMustHaveOrderId : Specification<TaxMoviment>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = TaxMoviment.Error.TaxMovimentMustHaveOrderId;

        public override Expression<Func<TaxMoviment, bool>> ToExpression()
        {
            return (o) => o.PurchaseOrderId != Guid.Empty;
        }
    }
}
