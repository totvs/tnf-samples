using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Application.Messages.Events;

public interface ICarEventPublisher
{
    Task NotifyCreationAsync(Car car, CancellationToken cancellationToken = default);
    Task NotifyUpdateAsync(CarDto carDto, CancellationToken cancellationToken = default);
}
