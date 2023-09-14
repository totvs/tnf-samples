using Tnf.Runtime.Session;

namespace Tnf.CarShop.Application.Commands.Car;
public class CarCommandCreateAdminPermissionMiddleware : AdminPermissionMiddleware<CarCommandCreateAdmin, CarResult>
{
    public CarCommandCreateAdminPermissionMiddleware(IPrincipalAccessor principalAccessor) : base(principalAccessor)
    {
    }
}
