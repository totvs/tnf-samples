using System;
using Tnf.Dto;

namespace Security.Dto
{
    public interface IDefaultRequestDto : IRequestDto
    {
        Guid Id { get; set; }
    }
}
