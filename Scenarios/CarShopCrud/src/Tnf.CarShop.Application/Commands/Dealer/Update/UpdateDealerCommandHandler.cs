using Microsoft.Extensions.Logging;
using Tnf.CarShop.Application.Commands.Dealer.Update;
using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.CarShop.Host.Commands.Dealer;
using Tnf.Commands;

public class UpdateDealerCommandHandler : ICommandHandler<UpdateDealerCommand, UpdateDealerResult>
{
    private readonly ILogger<UpdateDealerCommandHandler> _logger;
    private readonly IDealerRepository _dealerRepository;
    private readonly DealerFactory _dealerFactory;

    public UpdateDealerCommandHandler(ILogger<UpdateDealerCommandHandler> logger, IDealerRepository dealerRepository)
    {
        _logger = logger;
        _dealerRepository = dealerRepository;
    }

    public async Task HandleAsync(ICommandContext<UpdateDealerCommand, UpdateDealerResult> context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var dealerDto = context.Command.Dealer;

        var dealer = await _dealerRepository.GetAsync(dealerDto.Id, cancellationToken);

        if (dealer == null)
        {
            throw new Exception($"Dealer with id {dealerDto.Id} not found.");
        }

        dealer.UpdateName(dealerDto.Name);
        dealer.UpdateLocation(dealerDto.Location);
        
        var updatedDealer = await _dealerRepository.UpdateAsync(dealer, cancellationToken);
        
        context.Result = new UpdateDealerResult(_dealerFactory.ToDto(updatedDealer));
    }
}