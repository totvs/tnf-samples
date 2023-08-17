using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.CarShop.Application.Commands.Dealer.Create;
using Tnf.CarShop.Application.Commands.Dealer.Delete;
using Tnf.CarShop.Application.Commands.Dealer.Get;
using Tnf.CarShop.Application.Commands.Dealer.Update;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Host.Constants;
using Tnf.Commands;
using Tnf.Dto;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Dealer)]
public class DealerController : TnfController
{
    private readonly ICommandSender _commandSender;

    public DealerController(ICommandSender commandSender)
    {
        _commandSender = commandSender;
    }

    [HttpGet("{dealerId}")]
    [ProducesResponseType(typeof(StoreDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetById(Guid storeId)
    {
        var command = new GetStoreCommand { StoreId = storeId };

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
    [ProducesResponseType(typeof(UpdateDealerResult), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(UpdateShopCommand command)
    {        
        var result = await _commandSender.SendAsync<UpdateDealerResult>(command);

        return CreateResponseOnPut(result);
    }

    [HttpDelete("{dealerId}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid dealerId)
    {
        var command = new DeleteStoreCommand { StoreId = dealerId };

        var result = await _commandSender.SendAsync(command);

        return CreateResponseOnDelete(result);
    }
}