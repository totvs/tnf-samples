namespace Tnf.CarShop.Application.Commands.Purchase;

//Exemplo de um comando que não implementa um ICommand
public class PurchaseCommandCreate: ITransactionCommand
{
    public DateTime PurchaseDate { get; set; }
    public Guid StoreId { get; set; }
    public Guid CarId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Price { get; set; }
}
