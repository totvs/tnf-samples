using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.CarShop.Application.Commands.Purchase;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Constants;
using CarShopLocalization = Tnf.CarShop.Application.Localization;
using Tnf.Commands;
using Tnf.Dto;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Purchase)]
[TnfAuthorize]
public class PurchaseController : TnfController
{    

    private readonly IPurchaseRepository _purchaseRepository;
    private readonly ICommandSender _commandSender;

    //Para manter a simplicidade do projeto estamos realizando os GETs e o DELETE diretamente através do repository.
    //Para casos mais complexos deve-se criar uma service
    //ou até mesmo comandos que possam ter validações e regras de negócio, retornando os dados necessários.

    public PurchaseController(ICommandSender commandSender, IPurchaseRepository purchaseRepository)
    {
        _commandSender = commandSender;
        _purchaseRepository = purchaseRepository;
    }

    [HttpGet("{purchaseId}")]
    [ProducesResponseType(typeof(PurchaseDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid purchaseId)
    {
        var purchaseDto = await _purchaseRepository.GetPurchaseDtoAsync(purchaseId, HttpContext.RequestAborted);

        if (purchaseDto is null)
            return NotFound();

        return CreateResponseOnGet(purchaseDto);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<PurchaseDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAll([FromQuery] RequestAllDto requestAllDto)
    {
        var purchases = await _purchaseRepository.GetAllAsync(requestAllDto, HttpContext.RequestAborted);

        return CreateResponseOnGetAll(purchases);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PurchaseDto), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Create(PurchaseCommandCreate command)
    {
        var result = await _commandSender.SendAsync<PurchaseResult>(command);

        return CreateResponseOnPost(result.PurchaseDto);
    }

    [HttpPut("{purchaseId}")]
    [ProducesResponseType(typeof(PurchaseDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(Guid? purchaseId, [FromBody] PurchaseCommandUpdate command)
    {        
        if (!purchaseId.HasValue)
        {
            Notification.RaiseError(CarShopLocalization.LocalizationSource.Default, CarShopLocalization.LocalizationKeys.PropertyRequired, nameof(command.Id));
            return CreateResponseOnPut();
        }

        command.Id = purchaseId;

        var result = await _commandSender.SendAsync<PurchaseResult>(command);

        return CreateResponseOnPut(result.PurchaseDto);
    }

    [HttpDelete("{purchaseId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid purchaseId)
    {
        await _purchaseRepository.DeleteAsync(purchaseId, HttpContext.RequestAborted);

        return CreateResponseOnDelete();
    }
}
