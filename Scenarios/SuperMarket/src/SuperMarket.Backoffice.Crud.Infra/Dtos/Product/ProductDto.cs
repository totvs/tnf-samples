using System;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Infra.Dtos
{
    public class ProductDto : DtoBase<Guid>
    {
        public static ProductDto NullInstance = new ProductDto().AsNullable<ProductDto, Guid>();

        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
