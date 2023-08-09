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

    public DealerDto()
    {
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public List<CarDto> Cars { get; set; }
}