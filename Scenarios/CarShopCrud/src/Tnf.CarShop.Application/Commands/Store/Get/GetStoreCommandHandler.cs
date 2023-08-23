using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store.Get;

public class GetStoreCommandHandler : CommandHandler<GetStoreCommand, GetStoreResult>
{
    private readonly ILogger<GetStoreCommandHandler> _logger;
    private readonly IStoreRepository _storeRepository;

    public GetStoreCommandHandler(ILogger<GetStoreCommandHandler> logger, IStoreRepository storeRepository)
    {
        _logger = logger;
        _storeRepository = storeRepository;
    }

    public override async Task<GetStoreResult> ExecuteAsync(GetStoreCommand command,
        CancellationToken cancellationToken = default)
    {
        if (Guid.TryParse(command.StoreId.ToString(), out Guid tenantId))
        {
            var store = await _storeRepository.GetAsync(tenantId, cancellationToken);

            if (store is null)
                throw new Exception($"Store with id {command} not found.");

            var storeDto = new StoreDto(store.Id, store.Name, store.Location);


            return new GetStoreResult(storeDto);
        }

        var stores = await _storeRepository.GetAllAsync(cancellationToken);

        var storesDto = stores.Select(x => new StoreDto(x.Id, x.Name, x.Location)).ToList();

        return new GetStoreResult(storesDto);
    }
}
