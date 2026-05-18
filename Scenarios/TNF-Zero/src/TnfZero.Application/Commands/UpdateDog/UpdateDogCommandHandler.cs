using Tnf.Commands;
using TnfZero.Domain.Repositories;

namespace TnfZero.Application.Commands.UpdateDog;

public class UpdateDogCommandHandler : CommandHandler<UpdateDogCommand>
{
    private readonly IDogRepository _repository;

    public UpdateDogCommandHandler(IDogRepository repository)
    {
        _repository = repository;
    }

    public override async Task ExecuteAsync(
        UpdateDogCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await _repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
            throw new InvalidOperationException($"Dog with ID {command.Id} not found.");

        entity.Name = command.Name;
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}