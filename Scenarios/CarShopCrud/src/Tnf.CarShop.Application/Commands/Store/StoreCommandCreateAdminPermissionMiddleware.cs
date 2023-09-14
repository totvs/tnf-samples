using Tnf.Runtime.Session;

namespace Tnf.CarShop.Application.Commands.Store;
public class StoreCommandCreateAdminPermissionMiddleware : AdminPermissionMiddleware<StoreCommandCreateAdmin, StoreResult>
{
    public StoreCommandCreateAdminPermissionMiddleware(IPrincipalAccessor principalAccessor) : base(principalAccessor)
    {
    }
}
