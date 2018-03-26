using System;
using System.Collections.Generic;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Sales.Dto
{
    public class PurchaseOrderDto : DtoBase<Guid>
    {
        public static PurchaseOrderDto NullInstance = new PurchaseOrderDto().AsNullable<PurchaseOrderDto, Guid>();

        public Guid CustomerId { get; set; }
        public decimal Discount { get; set; }
        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();

        public class ProductDto
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }

            public ProductDto() { }

            public ProductDto(Guid productId, int quantity)
            {
                ProductId = productId;
                Quantity = quantity;
            }
        }
    }
}
