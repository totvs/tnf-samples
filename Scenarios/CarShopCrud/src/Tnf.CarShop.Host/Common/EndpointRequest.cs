using System.Security.Claims;

namespace Tnf.CarShop.Host.Common;

public abstract record EndpointRequest : IEndpointRequest
{
    //private Guid? _correlatedObjectId;

    //public required ClaimsPrincipal ClaimsPrincipal { get; init; }

    //public Guid CorrelatedObjectId => _correlatedObjectId ??= GetCorrelatedObjectId();

    //private Guid GetCorrelatedObjectId()
    //{
    //    var identity = ClaimsPrincipal.Identity as ClaimsIdentity;
    //    var correlatedIdClaim = identity?.Claims.SingleOrDefault(claim => claim.Type == nameof(APIKey.CorrelatedObjectID));

    //    if (correlatedIdClaim is not null && Guid.TryParse(correlatedIdClaim.Value, out var correlatedIdGuid))
    //    {
    //        return correlatedIdGuid;
    //    }

    //    throw new InvalidOperationException("Every ApiKey should have a CorrelatedObjectId");
    //}
}
