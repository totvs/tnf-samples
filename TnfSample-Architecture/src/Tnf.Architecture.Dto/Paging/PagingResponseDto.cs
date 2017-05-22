using System.Collections.Generic;
using Tnf.Dto;

namespace Tnf.Architecture.Dto
{
    public class PagingResponseDto<TEntity> : ResponseDtoBase<List<TEntity>>
        where TEntity : class
    {
        public PagingResponseDto() => Data = new List<TEntity>();
        public PagingResponseDto(List<TEntity> entities) => Data = entities;

        public int Total { get; set; }
    }
}