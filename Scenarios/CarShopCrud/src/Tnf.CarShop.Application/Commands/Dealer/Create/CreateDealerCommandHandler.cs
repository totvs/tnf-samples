using Tnf.CarShop.Application.Dtos;
using Tnf.CarShop.Application.Factories;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Dealer.Create;

public class CreateDealerCommandHandler : CommandHandler<CreateDealerCommand, CreateDealerResult>
{
    private readonly DealerFactory _dealerFactory;
    private readonly IDealerRepository _dealerRepository;

    public CreateDealerCommandHandler(IDealerRepository dealerRepository, DealerFactory dealerFactory)
    {
        _dealerRepository = dealerRepository;
        _dealerFactory = dealerFactory;
    }

    public override async Task<CreateDealerResult> ExecuteAsync(CreateDealerCommand command, CancellationToken cancellationToken = default)
    {
        var dealerDto = command.Dealer;

        var createdDealerId = await CreateDealerAsync(dealerDto, cancellationToken);

        return new CreateDealerResult(createdDealerId);
    }    

    private async Task<Guid> CreateDealerAsync(DealerDto dealerDto, CancellationToken cancellationToken)
    {
        var newDealer = _dealerFactory.ToEntity(dealerDto);

        var createdDealer = await _dealerRepository.InsertAsync(newDealer, cancellationToken);

        return createdDealer.Id;
    }
}