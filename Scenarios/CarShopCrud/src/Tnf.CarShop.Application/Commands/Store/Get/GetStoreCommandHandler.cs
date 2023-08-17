using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store.Get;

public class GetStoreCommandHandler : CommandHandler<GetStoreCommand, GetStoreResult>
{
    private readonly IStoreRepository _storeRepository;
    private readonly ILogger<GetStoreCommandHandler> _logger;

    public GetStoreCommandHandler(ILogger<GetStoreCommandHandler> logger, IStoreRepository storeRepository)
    {
        _logger = logger;
        _storeRepository = storeRepository;
    }

    public override async Task<GetStoreResult> ExecuteAsync(GetStoreCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.StoreId.HasValue)
        {
            var store = await _storeRepository.GetAsync(command.StoreId.Value, cancellationToken);

            if (store is null) throw new Exception($"Store with id {command} not found.");

            var storeDto = new StoreDto(store.TenantId, store.Name, store.Location);



            return new GetStoreResult(storeDto);
        }

        var dealers = await _storeRepository.GetAllAsync(cancellationToken);

        var dealersDto = dealers.Select(dealer => new StoreDto(dealer.TenantId, dealer.Name, dealer.Location)).ToList();

        return new GetStoreResult(dealersDto);
    }


}