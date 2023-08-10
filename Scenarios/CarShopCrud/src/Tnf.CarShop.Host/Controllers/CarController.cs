using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Constants;
using Tnf.Dto;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Car)]
public class CarController : TnfController
{

    private readonly ICarRepository _carRepository;

    public CarController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }


    [HttpGet("{model}")]
    [ProducesResponseType(typeof(CarDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public IActionResult GetByModel(string model)
    {
        var car = _carRepository.GetAsync()

        return CreateResponseOnGet();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<CarDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public IActionResult GetAll()
    {
        var cars = _carRepository.GetAllAsync();
        return CreateResponseOnGetAll(cars);
    }
         
}