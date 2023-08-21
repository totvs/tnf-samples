using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.CarShop.Application.Commands.Purchase.Create;
using Tnf.CarShop.Application.Commands.Purchase.Delete;
using Tnf.CarShop.Application.Commands.Purchase.Get;
using Tnf.CarShop.Application.Commands.Purchase.Update;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Host.Constants;
using Tnf.Commands;
using Tnf.Dto;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Purchase)]
public class PurchaseController : TnfController
{
    private readonly ICommandSender _commandSender;

    public PurchaseController(ICommandSender commandSender)
    {
        _commandSender = commandSender;
    }

    [HttpGet("{purchaseId}")]
    [ProducesResponseType(typeof(PurchaseDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetById(Guid purchaseId)
    {
        var command = new GetPurchaseCommand { PurchaseId = purchaseId };

        var result = await _commandSender.SendAsync<GetPurchaseResult>(command);

        return CreateResponseOnGet(result.Purchase);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<PurchaseDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _commandSender.SendAsync<GetPurchaseResult>(new GetPurchaseCommand());

        return CreateResponseOnGetAll(result.Purchases);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatePurchaseResult), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Create(CreatePurchaseCommand command)
    {
        var result = await _commandSender.SendAsync<CreatePurchaseResult>(command);

        return CreateResponseOnPost(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdatePurchaseResult), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(CreatePurchaseCommand command)
    {
        var result = await _commandSender.SendAsync<UpdatePurchaseResult>(command);

        return CreateResponseOnPut(result);
    }

    [HttpDelete("{purchaseId}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid purchaseId)
    {
        var command = new DeletePurchaseCommand { PurchaseId = purchaseId };

        var result = await _commandSender.SendAsync(command);

        return CreateResponseOnDelete(result);
    }
}