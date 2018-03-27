using SuperMarket.Backoffice.FiscalService.Domain.Entities.Specifications;
using System;
using Tnf.Builder;
using Tnf.Notifications;

namespace SuperMarket.Backoffice.FiscalService.Domain.Entities
{
    public partial class TaxMoviment
    {
        public class Builder : Builder<TaxMoviment>
        {
            public Builder(INotificationHandler notificationHandler)
                : base(notificationHandler)
            {
            }

            public Builder(INotificationHandler notificationHandler, TaxMoviment instance)
                : base(notificationHandler, instance)
            {
            }

            public Builder ForPurchaseOrder(Guid purchaseOrderId, decimal baseValue, decimal discount)
            {
                Instance.PurchaseOrderId = purchaseOrderId;
                Instance.PurchaseOrderBaseValue = baseValue;
                Instance.PurchaseOrderDiscount = discount;
                Instance.Percentage = 10;

                Instance.RecalculateTaxTotalValue();

                return this;
            }

            protected override void Specifications()
            {
                base.Specifications();

                AddSpecification<TaxMovimentMustHaveOrderBaseValue>();
                AddSpecification<TaxMovimentMustHaveOrderDiscount>();
                AddSpecification<TaxMovimentMustHaveOrderId>();
            }
        }
    }
}
