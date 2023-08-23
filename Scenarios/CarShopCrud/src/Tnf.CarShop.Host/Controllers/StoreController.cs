using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.CarShop.Application.Commands.Store.Create;
using Tnf.CarShop.Application.Commands.Store.Delete;
using Tnf.CarShop.Application.Commands.Store.Get;
using Tnf.CarShop.Application.Commands.Store.Update;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Host.Constants;
using Tnf.Commands;
using Tnf.Dto;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Store)]
[TnfAuthorize]
public class StoreController : TnfController
{
    private readonly ICommandSender _commandSender;

    public StoreController(ICommandSender commandSender)
    {
        _commandSender = commandSender;
    }

    [HttpGet("{tenantId}")]
    [ProducesResponseType(typeof(StoreDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetById(Guid tenantId)
    {
        var command = new GetStoreCommand { TenantId = tenantId };

        var result = await _commandSender.SendAsync<GetStoreResult>(command);

        return CreateResponseOnGet(result.Store);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<StoreDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _commandSender.SendAsync<GetStoreResult>(new GetStoreCommand());

        return CreateResponseOnGetAll(result.Stores);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateStoreResult), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Create(CreateStoreCommand command)
    {
        var result = await _commandSender.SendAsync<CreateStoreResult>(command);

        return CreateResponseOnPost(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateStoreResult), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(UpdateStoreCommand command)
    {
        var result = await _commandSender.SendAsync<UpdateStoreResult>(command);

        return CreateResponseOnPut(result);
    }

    [HttpDelete("{storeId}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid storeId)
    {
        var command = new DeleteStoreCommand { StoreId = storeId };

        var result = await _commandSender.SendAsync(command);

        return CreateResponseOnDelete(result);
    }
}
