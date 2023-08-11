﻿using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Purchase.Create;

public class CreatePurchaseCommandHandler : CommandHandler<CreatePurchaseCommand, CreatePurchaseResult>
{
    private readonly ICarRepository _carRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CreatePurchaseCommandHandler> _logger;
    private readonly PurchaseFactory _purchaseFactory;
    private readonly IPurchaseRepository _purchaseRepository;

    public CreatePurchaseCommandHandler(ILogger<CreatePurchaseCommandHandler> logger,
        IPurchaseRepository purchaseRepository, ICarRepository carRepository, ICustomerRepository customerRepository,
        PurchaseFactory purchaseFactory)
    {
        _logger = logger;
        _purchaseRepository = purchaseRepository;
        _carRepository = carRepository;
        _customerRepository = customerRepository;
        _purchaseFactory = purchaseFactory;
    }

    public override async Task<CreatePurchaseResult> ExecuteAsync(CreatePurchaseCommand command,
        CancellationToken cancellationToken = default)
    {
        var purchaseDto = command.Purchase;

        var createdPurchaseId = await CreatePurchaseAsync(purchaseDto, cancellationToken);

        return new CreatePurchaseResult(createdPurchaseId);
    }

    private async Task<Guid> CreatePurchaseAsync(PurchaseDto purchaseDto, CancellationToken cancellationToken)
    {
        var car = await _carRepository.GetAsync(purchaseDto.Car.Id, cancellationToken);
        var customer = await _customerRepository.GetAsync(purchaseDto.Customer.Id, cancellationToken);

        if (car == null || customer == null) throw new Exception("Invalid car or customer.");

        var newPurchase = _purchaseFactory.ToEntity(purchaseDto);

        var createdPurchase = await _purchaseRepository.InsertAsync(newPurchase, cancellationToken);

        return createdPurchase.Id;
    }
}