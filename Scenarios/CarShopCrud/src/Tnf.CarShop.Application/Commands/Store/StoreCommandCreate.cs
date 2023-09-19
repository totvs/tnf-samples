using Tnf.CarShop.Application.Commands.Purchase;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandCreate : ICommand<StoreResult>, ITransactionCommand
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Cnpj { get; set; }
}

public class StoreCommandCreateAdmin : StoreCommandCreate, IPermissionRequiredCommand
{
    public bool MustBeAdmin { get; set; }
}
