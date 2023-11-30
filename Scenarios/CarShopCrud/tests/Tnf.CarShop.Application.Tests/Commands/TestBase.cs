using FluentValidation.Results;

namespace Tnf.CarShop.Application.Tests.Commands;
public class TestBase
{   
    public static void ValidateValidEmail(ValidationResult result, string property)
    {
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == property &&
                 e.ErrorMessage == $"'{property}' is not a valid email address.");
    }

    public static void ValidatePropertySize(ValidationResult result, string property, int size, string minLength, string maxLength)
    {
        var propertyName = property.Replace(" ", "");
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == propertyName &&
                 e.ErrorMessage == $"'{property}' must be between {minLength} and {maxLength} characters. You entered {size} characters.");
    }

    public static void ValidateNullOrEmpty(ValidationResult result, string property)
    {
        var propertyName = property.Replace(" ", "");
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors,
            e => e.PropertyName == propertyName && e.ErrorMessage == $"'{property}' must not be empty.");
    }

    public static void ValidatePropertyGreaterThanValue(ValidationResult result, string property, string value)
    {
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == property && e.ErrorMessage == $"'{property}' must be greater than '{value}'.");
    }

    public static void ValidatePropertyLessThanValue(ValidationResult result, string property, string value)
    {
        var propertyName = property.Replace(" ", "");
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == propertyName && e.ErrorMessage == $"'{property}' must be less than '{value}'.");
    }

    public static void ValidatePropertyFormat(ValidationResult result, string property)
    {
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors,
            e => e.PropertyName == property && e.ErrorMessage == $"'{property}' is not in the correct format.");
    }

    public static void ValidatePropertyConditionNotMet(ValidationResult result, string property)
    {
        var propertyName = property.Replace(" ", "");
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors,
            e => e.PropertyName == propertyName && e.ErrorMessage == $"The specified condition was not met for '{property}'.");
    }
}
