using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.CarShop.Application.Commands.Car;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Constants;
using Tnf.Commands;
using Tnf.Dto;
using CarShopLocalization = Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Car)]
[TnfAuthorize]
public class CarController : TnfController
{
    private readonly ICommandSender _commandSender;
    private readonly ICarRepository _carRepository;

    //Para manter a simplicidade do projeto estamos realizando os GETs e o DELETE diretamente através do repository.
    //Para casos mais complexos deve-se criar uma service
    //ou até mesmo comandos que possam ter validações e regras de negócio, retornando os dados necessários.

    public CarController(ICommandSender commandSender, ICarRepository carRepository)
    {
        _commandSender = commandSender;
        _carRepository = carRepository;
    }


    [HttpGet("{carId}")]
    [ProducesResponseType(typeof(CarDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid carId)
    {
        var car = await _carRepository.GetAsync(carId, HttpContext.RequestAborted);

        if (car is null)
            return NotFound();

        var carDto = car.ToDto();

        return CreateResponseOnGet(carDto);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<CarDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAll([FromQuery] RequestAllDto requestAllDto)
    {
        var cars = await _carRepository.GetAllAsync(requestAllDto, HttpContext.RequestAborted);

        return CreateResponseOnGetAll(cars);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CarDto), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Create(CarCommandCreateAdmin command)
    {
        command.MustBeAdmin = true;
        var result =  await _commandSender.SendAsync<CarResult>(command);

        return CreateResponseOnPost(result.CarDto);
    }

    [HttpPut("{carId}")]
    [ProducesResponseType(typeof(CarDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(Guid? carId, [FromBody] CarCommandUpdate command)
    {       
        if (!carId.HasValue)
        {
            Notification.RaiseError(CarShopLocalization.LocalizationSource.Default, CarShopLocalization.LocalizationKeys.PropertyRequired, nameof(command.Id));
            return CreateResponseOnPut();
        }

        command.Id = carId;

        var result = await _commandSender.SendAsync<CarResult>(command);

        return CreateResponseOnPut(result.CarDto);
    }

    [HttpDelete("{carId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid carId)
    {
        await _carRepository.DeleteAsync(carId, HttpContext.RequestAborted);

        return CreateResponseOnDelete();
    }
}
