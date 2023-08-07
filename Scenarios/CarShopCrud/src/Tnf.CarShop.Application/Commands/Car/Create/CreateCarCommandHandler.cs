﻿using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Car.Create;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Car.Create;
public class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, CreateCarResult>
{
    private readonly ILogger<CreateCarCommandHandler> _logger;
    private readonly ICarRepository _carRepository;
    private readonly IDealerRepository _dealerRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly CarFactory _carFactory;
    

    public CreateCarCommandHandler(ILogger<CreateCarCommandHandler> logger, ICarRepository carRepository, IDealerRepository dealerRepository, ICustomerRepository customerRepository, CarFactory carFactory)
    {
        _logger = logger;
        _carRepository = carRepository;
        _dealerRepository = dealerRepository;
        _customerRepository = customerRepository;
        _carFactory = carFactory;
    }

    public async Task HandleAsync(ICommandContext<CreateCarCommand, CreateCarResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {

        var carDto = context.Command.Car;

        var createdCarId = await CreateCarAsync(carDto, cancellationToken);
        
        context.Result = new CreateCarResult(createdCarId, true);

        return;
    }

    private async Task<Guid> CreateCarAsync(CarDto carDto, CancellationToken cancellationToken)
    {
        var newCar = _carFactory.ToEntity(carDto);

        if (carDto.Dealer != null)
        {
            var dealer = await FetchDealerAsync(carDto.Dealer.Id, cancellationToken);
            CheckEntityNotNull(dealer, "Dealer", carDto.Dealer.Id);
            newCar.AssignToDealer(dealer);
        }
        
        if (carDto.Owner != null)
        {
            var owner = await FetchOwnerAsync(carDto.Owner.Id, cancellationToken);
            CheckEntityNotNull(owner, "Customer", carDto.Owner.Id);
            newCar.AssignToOwner(owner);
        }
        
        var createdCar = await _carRepository.InsertAsync(newCar, cancellationToken);

        return createdCar.Id;
    }
    
    private async Task<Domain.Entities.Dealer> FetchDealerAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dealerRepository.GetAsync(id, cancellationToken);
    }

    private async Task<Domain.Entities.Customer> FetchOwnerAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetAsync(id, cancellationToken);
    }

    private void CheckEntityNotNull<T>(T entity, string entityType, Guid id)
    {
        if (entity == null)
        {
            throw new Exception($"{entityType} with id {id} not found.");
        }
    }
    
}