using Tnf.Commands;
using TnfZero.Domain.Repositories;

namespace TnfZero.Application.Commands.DeleteDog;

public class DeleteDogCommandHandler : CommandHandler<DeleteDogCommand>
{
    private readonly IDogRepository _repository;

    public DeleteDogCommandHandler(IDogRepository repository)
    {
        _repository = repository;
    }

    public override async Task ExecuteAsync(
        DeleteDogCommand command,
        CancellationToken cancellationToken = default)
    {
        var entity = await _repository.FindByIdAsync(command.Id, cancellationToken);
        if (entity is null)
            throw new InvalidOperationException($"Dog with ID {command.Id} not found.");

        await _repository.DeleteAsync(entity, cancellationToken);
    }
}