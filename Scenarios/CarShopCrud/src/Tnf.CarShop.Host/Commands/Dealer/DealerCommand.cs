namespace Tnf.CarShop.Host.Commands.Dealer.Create
{
    public class CreateDealerCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class DealerResult
    {
        public DealerResult(Guid createdDealerId, bool success)
        {
            DealerId = createdDealerId;
            Success = success;
        }

        public Guid DealerId { get; set; }
        public bool Success { get; set; }
    }
}