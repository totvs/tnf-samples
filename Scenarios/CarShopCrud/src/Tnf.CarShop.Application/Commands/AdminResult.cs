using Newtonsoft.Json;

namespace Tnf.CarShop.Application.Commands;
public abstract class AdminResult
{
    [JsonIgnore]
    public bool IsUnauthorized { get; set; } = false;

    public static TResult Unauthorized<TResult>()
        where TResult : AdminResult, new()
    {
        return new TResult
        {
            IsUnauthorized = true
        };
    }
}
