namespace Tnf.CarShop.Application.Dtos;

public sealed record DealerDto
{
    public DealerDto(Guid id, string name, string location, List<CarDto> cars)
    {
        Id = id;
        Name = name;
        Location = location;
        Cars = cars;
    }

    public DealerDto(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Location { get; init; }
    public List<CarDto> Cars { get; init; }
}