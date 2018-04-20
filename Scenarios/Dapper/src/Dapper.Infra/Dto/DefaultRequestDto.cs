using Tnf.Dto;

namespace Dapper.Infra.Dto
{
    public class DefaultRequestDto : RequestDto, IDefaultRequestDto
    {
        public int Id { get; set; }

        public DefaultRequestDto()
        {
        }

        public DefaultRequestDto(int id)
        {
            Id = Id;
        }

        public DefaultRequestDto(int id, RequestDto requestDto)
        {
            Id = id;
            Fields = requestDto.Fields;
            Expand = requestDto.Expand;
        }
    }
}
