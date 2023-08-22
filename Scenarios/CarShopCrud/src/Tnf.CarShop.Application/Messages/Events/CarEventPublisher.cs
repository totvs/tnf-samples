using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Localization;
using Tnf.CarShop.Domain.Entities;

using Tnf.Messaging;

namespace Tnf.CarShop.Application.Messages.Events;

public class CarEventPublisher : ICarEventPublisher
{
    private readonly IMessageSender _messageSender;

    public CarEventPublisher(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task NotifyCreationAsync(Car car, CancellationToken cancellationToken = default)
    {
        if (car is null)
            return;

        var message = new CloudEvent<CarCreatedEvent>
        {
            TransactionId = Guid.NewGuid().ToString(),
            Source = new Uri(LocalizationSource.Default, UriKind.Relative),
            Data = new CarCreatedEvent
            {
                Brand = car.Brand,
                Model = car.Model,
                Price = car.Price,
                TenantId = car.TenantId,
                Year = car.Year
            }
        };

        await SendMessageAsync(message);
    }

    public async Task NotifyUpdateAsync(CarDto carDto, CancellationToken cancellationToken = default)
    {
        if (carDto is null)
            return;

        var message = new CloudEvent<CarUpdatedEvent>
        {
            TransactionId = Guid.NewGuid().ToString(),
            Source = new Uri(LocalizationSource.Default, UriKind.Relative),
            Data = new CarUpdatedEvent
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                Price = carDto.Price,
                TenantId = carDto.TenantId,
                Year = carDto.Year
            }
        };

        await SendMessageAsync(message, cancellationToken);
    }

    private async Task SendMessageAsync<T>(CloudEvent<T> @event, CancellationToken cancellationToken = default)
    {
        await _messageSender.SendAsync(@event, cancellationToken);
    }
}
