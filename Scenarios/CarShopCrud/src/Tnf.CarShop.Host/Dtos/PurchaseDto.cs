using System.Diagnostics.CodeAnalysis;
using Tnf.CarShop.Domain.Entities;

namespace Tnf.CarShop.Host.Dtos;

[ExcludeFromCodeCoverage]
internal sealed record PurchaseDto
{
    public Guid Id { get; init; }
    public DateTime PurchaseDate { get; init; }
    public CustomerDto Customer { get; init; }
    public CarDto Car { get; init; }
    public decimal Price { get; init; }
    public DateTime CreationTime { get; init; }
}
