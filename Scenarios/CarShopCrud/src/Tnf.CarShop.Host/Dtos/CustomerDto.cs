using System.Diagnostics.CodeAnalysis;

namespace Tnf.CarShop.Host.Dtos;

[ExcludeFromCodeCoverage]
internal sealed record CustomerDto
{
    public Guid Id { get; init; }
    public string? FullName { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public DateTime DateOfBirth { get; init; }
    public ICollection<CarDto>? CarsOwned { get; init; }

    public DateTime CreationTime { get; init; }
    public DateTime? LastModificationTime { get; init; }
}
