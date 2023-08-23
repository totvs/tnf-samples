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
        var storeId = await CreateShopAsync(command, cancellationToken);

        return new CreateStoreResult(storeId);
    }

    private async Task<Guid> CreateShopAsync(CreateStoreCommand shopCommand, CancellationToken cancellationToken)
    {
        var newStore = new Domain.Entities.Store(shopCommand.Name, shopCommand.Cnpj, shopCommand.Location);

        newStore = await _storeRepository.InsertAsync(newStore, cancellationToken);

        return newStore.Id;
    }
}
