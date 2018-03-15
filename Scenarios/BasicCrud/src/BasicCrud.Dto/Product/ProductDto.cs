using System;
using Tnf.Dto;

namespace BasicCrud.Dto.Product
{
    public class ProductDto : DtoBase<Guid>
    {
        public static ProductDto NullInstance = new ProductDto().AsNullable<ProductDto, Guid>();

        public string Description { get; set; }

        public float Value { get; set; }
    }
}
