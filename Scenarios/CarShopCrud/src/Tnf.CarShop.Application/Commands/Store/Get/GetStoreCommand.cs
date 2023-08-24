using Tnf.CarShop.Domain.Dtos;
using Tnf.Dto;

namespace Tnf.CarShop.Application.Commands.Store.Get;

public class GetStoreCommand
{
    public Guid? StoreId { get; set; }
    public RequestAllDto RequestAllStores { get; set; }
}

public class GetStoreResult
{
    public GetStoreResult(StoreDto store)
    {
        Store = store;
    }

    public GetStoreResult(IListDto<StoreDto> stores)
    {
        Stores = stores;
    }

    public IListDto<StoreDto> Stores { get; set; }
    public StoreDto Store { get; set; }
}
