using FluentAssertions;
using FluentValidation.Results;

namespace Tnf.CarShop.Application.Tests.Commands;
public class TestBase
{   
    public static void ValidateAddressTooLong(ValidationResult result)
    {
        result.Errors.Should().Contain(e => e.ErrorMessage == "'Address' must be between 5 and 250 characters. You entered 251 characters.");
    }

  
    public static void ValidateValidEmail(ValidationResult result)
    {
        result.Errors.Should().Contain(e => e.ErrorMessage == "'Email' is not a valid email address.");
    }


    public static void ValidateSizeFullName(ValidationResult result, int size)
    {
        result.Errors.Should().Contain(e => e.ErrorMessage == "'Full Name' must be between 2 and 150 characters. You entered "+ size.ToString() + " characters.");
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

    public static void ValidateGenericMessage(ValidationResult result, string mensagem)
    {
        result.Errors.Should().Contain(e => e.ErrorMessage == mensagem);
    }

    public static void ValidatePropertyValue(ValidationResult result, string property, string value)
    {
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors,
            e => e.PropertyName == property && e.ErrorMessage == $"'{property}' must be greater than '{value}'.");
    }

}
