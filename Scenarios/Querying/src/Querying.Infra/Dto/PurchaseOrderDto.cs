using System;
using System.Collections.Generic;
using System.Text;
using Tnf.Dto;

namespace Querying.Infra.Dto
{
    public class PurchaseOrderDto : DtoBase
    {
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalValue { get; set; }
    }
}
