using System;

namespace SGDP.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
