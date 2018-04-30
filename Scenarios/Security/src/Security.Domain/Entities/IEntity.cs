using System;

namespace Security.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
