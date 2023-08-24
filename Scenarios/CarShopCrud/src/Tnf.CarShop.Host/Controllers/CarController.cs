﻿using Microsoft.AspNetCore.Mvc;

using Tnf.AspNetCore.Mvc.Response;

using Tnf.CarShop.Application.Commands.Car.Create;
using Tnf.CarShop.Application.Commands.Car.Delete;
using Tnf.CarShop.Application.Commands.Car.Get;
using Tnf.CarShop.Application.Commands.Car.Update;
using Tnf.CarShop.Domain.Dtos;
using Tnf.CarShop.Host.Constants;

using Tnf.Commands;

using Tnf.Dto;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Car)]
[TnfAuthorize]
public class CarController : TnfController
{
    private readonly ICommandSender _commandSender;

    public CarController(ICommandSender commandSender)
    {
        _commandSender = commandSender;
    }


    [HttpGet("{carId}")]
    [ProducesResponseType(typeof(CarDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid carId)
    {
        var command = new GetCarCommand { CarId = carId };

        var result = await _commandSender.SendAsync<GetCarResult>(command);

        if (result is null)
            return NotFound();

        return CreateResponseOnGet(result.Car);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<CarDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAll([FromQuery] RequestAllDto requestAllDto)
    {
        var result = await _commandSender.SendAsync<GetCarResult>(new GetCarCommand { RequestAllCars = requestAllDto });

        return CreateResponseOnGetAll(result.Cars);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCarResult), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Create(CreateCarCommand command)
    {
        var result = await _commandSender.SendAsync<CreateCarResult>(command);

        return CreateResponseOnPost(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateCarResult), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(UpdateCarCommand command)
    {
        var result = await _commandSender.SendAsync<UpdateCarResult>(command);

        return CreateResponseOnPut(result);
    }

    [HttpDelete("{carId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid carId)
    {
        var command = new DeleteCarCommand(carId);

        var result = await _commandSender.SendAsync<DeleteCarResult>(command);

        if (!result.Success)
            return BadRequest();

        return CreateResponseOnDelete();
    }
}
