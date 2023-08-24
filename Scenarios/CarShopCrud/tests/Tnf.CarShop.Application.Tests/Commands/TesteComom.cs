using System;
using FluentAssertions;
using FluentValidation.Results;

namespace Tnf.CarShop.Application.Tests.Commands;
public class TesteComom
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

    public static void ValidateEmpty(ValidationResult result, string property) {

        result.Errors.Should().Contain(e => e.ErrorMessage == "'" + property + "' must not be empty.");
    }

    public static void ValidateGenericMessage(ValidationResult result, string mensagem)
    {
        result.Errors.Should().Contain(e => e.ErrorMessage == mensagem);
    }

}
