using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Customer;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Create
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, CreateCustomerResult>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task HandleAsync(ICommandContext<CreateCustomerCommand, CreateCustomerResult> context,
            CancellationToken cancellationToken = new())
        {
            var customerDto = context.Command.Customer;

            var createdCustomerId = await CreateCustomerAsync(customerDto, cancellationToken);

            context.Result = new CreateCustomerResult(createdCustomerId, true);

            return;
        }

        private async Task<Guid> CreateCustomerAsync(CustomerDto customer, CancellationToken cancellationToken)
        {
            var newCustomer = new Domain.Entities.Customer(customer.FullName, customer.Address, customer.Phone, customer.Email, customer.DateOfBirth);

            var createdCustomer = await _customerRepository.InsertAsync(newCustomer, cancellationToken);

            return createdCustomer.Id;
        }
    }
}