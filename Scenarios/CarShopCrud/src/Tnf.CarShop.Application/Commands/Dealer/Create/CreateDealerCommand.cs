using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Dealer.Create;

public class CreateDealerCommand
{
    public DealerDto Dealer { get; set; }
}

public class CreateDealerResult
{
    public CreateDealerResult(Guid dealerId)
    {
        DealerId = dealerId;
    }

    public Guid DealerId { get; set; }
}