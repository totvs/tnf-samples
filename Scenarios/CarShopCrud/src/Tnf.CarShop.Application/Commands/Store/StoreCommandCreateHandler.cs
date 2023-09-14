using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Extensions;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandCreateHandler : CommandHandler<StoreCommandCreateAdmin, StoreResult>
{
    private readonly ILogger<StoreCommandCreateHandler> _logger;
    private readonly IStoreRepository _storeRepository;

    public StoreCommandCreateHandler(ILogger<StoreCommandCreateHandler> logger, IStoreRepository storeRepository)
    {
        _logger = logger;
        _storeRepository = storeRepository;
    }

    public override async Task<StoreResult> ExecuteAsync(StoreCommandCreateAdmin command, CancellationToken cancellationToken = default)
    {

        var store = new Domain.Entities.Store(command.Name, command.Cnpj, command.Location);

        store = await _storeRepository.InsertAsync(store, cancellationToken);

        _logger.EntitySuccessfullyCreated(nameof(store), store.Id);

        return new StoreResult { StoreDto = store.ToDto() };

    }

}
