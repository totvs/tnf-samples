using Microsoft.Extensions.Options;

namespace Tnf.CarShop.Application.DependencyInjection;

public class RabbitMqOptions
{
    public string HostName { get; set; }
    public string VirtualHost { get; set; } = "/";
    public int HostPort { get; set; } = 5672;
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class RabbitMqOptionsValidator : IValidateOptions<RabbitMqOptions>
{
    public ValidateOptionsResult Validate(string name, RabbitMqOptions options)
    {
        var errors = new List<string>();
        Func<string, string> requiredField = propName => $"{nameof(RabbitMqOptions)} must have {propName}";

        if (options.HostName.IsNullOrWhiteSpace())
        {
            errors.Add(requiredField(nameof(options.HostName)));
        }

        if (options.VirtualHost.IsNullOrWhiteSpace())
        {
            errors.Add(requiredField(nameof(options.VirtualHost)));
        }

        if (options.UserName.IsNullOrWhiteSpace())
        {
            errors.Add(requiredField(nameof(options.UserName)));
        }

        if (options.Password.IsNullOrWhiteSpace())
        {
            errors.Add(requiredField(nameof(options.Password)));
        }

        if (options.HostPort <= 0)
        {
            errors.Add($"Invalid {nameof(options.HostPort)} {options.HostPort}");
        }

        return errors.Any() ?
            ValidateOptionsResult.Fail(errors) :
            ValidateOptionsResult.Success;
    }
}
