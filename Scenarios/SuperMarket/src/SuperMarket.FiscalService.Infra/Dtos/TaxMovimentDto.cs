using System;
using Tnf.Dto;

namespace SuperMarket.FiscalService.Infra.Dtos
{
    public class TaxMovimentDto : BaseDto
    {
        public Guid Id { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public decimal PurchaseOrderBaseValue { get; set; }
        public decimal PurchaseOrderDiscount { get; set; }
        public int Percentage { get; set; }
        public decimal Tax { get; set; }
        public decimal PurchaseOrderTotalValue { get; set; }
    }
}
