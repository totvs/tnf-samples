using System;
using Tnf.Dto;

namespace ProdutoXyz.Dto
{
    public interface IDefaultRequestDto : IRequestDto
    {
        Guid Id { get; set; }
    }
}
