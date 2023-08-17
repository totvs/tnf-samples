using FluentValidation;
//use xunit
namespace Tnf.CarShop.Application.Commands.Store.Get
{
    public class GetStoreCommandValidator : TnfFluentValidator<GetStoreCommand>
    {
        public override void Configure()
        {
            RuleFor(x => x.StoreId)
                .NotNull()
                .WithMessage("StoreId is required.");
        }
    }
}