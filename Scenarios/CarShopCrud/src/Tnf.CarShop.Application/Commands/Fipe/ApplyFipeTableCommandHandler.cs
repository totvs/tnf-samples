using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Fipe;

public class ApplyFipeTableCommandHandler : CommandHandler<ApplyFipeTableCommand>
{
    private readonly IFipeRepository _fipeRepository;

    public ApplyFipeTableCommandHandler(IFipeRepository fipeRepository)
    {
        _fipeRepository = fipeRepository;
    }

    public override Task ExecuteAsync(ApplyFipeTableCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
