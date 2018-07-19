using System;

namespace ProdutoXyz.Dto.Product
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public float Value { get; set; }

        public int TenantId { get; set; }
    }
}
