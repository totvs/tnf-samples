using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace SuperMarket.Backoffice.FiscalService.Domain.Entities.Specifications
{
    public class TaxMovimentMustHaveOrderDate : Specification<TaxMoviment>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = TaxMoviment.Error.TaxMovimentMustHaveOrderNumber;

        public override Expression<Func<TaxMoviment, bool>> ToExpression()
        {
            return (o) => o.OrderDate > new DateTime(17531, 1, 1);
        }
    }
}
