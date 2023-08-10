using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer.Get;

public class GetCustomerCommandHandler : CommandHandler<GetCustomerCommand, GetCustomerResult>
{
    private readonly CustomerFactory _customerFactory;
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerCommandHandler(ICustomerRepository customerRepository, CustomerFactory customerFactory)
    {
        _customerRepository = customerRepository;
        _customerFactory = customerFactory;
    }

    public override async Task<GetCustomerResult> ExecuteAsync(GetCustomerCommand command, CancellationToken cancellationToken = default)
    {
        if (command.CustomerId.HasValue)
        {
            var customer = await _customerRepository.GetAsync(command.CustomerId.Value, cancellationToken);

            if (customer == null) throw new Exception($"Customer with id {command.CustomerId.Value} not found.");

            var customerDto = _customerFactory.ToDto(customer);

            return new GetCustomerResult(customerDto);
        }

        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        var customersDto = customers.Select(_customerFactory.ToDto).ToList();

        return new GetCustomerResult(customersDto);
    }    
}