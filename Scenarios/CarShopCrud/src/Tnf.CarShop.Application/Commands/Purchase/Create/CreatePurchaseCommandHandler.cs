using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;
using System.Threading;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;

namespace Tnf.CarShop.Host.Commands.Purchase.Create
{
    public class CreatePurchaseCommandHandler : ICommandHandler<CreatePurchaseCommand, CreatePurchaseResult>
    {
        private readonly ILogger<CreatePurchaseCommandHandler> _logger;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly PurchaseFactory _purchaseFactory;

        public CreatePurchaseCommandHandler(ILogger<CreatePurchaseCommandHandler> logger, IPurchaseRepository purchaseRepository, ICarRepository carRepository, ICustomerRepository customerRepository, PurchaseFactory purchaseFactory)
        {
            _logger = logger;
            _purchaseRepository = purchaseRepository;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _purchaseFactory = purchaseFactory;
        }

        public async Task HandleAsync(ICommandContext<CreatePurchaseCommand, CreatePurchaseResult> context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var purchaseDto = context.Command.Purchase;

            var createdPurchaseId = await CreatePurchaseAsync(purchaseDto, cancellationToken);

            context.Result = new CreatePurchaseResult(createdPurchaseId);

            return;
        }

        private async Task<Guid> CreatePurchaseAsync(PurchaseDto purchaseDto, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetAsync(purchaseDto.Car.Id, cancellationToken);
            var customer = await _customerRepository.GetAsync(purchaseDto.Customer.Id, cancellationToken);

            if (car == null || customer == null)
            {
                throw new Exception("Invalid car or customer.");
            }

            var newPurchase = _purchaseFactory.ToEntity(purchaseDto);

            var createdPurchase = await _purchaseRepository.InsertAsync(newPurchase, cancellationToken);

            return createdPurchase.Id;
        }
    }
}