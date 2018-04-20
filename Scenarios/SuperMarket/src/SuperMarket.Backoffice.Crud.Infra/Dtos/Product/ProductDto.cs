using System;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Infra.Dtos
{
    public class ProductDto : BaseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
