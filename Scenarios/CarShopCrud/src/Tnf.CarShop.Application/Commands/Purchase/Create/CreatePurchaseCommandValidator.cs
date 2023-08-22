using FluentValidation;
using Tnf.CarShop.Application.Localization;

namespace Tnf.CarShop.Application.Commands.Purchase.Create;

public class CreatePurchaseCommandValidator : TnfFluentValidator<CreatePurchaseCommand>
{
    public override void Configure()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreatePurchaseCommand.Id));

        RuleFor(command => command.PurchaseDate)
            .LessThanOrEqualTo(DateTime.Now)
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.BeforeValidDate);

        RuleFor(command => command.CarId)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreatePurchaseCommand.CarId));

        RuleFor(command => command.StoreId)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreatePurchaseCommand.StoreId));

        RuleFor(command => command.CustomerId)
            .NotEmpty()
            .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreatePurchaseCommand.CustomerId));

        RuleFor(command => command.TenantId)
           .NotEmpty()
           .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreatePurchaseCommand.TenantId));

        RuleFor(command => command.Price)
           .NotEmpty()
           .WithTnfNotification(LocalizationSource.Default, LocalizationKeys.PropertyRequired, nameof(CreatePurchaseCommand.Price));
    }
}
