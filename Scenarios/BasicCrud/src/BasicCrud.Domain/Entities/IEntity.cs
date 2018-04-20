using System;

namespace BasicCrud.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
