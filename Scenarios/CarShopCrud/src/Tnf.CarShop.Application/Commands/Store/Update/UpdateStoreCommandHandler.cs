using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store.Update;

public class UpdateStoreCommandHandler : CommandHandler<UpdateStoreCommand, UpdateStoreResult>
{
    private readonly IStoreRepository _StoreRepository;
    private readonly ILogger<UpdateStoreCommandHandler> _logger;

    public UpdateStoreCommandHandler(ILogger<UpdateStoreCommandHandler> logger, IStoreRepository storeRepository)
    {
        _logger = logger;
        _StoreRepository = storeRepository;
    }

    public override async Task<UpdateStoreResult> ExecuteAsync(UpdateStoreCommand command,
        CancellationToken cancellationToken = default)
    {

        var store = await _StoreRepository.GetAsync(command.Id, cancellationToken);

        if (store == null) throw new Exception($"Shop with id {command.Id} not found.");

        store.UpdateName(command.Name);
        store.UpdateLocation(command.Location);

        var updatedDealer = await _StoreRepository.UpdateAsync(store, cancellationToken);

        var storeDto = new StoreDto(updatedDealer.TenantId, updatedDealer.Name, updatedDealer.Location);

        return new UpdateStoreResult(storeDto);
    }
}