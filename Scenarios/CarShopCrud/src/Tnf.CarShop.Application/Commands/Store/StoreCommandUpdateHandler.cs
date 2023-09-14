using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandUpdateHandler : CommandHandler<StoreCommandUpdateAdmin, StoreResult>
{
    private readonly ILogger<StoreCommandUpdateHandler> _logger;
    private readonly IStoreRepository _storeRepository;

    public StoreCommandUpdateHandler(ILogger<StoreCommandUpdateHandler> logger, IStoreRepository storeRepository)
    {
        _logger = logger;
        _storeRepository = storeRepository;
    }

    public override async Task<StoreResult> ExecuteAsync(StoreCommandUpdateAdmin command, CancellationToken cancellationToken = default)
    {
        var store = await _storeRepository.GetAsync(command.Id.Value, cancellationToken);

        if (store == null) throw new Exception($"Shop with id {command.Id} not found.");

        store.UpdateName(command.Name);
        store.UpdateLocation(command.Location);

        store = await _storeRepository.UpdateAsync(store, cancellationToken);

        _logger.EntitySuccessfullyUpdated(nameof(store), store.Id);

        return new StoreResult { StoreDto = store.ToDto() };
    }
}
