using Tnf.CarShop.Domain.Dtos;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreResult : AdminResult
{
    public StoreDto StoreDto { get; set; }
}
