using System;

namespace ProdutoXyz.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
