using Tnf.Commands;

using Tnf.Repositories.Uow;

namespace Tnf.CarShop.Application.Commands;
public class TransactionMiddleware<TCommand, TResult> : ICommandMiddleware<TCommand, TResult>
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public TransactionMiddleware(IUnitOfWorkManager unitOfWorkManager)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task InvokeAsync(ICommandContext<TCommand, TResult> context, Func<Task> next, CancellationToken cancellationToken = default)
    {
        if (context.Command is ITransactionCommand)
        {
            using var uow = _unitOfWorkManager.Begin();

            await next();

            await uow.CompleteAsync(cancellationToken);
        }
        else
        {
            await next();
        }
    }
}
