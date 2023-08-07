﻿using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Customer.Get;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Customer;
using Tnf.Commands;

public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, UpdateCustomerResult>
{
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerFactory _customerFactory;


    public UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger, ICustomerRepository customerRepository, CustomerFactory customerFactory)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _customerFactory = customerFactory;
    }

    public async Task HandleAsync(ICommandContext<UpdateCustomerCommand, UpdateCustomerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var customerDto = context.Command.Customer;

        var customer = await _customerRepository.GetAsync(customerDto.Id, cancellationToken);

        if (customer == null)
        {
            throw new Exception($"Customer with id {customerDto.Id} not found.");
        }

        customer.UpdateFullName(customerDto.FullName);
        customer.UpdateAddress(customerDto.Address);
        customer.UpdatePhone(customerDto.Phone);
        customer.UpdateEmail(customerDto.Email);
        customer.UpdateDateOfBirth(customerDto.DateOfBirth);

        var updatedCustomer = await _customerRepository.UpdateAsync(customer, cancellationToken);
        
        var updatedCustomerDto = _customerFactory.ToDto(updatedCustomer);
        
        context.Result = new UpdateCustomerResult(updatedCustomerDto);
        
        return;
    }
}