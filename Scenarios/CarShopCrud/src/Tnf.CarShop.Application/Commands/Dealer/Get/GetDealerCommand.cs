using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Dealer.Get;

public class GetDealerCommand
{
    public Guid? DealerId { get; set; }
}

public class GetDealerResult
{
    public GetDealerResult(DealerDto dealer)
    {
        Dealer = dealer;
    }

    public GetDealerResult(List<DealerDto> dealers)
    {
        Dealers = dealers;
    }

    public List<DealerDto> Dealers { get; set; }
    public DealerDto Dealer { get; set; }
}