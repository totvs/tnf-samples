using Microsoft.AspNetCore.Mvc;

using Tnf.AspNetCore.Mvc.Response;

using Tnf.CarShop.Application.Commands.Customer;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Constants;
using CarShopLocalization = Tnf.CarShop.Application.Localization;

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
    private readonly ICustomerRepository _customerRepository;

    //Para manter a simplicidade do projeto estamos realizando os GETs e o DELETE diretamente através do repository.
    //Para casos mais complexos deve-se criar uma service
    //ou até mesmo comandos que possam ter validações e regras de negócio, retornando os dados necessários.

    public CustomerController(ICommandSender commandSender, ICustomerRepository customerRepository)
    {
        _commandSender = commandSender;
        _customerRepository = customerRepository;
    }

    [HttpGet("{customerId}")]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid customerId)
    {
        var customer = await _customerRepository.GetAsync(customerId, HttpContext.RequestAborted);

        if (customer is null)
            return NotFound();

        var customerDto = customer.ToDto();

        return CreateResponseOnGet(customerDto);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<CustomerDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAll([FromQuery] RequestAllDto requestAllDto)
    {
        var customers = await _customerRepository.GetAllAsync(requestAllDto, HttpContext.RequestAborted);

        return CreateResponseOnGetAll(customers);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Create(CustomerCommand command)
    {
        var result = await _commandSender.SendAsync<CustomerResult>(command);

        return CreateResponseOnPost(result.CustomerDto);
    }

    [HttpPut]
    [ProducesResponseType(typeof(CustomerDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(CustomerCommand command)
    {
        if (!command.Id.HasValue)
        {
            Notification.RaiseError(CarShopLocalization.LocalizationSource.Default, CarShopLocalization.LocalizationKeys.PropertyRequired, nameof(command.Id));
            return CreateResponseOnPut();
        }

        var result = await _commandSender.SendAsync<CustomerResult>(command);

        return CreateResponseOnPut(result.CustomerDto);
    }

    [HttpDelete("{customerId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid customerId)
    {
        await _customerRepository.DeleteAsync(customerId, HttpContext.RequestAborted);

        return CreateResponseOnDelete();
    }
}
