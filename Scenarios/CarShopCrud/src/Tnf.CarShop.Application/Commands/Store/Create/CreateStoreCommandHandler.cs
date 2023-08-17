using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Entities;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store.Create;

public class CreateStoreCommandHandler : CommandHandler<CreateStoreCommand, CreateStoreResult>
{
    public Guid Id { get; set; }

    private readonly IStoreRepository _storeRepository;

    public CreateStoreCommandHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;        
    }

    public override async Task<CreateStoreResult> ExecuteAsync(CreateStoreCommand command,
        CancellationToken cancellationToken = default)
    {        
        var createdDealerId = await CreateShopAsync(command, cancellationToken);

        return new CreateStoreResult(createdDealerId);
    }

    private async Task<Guid> CreateShopAsync(CreateStoreCommand shopCommand, CancellationToken cancellationToken)
    {
        var newDealer = new Domain.Entities.Store(shopCommand.Name, shopCommand.Cnpj, shopCommand.Location);

        var createdDealer = await _storeRepository.InsertAsync(newDealer, cancellationToken);

        return createdDealer.TenantId;
    }
}