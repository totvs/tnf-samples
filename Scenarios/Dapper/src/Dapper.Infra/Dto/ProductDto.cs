using Tnf.Dto;

namespace Dapper.Infra.Dto
{
    public class ProductDto : DtoBase
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
