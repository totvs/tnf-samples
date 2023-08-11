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
    [ProducesResponseType(typeof(DealerDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetById(Guid dealerId)
    {
        var command = new GetDealerCommand { DealerId = dealerId };

        var result = await _commandSender.SendAsync<GetDealerResult>(command);

        return CreateResponseOnGet(result.Dealer);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IListDto<DealerDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _commandSender.SendAsync<GetDealerResult>(new GetDealerCommand());

        return CreateResponseOnGetAll(result.Dealers);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateDealerResult), 201)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Create(DealerDto dealer)
    {
        var command = new CreateDealerCommand { Dealer = dealer };

        var result = await _commandSender.SendAsync<CreateDealerResult>(command);

        return CreateResponseOnPost(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateDealerResult), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Update(DealerDto dealer)
    {
        var command = new UpdateDealerCommand { Dealer = dealer };

        var result = await _commandSender.SendAsync<UpdateDealerResult>(command);

        return CreateResponseOnPut(result);
    }

    [HttpDelete("{dealerId}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 400)]
    public async Task<IActionResult> Delete(Guid dealerId)
    {
        var command = new DeleteDealerCommand { DealerId = dealerId };

        var result = await _commandSender.SendAsync(command);

        return CreateResponseOnDelete(result);
    }
}