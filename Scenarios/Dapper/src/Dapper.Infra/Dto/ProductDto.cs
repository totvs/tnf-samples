using Tnf.Dto;

namespace Dapper.Infra.Dto
{
    public class ProductDto : BaseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
