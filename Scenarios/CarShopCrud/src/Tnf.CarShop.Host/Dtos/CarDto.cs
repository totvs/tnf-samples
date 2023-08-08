using System.Diagnostics.CodeAnalysis;

namespace Tnf.CarShop.Host.Dtos;

[ExcludeFromCodeCoverage]
internal sealed record CarDto
{
    public Guid Id { get; init; }
    public string Brand { get; init; }
    public string Model { get; init; }
    public int Year { get; init; }
    public decimal Price { get; init; }
    public DealerDto Dealer { get; init; }
    public CustomerDto Owner { get; init; }
    private decimal Discount { get; init; }
    public bool IsNew { get; init; }
    public bool IsOld { get; init; }
    public DateTime CreationTime { get; init; }
    public DateTime? LastModificationTime { get; init; }
}
