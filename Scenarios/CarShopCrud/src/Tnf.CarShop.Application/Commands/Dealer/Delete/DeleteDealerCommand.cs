namespace Tnf.CarShop.Host.Commands.Dealer.Delete;

public class DeleteDealerCommand
{
    public Guid DealerId { get; set; }
}

public class DeleteDealerResult
{
    public DeleteDealerResult(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }
}