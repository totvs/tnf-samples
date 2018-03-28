using System;
using System.Collections.Generic;

namespace Dapper.Infra.Dto
{
    public class SumarizedOrder
    {
        public DateTime Date { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalValue { get; set; }
        public IEnumerable<SumarizedProduct> Products { get; set; }
    }

    public class SumarizedProduct
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
    }
}
