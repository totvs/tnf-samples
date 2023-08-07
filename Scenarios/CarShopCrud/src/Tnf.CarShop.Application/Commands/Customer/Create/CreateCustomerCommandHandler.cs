using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Tnf.CarShop.Host.Commands.Customer.Create
{
    public class CreateCustomerCommandHandler : ICommandHandler<CustomerCommand, CustomerResult>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task HandleAsync(ICommandContext<CustomerCommand, CustomerResult> context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var command = context.Command;

            var createdCustomerId = await CreateCustomerAsync(command, cancellationToken);

            context.Result = new CustomerResult(createdCustomerId, true);

            return;
        }

        private async Task<Guid> CreateCustomerAsync(CustomerCommand command, CancellationToken cancellationToken)
        {
            var newCustomer = new Domain.Entities.Customer(command.FullName, command.Address, command.Phone, command.Email, command.DateOfBirthDay);

            var createdCustomer = await _customerRepository.InsertAsync(newCustomer, cancellationToken);

            return createdCustomer.Id;
        }
    }
}