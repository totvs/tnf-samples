using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Tnf.CarShop.Host.Commands.Purchase.Create
{
    public class CreatePurchaseCommandHandler : ICommandHandler<PurchaseCommand, PurchaseResult>
    {
        private readonly ILogger<CreatePurchaseCommandHandler> _logger;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;

        public CreatePurchaseCommandHandler(ILogger<CreatePurchaseCommandHandler> logger, IPurchaseRepository purchaseRepository, ICarRepository carRepository, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _purchaseRepository = purchaseRepository;
            _carRepository = carRepository;
            _customerRepository = customerRepository;
        }

        public async Task HandleAsync(ICommandContext<PurchaseCommand, PurchaseResult> context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var command = context.Command;

            var createdPurchaseId = await CreatePurchaseAsync(command, cancellationToken);

            context.Result = new PurchaseResult(createdPurchaseId, true);

            return;
        }

        private async Task<Guid> CreatePurchaseAsync(PurchaseCommand command, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetAsync(command.CarId, cancellationToken);
            var customer = await _customerRepository.GetAsync(command.CustomerId, cancellationToken);

            if (car == null || customer == null)
            {
                throw new Exception("Invalid car or customer.");
            }

            var newPurchase = new Domain.Entities.Purchase(customer, car);

            var createdPurchase = await _purchaseRepository.InsertAsync(newPurchase, cancellationToken);

            return createdPurchase.Id;
        }
    }
}