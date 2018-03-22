using System;
using Tnf.Builder;
using Tnf.Notifications;
using Tnf.Repositories.Entities;

namespace SuperMarket.Backoffice.FiscalService.Domain.Entities
{
    public class TaxMoviment : Entity<Guid>
    {
        public Guid OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid OrderCustomer { get; set; }
        public int Percentage { get; set; }
        public decimal OrderBaseValue { get; set; }
        public decimal OrderTotalValue { get; set; }

        internal void RecalculateTaxTotalValue()
        {
            OrderTotalValue -= ((Percentage / 100) * OrderTotalValue);
        }

        public enum Error
        {
            TaxMovimentMustHaveOrderBaseValue,
            TaxMovimentMustHaveOrderCustomer,
            TaxMovimentMustHaveOrderDate,
            TaxMovimentMustHaveOrderNumber
        }

        public class TaxMovimentBuilder : Builder<TaxMoviment>
        {
            public TaxMovimentBuilder(INotificationHandler notificationHandler)
                : base(notificationHandler)
            {
            }

            public TaxMovimentBuilder(INotificationHandler notificationHandler, TaxMoviment instance)
                : base(notificationHandler, instance)
            {
            }

            public TaxMovimentBuilder ForPurchaseOrder(Guid number, DateTime date, Guid customerId, decimal baseValue)
            {
                Instance.OrderNumber = number;
                Instance.OrderDate = date;
                Instance.OrderCustomer = customerId;
                Instance.OrderBaseValue = baseValue;
                Instance.Percentage = 10;

                Instance.RecalculateTaxTotalValue();

                return this;
            }
        }
    }
}
