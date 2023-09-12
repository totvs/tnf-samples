namespace Tnf.CarShop.Application.Commands.Purchase;
public class PurchaseCommandUpdate
{
    public Guid? Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public Guid StoreId { get; set; }
    public Guid CarId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Price { get; set; }
}
