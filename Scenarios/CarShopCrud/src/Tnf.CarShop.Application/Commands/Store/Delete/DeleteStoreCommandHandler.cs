using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store.Delete;

public class DeleteStoreCommandHandler : CommandHandler<DeleteStoreCommand, DeleteStoreResult>
{
    private readonly ILogger<DeleteStoreCommandHandler> _logger;
    private readonly IStoreRepository _storeRepository;

    public DeleteStoreCommandHandler(ILogger<DeleteStoreCommandHandler> logger, IStoreRepository dealerRepository)
    {
        _logger = logger;
        _storeRepository = dealerRepository;
    }

    public override async Task<DeleteStoreResult> ExecuteAsync(DeleteStoreCommand command,
        CancellationToken cancellationToken = default)
    {
        await _storeRepository.DeleteAsync(command.StoreId, cancellationToken);

        return new DeleteStoreResult(true);
    }
}