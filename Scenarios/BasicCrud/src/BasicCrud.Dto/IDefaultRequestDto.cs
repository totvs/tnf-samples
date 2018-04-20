using System;
using Tnf.Dto;

namespace BasicCrud.Dto
{
    public interface IDefaultRequestDto : IRequestDto
    {
        Guid Id { get; set; }
    }
}
