using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Dealer.Update;

public class UpdateDealerCommand
{
    public DealerDto Dealer { get; set; }
}

public class UpdateDealerResult
{
    public UpdateDealerResult(DealerDto dealer)
    {
        Dealer = dealer;
    }

    public DealerDto Dealer { get; set; }
}