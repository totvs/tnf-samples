using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandCreate : ICommand<StoreResult>, ITransactionCommand
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Cnpj { get; set; }
}
