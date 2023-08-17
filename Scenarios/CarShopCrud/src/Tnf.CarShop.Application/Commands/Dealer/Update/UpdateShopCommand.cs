using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Dealer.Update;

public class UpdateShopCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
}

public class UpdateDealerResult
{
    public UpdateDealerResult(StoreDto dealer)
    {
        Dealer = dealer;
    }

    public StoreDto Dealer { get; set; }
}