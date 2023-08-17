using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Store.Get;

public class GetStoreCommand
{
    public Guid? StoreId { get; set; }
}

public class GetStoreResult
{
    public GetStoreResult(StoreDto dealer)
    {
        Store = dealer;
    }

    public GetStoreResult(List<StoreDto> stores)
    {
        Stores = stores;
    }

    public List<StoreDto> Stores { get; set; }
    public StoreDto Store { get; set; }
}