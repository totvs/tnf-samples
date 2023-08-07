using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Dealer.Get;

public class GetDealerCommand
{
    public Guid DealerId { get; set; }
}

public class GetDealerResult
{
    public GetDealerResult(DealerDto dealer)
    {
        Dealer = dealer;
    }

    public DealerDto Dealer { get; set; }
}