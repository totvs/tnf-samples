namespace Tnf.CarShop.Application.Dtos;

public sealed record PurchaseDto
{
    public Guid Id { get; init; }
    public DateTime PurchaseDate { get; init; }
    public CustomerDto Customer { get; init; }
    public CarDto Car { get; init; }
}