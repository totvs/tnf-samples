using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store.Create;

public class CreateStoreCommandHandler : CommandHandler<CreateStoreCommand, CreateStoreResult>
{
    private readonly IStoreRepository _storeRepository;

    public CreateStoreCommandHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public override async Task<CreateStoreResult> ExecuteAsync(CreateStoreCommand command, CancellationToken cancellationToken = default)
    {
        var newStore = new Domain.Entities.Store(command.Name, command.Cnpj, command.Location);

        newStore = await _storeRepository.InsertAsync(newStore, cancellationToken);

        return new CreateStoreResult(newStore.Id);
    }
}
