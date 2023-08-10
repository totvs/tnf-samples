using AutoMapper;
using Tnf.AspNetCore.Mvc.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Tnf.CarShop.Host.Common;

public abstract class Endpoint<TRequest, TResponse> : IEndpoint<TRequest, TResponse>
    where TRequest : EndpointRequest
    where TResponse : IResult
{
    protected Endpoint(IMapper mapper)
    {
        Mapper = mapper;
    }

    protected IMapper Mapper { get; }

    public abstract Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);

    protected Ok<TOkResponse> Ok<TOkResponse>(object response) => TypedResults.Ok(Mapper.Map<TOkResponse>(response));

    protected BadRequest<ErrorResponse> BadRequest(IEnumerable<Error> errors) => BadRequestCore(errors);

    protected BadRequest<ErrorResponse> BadRequest(params Error[] errors) => BadRequestCore(errors);

    protected NotFound<ErrorResponse> NotFound(IEnumerable<Error> errors) => TypedResults.NotFound(Mapper.MapToErrorResponse(errors));

    private BadRequest<ErrorResponse> BadRequestCore(IEnumerable<Error> errors) => TypedResults.BadRequest(Mapper.MapToErrorResponse(errors));
}
