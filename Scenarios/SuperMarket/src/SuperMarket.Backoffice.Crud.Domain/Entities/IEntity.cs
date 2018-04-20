using System;

namespace SuperMarket.Backoffice.Crud.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
