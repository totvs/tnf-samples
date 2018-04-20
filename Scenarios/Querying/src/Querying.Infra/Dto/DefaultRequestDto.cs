using Tnf.Dto;

namespace Querying.Infra.Dto
{
    public class DefaultRequestDto : RequestDto, IDefaultRequestDto
    {
        public int Id { get; set; }

        public DefaultRequestDto()
        {
        }

        public DefaultRequestDto(int id)
        {
            Id = id;
        }

        public DefaultRequestDto(int id, RequestDto requestDto)
        {
            Id = id;
            Fields = requestDto.Fields;
            Expand = requestDto.Expand;
        }
    }
}
