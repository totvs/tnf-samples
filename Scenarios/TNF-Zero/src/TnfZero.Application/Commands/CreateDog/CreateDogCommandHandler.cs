using Tnf.Commands;
using TnfZero.Domain.Entities;
using TnfZero.Domain.Repositories;

namespace TnfZero.Application.Commands.CreateDog;

public class CreateDogCommandHandler : CommandHandler<CreateDogCommand, Guid>
{
    private readonly IDogRepository _repository;

    public CreateDogCommandHandler(IDogRepository repository)
    {
        _repository = repository;
    }

    public override async Task<Guid> ExecuteAsync(
        CreateDogCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new DogEntity(command.Name);
            await _repository.InsertAsync(entity, cancellationToken);
            return entity.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}