using Tnf.Commands;
using Tnf.Runtime.Session;

namespace Tnf.CarShop.Application.Commands;
public abstract class AdminPermissionMiddleware<TCommand, TResult> : CommandMiddleware<TCommand, TResult>
        where TCommand : IPermissionRequiredCommand
        where TResult : AdminResult, new()
{
    private readonly IPrincipalAccessor _principalAccessor;

    public AdminPermissionMiddleware(IPrincipalAccessor principalAccessor)
    {
        _principalAccessor = principalAccessor;
    }


    public override async Task<TResult> InvokeAsync(
           TCommand command,
           Func<Task<TResult>> next,
           CancellationToken cancellationToken = default)
    {
        if (command.MustBeAdmin)
        {
            if (!_principalAccessor.Principal.HasClaim("authorities", "admin"))
            {
                return AdminResult.Unauthorized<TResult>();
            }
        }

        return await next();

    }

}
