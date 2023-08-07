namespace Tnf.CarShop.Application.Factories.Interfaces;

public interface IFactory<TDto, TEntity>
{
    TEntity ToEntity(TDto dto);
    TDto ToDto(TEntity entity);
}