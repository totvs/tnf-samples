using Microsoft.Extensions.Logging;
using Tnf.CarShop.Domain.Repositories;
using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Fipe;

public class ApplyFipeTableCommandHandler : CommandHandler<ApplyFipeTableCommand, bool>
{
    private readonly IFipeRepository _fipeRepository;
    private readonly ILogger<ApplyFipeTableCommandHandler> _logger;

    public ApplyFipeTableCommandHandler(IFipeRepository fipeRepository, ILogger<ApplyFipeTableCommandHandler> logger)
    {
        _fipeRepository = fipeRepository;
        _logger = logger;
    }

    public override async Task<bool> ExecuteAsync(ApplyFipeTableCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var fipe = await _fipeRepository.GetByFipeCodeAsync(command.FipeCode, cancellationToken);

            if (fipe is null)
            {
                fipe = new Domain.Entities.Fipe(command.FipeCode, command.MonthYearReference, command.Brand, command.Model, command.Year, command.AveragePrice);
                await _fipeRepository.InsertAsync(fipe, cancellationToken);
                return true;
            }

            fipe.UpdateTable(command.MonthYearReference, command.Brand, command.Model, command.Year, command.AveragePrice);
            await _fipeRepository.UpdateAsync(fipe, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return false;
        }        
    }
}
