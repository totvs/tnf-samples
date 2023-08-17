using FluentValidation;

namespace Tnf.CarShop.Application.Commands.Store.Delete;
//use Xunit
public class DeleteStoreCommandValidator : TnfFluentValidator<DeleteStoreCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.StoreId)
            .NotEmpty().WithMessage("StoreId is required.");
    }
}
