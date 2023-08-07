using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;


namespace Tnf.CarShop.Host.Commands.Dealer.Create
{
    public class CreateDealerCommandHandler : ICommandHandler<DealerCommand, DealerResult>
    {
        private readonly ILogger<CreateDealerCommandHandler> _logger;
        private readonly IDealerRepository _dealerRepository;

        public CreateDealerCommandHandler(ILogger<CreateDealerCommandHandler> logger, IDealerRepository dealerRepository)
        {
            _logger = logger;
            _dealerRepository = dealerRepository;
        }

        public async Task HandleAsync(ICommandContext<DealerCommand, DealerResult> context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var command = context.Command;

            var createdDealerId = await CreateDealerAsync(command, cancellationToken);

            context.Result = new DealerResult(createdDealerId, true);

            return;
        }

        private async Task<Guid> CreateDealerAsync(DealerCommand command, CancellationToken cancellationToken)
        {
            var newDealer = new Domain.Entities.Dealer(command.Name, command.Location);

            var createdDealer = await _dealerRepository.InsertAsync(newDealer, cancellationToken);

            return createdDealer.Id;
        }
    }
}