using System;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Domain
{
    public class DefaultRequestDto : RequestDto, IDefaultRequestDto
    {
        public Guid Id { get; set; }

        public DefaultRequestDto()
        {
        }

        public DefaultRequestDto(Guid id)
        {
            Id = id;
        }

        public DefaultRequestDto(Guid id, RequestDto requestDto)
        {
            Id = id;
            Fields = requestDto.Fields;
            Expand = requestDto.Expand;
        }
    }
}
