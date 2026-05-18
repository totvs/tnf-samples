using Microsoft.AspNetCore.Mvc;
using Tnf.Commands;
using TnfZero.Application.Commands.CreateDog;
using TnfZero.Application.Commands.DeleteDog;
using TnfZero.Application.Commands.UpdateDog;
using TnfZero.Domain.Dtos;
using TnfZero.Domain.Repositories;

namespace TnfZero.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[TnfAuthorize]
public class DogsController : TnfController
{
    private readonly IDogRepository _repository;
    private readonly ICommandSender _sender;

    public DogsController(ICommandSender sender, IDogRepository repository)
    {
        _sender = sender;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DogRequestAllDto request, CancellationToken ct)
    {
        var result = await _repository.GetAllAsync(request, ct);
        return CreateResponseOnGetAll(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var entity = await _repository.FindByIdAsync(id, ct);
        if (entity is null) return NotFound();
        return Ok(new { entity.Id, entity.Name });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDogCommand command, CancellationToken ct)
    {
        var id = await _sender.SendAsync<Guid>(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDogCommand command, CancellationToken ct)
    {
        if (id != command.Id)
            return BadRequest("Route ID does not match command ID.");

        try
        {
            await _sender.SendAsync(command, ct);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        try
        {
            await _sender.SendAsync(new DeleteDogCommand(id), ct);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}