using System;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Sales.Dto
{
    public class PurchaseOrderRequestAllDto : RequestAllDto
    {
        public Guid Number { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
