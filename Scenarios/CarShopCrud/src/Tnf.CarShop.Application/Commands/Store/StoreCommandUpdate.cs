using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandUpdate : ICommand<StoreResult>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Cnpj { get; set; }
}

public class StoreCommandUpdateAdmin : StoreCommandUpdate, IPermissionRequiredCommand
{
    public bool MustBeAdmin { get; set; }
}
