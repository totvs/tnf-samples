using System;
using Tnf.Dto;

namespace SGDP.Dto
{
    public interface IDefaultRequestDto : IRequestDto
    {
        Guid Id { get; set; }
    }
}
