namespace Tnf.CarShop.Application.Commands;
public interface IPermissionRequiredCommand
{
    bool MustBeAdmin { get; }
}
