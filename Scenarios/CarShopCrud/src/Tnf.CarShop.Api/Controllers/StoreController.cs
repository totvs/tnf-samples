using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.CarShop.Application.Commands.Store;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Constants;
using Tnf.Commands;
using Tnf.Dto;
using CarShopLocalization = Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Store)]
[TnfAuthorize]
public class StoreController : TnfController
{
    private readonly ICommandSender _commandSender;
    private readonly IStoreRepository _storeRepository;

    //Para manter a simplicidade do projeto estamos realizando os GETs e o DELETE diretamente através do repository.
    //Para casos mais complexos deve-se criar uma service
    //ou até mesmo comandos que possam ter validações e regras de negócio, retornando os dados necessários.

    public StoreController(ICommandSender commandSender, IStoreRepository storeRepository)
    {
        _commandSender = commandSender;
        _storeRepository = storeRepository;
    }

    [HttpGet("{storeId}")]
    [ProducesResponseType(typeof(StoreDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid storeId)
    {
        var store = await _storeRepository.GetAsync(storeId, HttpContext.RequestAborted);

        if (store is null)
            return NotFound();

        var storeDto = store.ToDto();

        return CreateResponseOnGet(storeDto);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<StoreDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAll([FromQuery] RequestAllDto requestAllDto)
    {
        var storesDto = await _storeRepository.GetAllAsync(requestAllDto, HttpContext.RequestAborted);

        return CreateResponseOnGetAll(storesDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(StoreDto), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Create(StoreCommandCreate command)
    {
        var result = await _commandSender.SendAsync<StoreResult>(command);

        return CreateResponseOnPost(result.StoreDto);
    }

    [HttpPut("{storeId}")]
    [ProducesResponseType(typeof(StoreDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(Guid? storeId, [FromBody] StoreCommandUpdate command)
    {        
        if (!storeId.HasValue)
        {
            Notification.RaiseError(CarShopLocalization.LocalizationSource.Default, CarShopLocalization.LocalizationKeys.PropertyRequired, nameof(command.Id));
            return CreateResponseOnPut();
        }

        command.Id = storeId;

        var result = await _commandSender.SendAsync<StoreResult>(command);

        return CreateResponseOnPut(result.StoreDto);
    }

    [HttpDelete("{storeId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid storeId)
    {
        await _storeRepository.DeleteAsync(storeId, HttpContext.RequestAborted);

        return CreateResponseOnDelete();
    }
}
