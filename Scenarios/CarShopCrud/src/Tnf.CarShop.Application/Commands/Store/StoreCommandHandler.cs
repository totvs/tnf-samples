using Microsoft.Extensions.Logging;

using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Domain.Repositories;

using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandHandler : CommandHandler<StoreCommand, StoreResult>
{
    private readonly ILogger<StoreCommandHandler> _logger;
    private readonly IStoreRepository _storeRepository;

    public StoreCommandHandler(ILogger<StoreCommandHandler> logger, IStoreRepository storeRepository)
    {
        _logger = logger;
        _storeRepository = storeRepository;
    }

    public override async Task<StoreResult> ExecuteAsync(StoreCommand command, CancellationToken cancellationToken = default)
    {
        Domain.Entities.Store store;

        if (!command.Id.HasValue)
        {
            store = await InsertStoreAsync(command, cancellationToken);

            _logger.EntitySuccessfullyCreated(nameof(store), store.Id);

            return new StoreResult { StoreDto = store.ToDto() };
        }

        store = await UpdateStoreAsync(command, cancellationToken);

        _logger.EntitySuccessfullyUpdated(nameof(store), store.Id);

        return new StoreResult { StoreDto = store.ToDto() };
    }

    private async Task<Domain.Entities.Store> UpdateStoreAsync(StoreCommand command, CancellationToken cancellationToken)
    {
        var store = await _storeRepository.GetAsync(command.Id.Value, cancellationToken);

        if (store == null) throw new Exception($"Shop with id {command.Id} not found.");

        store.UpdateName(command.Name);
        store.UpdateLocation(command.Location);

        store = await _storeRepository.UpdateAsync(store, cancellationToken);

        return store;
    }

    private async Task<Domain.Entities.Store> InsertStoreAsync(StoreCommand command, CancellationToken cancellationToken)
    {
        var store = new Domain.Entities.Store(command.Name, command.Cnpj, command.Location);

        store = await _storeRepository.InsertAsync(store, cancellationToken);

        return store;
    }
}
