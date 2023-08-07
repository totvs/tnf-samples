using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Customer.Get;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Customer;
using Tnf.Commands;

public class GetCustomerCommandHandler : ICommandHandler<GetCustomerCommand, GetCustomerResult>
{
    private readonly ILogger<GetCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly CarFactory _carFactory;
    private readonly CustomerFactory _customerFactory;

    public GetCustomerCommandHandler(ILogger<GetCustomerCommandHandler> logger, ICustomerRepository customerRepository, CarFactory carFactory, CustomerFactory customerFactory)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _carFactory = carFactory;
        _customerFactory = customerFactory;
    }

    public async Task HandleAsync(ICommandContext<GetCustomerCommand, GetCustomerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var customerId = context.Command.CustomerId;

        var customer = await _customerRepository.GetAsync(customerId, cancellationToken);

        if (customer == null)
        {
            throw new Exception($"Customer with id {customerId} not found.");
        }

        var customerDto = _customerFactory.ToDto(customer);
        
        context.Result = new GetCustomerResult(customerDto);

        return;
    }
}