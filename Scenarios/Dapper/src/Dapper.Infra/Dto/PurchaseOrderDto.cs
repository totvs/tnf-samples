using System;
using Tnf.Dto;

namespace Dapper.Infra.Dto
{
    public class PurchaseOrderDto : BaseDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalValue { get; set; }
    }
}
