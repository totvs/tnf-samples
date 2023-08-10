namespace Tnf.CarShop.Host.Common;

public interface IEndpoint
{
}

public interface IEndpoint<in TRequest, TResponse> : IEndpoint
    where TRequest : IEndpointRequest
    where TResponse : IResult
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
