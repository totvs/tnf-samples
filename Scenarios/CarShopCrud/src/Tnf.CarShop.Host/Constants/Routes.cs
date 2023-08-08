namespace Dap.Api;

internal sealed record Routes
{
    public const string AppConfiguration = "api/dap/v{version:apiVersion}/app-configuration";
    public const string Products = "api/dap/v{version:apiVersion}/products";
    public const string EventHistory = "api/dap/v{version:apiVersion}/event-history";
}
