using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;


namespace Tnf.CarShop.Host.Commands.Dealer.Create
{
    public class CreateDealerCommandHandler : ICommandHandler<CreateDealerCommand, CreateDealerResult>
    {
        private readonly ILogger<CreateDealerCommandHandler> _logger;
        private readonly IDealerRepository _dealerRepository;

        public CreateDealerCommandHandler(ILogger<CreateDealerCommandHandler> logger, IDealerRepository dealerRepository)
        {
            _logger = logger;
            _dealerRepository = dealerRepository;
        }

        public async Task HandleAsync(ICommandContext<CreateDealerCommand, CreateDealerResult> context,
            CancellationToken cancellationToken = new())
        {
            var dealerDto = context.Command.Dealer;
            
            var createdDealerId = await CreateDealerAsync(dealerDto, cancellationToken);

            context.Result = new CreateDealerResult(createdDealerId);

            return;
        }

        private async Task<Guid> CreateDealerAsync(DealerDto command, CancellationToken cancellationToken)
        {
            var newDealer = new Domain.Entities.Dealer(command.Name, command.Location);

            var createdDealer = await _dealerRepository.InsertAsync(newDealer, cancellationToken);

            return createdDealer.Id;
        }
    }
}