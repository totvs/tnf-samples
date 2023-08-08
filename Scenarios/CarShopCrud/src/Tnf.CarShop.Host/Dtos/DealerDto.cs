using System.Diagnostics.CodeAnalysis;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Host.Dtos;

[ExcludeFromCodeCoverage]
internal sealed record DealerDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Location { get; init; }
    public ICollection<CarDto>? Cars { get; init; }
    public DateTime CreationTime { get; init; }
    public DateTime? LastModificationTime { get; init; }
}
