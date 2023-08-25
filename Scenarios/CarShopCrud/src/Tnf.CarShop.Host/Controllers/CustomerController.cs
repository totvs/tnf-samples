using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.CarShop.Application.Commands.Customer.Create;
using Tnf.CarShop.Application.Commands.Customer.Delete;
using Tnf.CarShop.Application.Commands.Customer.Get;
using Tnf.CarShop.Application.Commands.Customer.Update;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Host.Constants;
using Tnf.Commands;
using Tnf.Dto;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Customer)]
[TnfAuthorize]
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
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid customerId)
    {
        var command = new GetCustomerCommand { CustomerId = customerId };

        var result = await _commandSender.SendAsync<GetCustomerResult>(command);

        if (result is null)
            return NotFound();

        return CreateResponseOnGet(result.Customer);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<CustomerDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAll([FromQuery] RequestAllDto requestAllDto)
    {
        var result = await _commandSender.SendAsync<GetCustomerResult>(new GetCustomerCommand { RequestAllCustomers = requestAllDto });

        return CreateResponseOnGetAll(result.Customers);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCustomerResult), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Create(CreateCustomerCommand command)
    {
        var result = await _commandSender.SendAsync<CreateCustomerResult>(command);

        return CreateResponseOnPost(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateCustomerResult), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(CustomerDto command)
    {
        var result = await _commandSender.SendAsync<UpdateCustomerResult>(command);

        return CreateResponseOnPut(result);
    }

    [HttpDelete("{customerId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid customerId)
    {
        var command = new DeleteCustomerCommand { CustomerId = customerId };

        var result = await _commandSender.SendAsync<DeleteCustomerResult>(command);

        if (!result.Success)
            return BadRequest();

        return CreateResponseOnDelete(result);
    }
}
