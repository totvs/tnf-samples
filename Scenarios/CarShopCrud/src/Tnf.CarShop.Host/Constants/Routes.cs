namespace Tnf.CarShop.Host.Constants;

internal sealed record Routes
{
    public const string Car = "api/carshop/v{version:apiVersion}/cars";
    public const string Customer = "api/carshop/v{version:apiVersion}/customers";
    public const string Dealer = "api/carshop/v{version:apiVersion}/dealers";
    public const string Purchase = "api/carshop/v{version:apiVersion}/purchases";
}
