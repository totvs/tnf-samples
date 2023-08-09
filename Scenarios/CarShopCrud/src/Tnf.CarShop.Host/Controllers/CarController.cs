using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Host.Constants;
using Tnf.Dto;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Car)]
public class CarController : TnfController
{    
    [HttpGet("{model}")]
    [ProducesResponseType(typeof(CarDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public IActionResult GetByModel(string model)
    {
        return CreateResponseOnGet();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<CarDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public IActionResult GetAll()
    {
        return CreateResponseOnGetAll();
    }
         
}