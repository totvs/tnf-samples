using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Dealer.Create;

public class CreateDealerCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
}

public class CreateDealerResult
{
    public CreateDealerResult(Guid dealerId)
    {
        DealerId = dealerId;
    }

    public Guid DealerId { get; set; }
}