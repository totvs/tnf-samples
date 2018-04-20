using Tnf.Dto;

namespace Querying.Infra.Dto
{
    public interface IDefaultRequestDto : IRequestDto
    {
        int Id { get; set; }
    }
}
