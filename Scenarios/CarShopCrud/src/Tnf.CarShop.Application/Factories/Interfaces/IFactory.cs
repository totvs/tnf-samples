using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Factories.Interfaces;

public interface IFactory<TDto, TEntity>
{
    TEntity ToEntity(TDto dto);
    TDto ToDto(TEntity entity);
}

public interface ICustomerFactory : IFactory<CustomerDto, Customer>
{
}

public interface IDealerFactory : IFactory<DealerDto, Store>
{
}

public interface IPurchaseFactory : IFactory<PurchaseDto, Purchase>
{
}