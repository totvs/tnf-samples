using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.CarShop.Application.Commands.Customer.Create;
using Tnf.CarShop.Application.Commands.Customer.Delete;
using Tnf.CarShop.Application.Commands.Customer.Get;
using Tnf.CarShop.Application.Commands.Customer.Update;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Host.Constants;
using Tnf.Commands;
using Tnf.Dto;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Customer)]
public class CustomerController : TnfController
{
    private readonly ICommandSender _commandSender;

    public CustomerController(ICommandSender commandSender)
    {
        _commandSender = commandSender;
    }

    [HttpGet("{customerId}")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetById(Guid customerId)
    {
        var command = new GetCustomerCommand { CustomerId = customerId };

        var result = await _commandSender.SendAsync<GetCustomerResult>(command);

        return CreateResponseOnGet(result.Customer);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<CustomerDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _commandSender.SendAsync<GetCustomerResult>(new GetCustomerCommand());

        return CreateResponseOnGetAll(result.Customers);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCustomerResult), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Create(CustomerDto customer)
    {
        var command = new CreateCustomerCommand { Customer = customer };

        var result = await _commandSender.SendAsync<CreateCustomerResult>(command);

        return CreateResponseOnPost(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateCustomerResult), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(CustomerDto customer)
    {
        var command = new UpdateCustomerCommand { Customer = customer };

        var result = await _commandSender.SendAsync<UpdateCustomerResult>(command);

        return CreateResponseOnPut(result);
    }

    [HttpDelete("{customerId}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid customerId)
    {
        var command = new DeleteCustomerCommand { CustomerId = customerId };

        var result = await _commandSender.SendAsync(command);

        return CreateResponseOnDelete(result);
    }
}