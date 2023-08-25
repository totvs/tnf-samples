using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Dtos;
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

    public override async Task<GetStoreResult> ExecuteAsync(GetStoreCommand command, CancellationToken cancellationToken = default)
    {
        if (command.StoreId.HasValue)
        {
            var store = await _storeRepository.GetAsync(command.StoreId.Value, cancellationToken);

            if (store is null)
                return null;

            var storeDto = new StoreDto(store.Id, store.Name, store.Location);

            return new GetStoreResult(storeDto);
        }

        var storesDto = await _storeRepository.GetAllAsync(command.RequestAllStores, cancellationToken);        

        return new GetStoreResult(storesDto);
    }
}
