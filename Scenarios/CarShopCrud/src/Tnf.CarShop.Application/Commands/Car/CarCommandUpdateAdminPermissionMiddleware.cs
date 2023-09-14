using Tnf.Runtime.Session;

namespace Tnf.CarShop.Application.Commands.Car;
public class CarCommandUpdateAdminPermissionMiddleware : AdminPermissionMiddleware<CarCommandUpdateAdmin, CarResult>
{
    public CarCommandUpdateAdminPermissionMiddleware(IPrincipalAccessor principalAccessor) : base(principalAccessor)
    {
    }
}
