using Tnf.Dto;

namespace Dapper.Infra.Dto
{
    public interface IDefaultRequestDto : IRequestDto
    {
        int Id { get; set; }
    }
}
