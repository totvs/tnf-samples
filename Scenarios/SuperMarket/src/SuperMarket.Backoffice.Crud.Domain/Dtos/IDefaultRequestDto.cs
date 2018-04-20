using System;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Domain
{
    public interface IDefaultRequestDto : IRequestDto
    {
        Guid Id { get; set; }
    }
}
