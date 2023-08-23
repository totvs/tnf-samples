using Tnf.CarShop.Application.Dtos;

namespace Tnf.CarShop.Application.Commands.Store.Get;

public class GetStoreCommand
{
    public GetStoreCommand()
    {
    }

    public GetStoreCommand(Guid storeId)
    {
        StoreId = storeId;
    }

    public Guid StoreId { get; set; }
}

public class GetStoreResult
{
    public GetStoreResult(StoreDto store)
    {
        Store = store;
    }

    public GetStoreResult(List<StoreDto> stores)
    {
        Stores = stores;
    }

    public List<StoreDto> Stores { get; set; }
    public StoreDto Store { get; set; }
}
