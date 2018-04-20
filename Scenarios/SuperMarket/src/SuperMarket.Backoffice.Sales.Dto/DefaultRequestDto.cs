using System;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Sales.Dto
{
    public class DefaultRequestDto : RequestDto
    {
        public Guid Id { get; set; }

        public DefaultRequestDto()
        {
        }

        public DefaultRequestDto(Guid id)
        {
            Id = id;
        }

        public DefaultRequestDto(Guid id, RequestDto request)
        {
            Id = id;
            Fields = request.Fields;
            Expand = request.Expand;
        }
    }
}
