namespace Tnf.CarShop.Application.Dtos;

public class DealerDto
{
    public DealerDto(Guid id, string name, string location, List<CarDto> cars)
    {
        Id = id;
        Name = name;
        Location = location;
        Cars = cars;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public List<CarDto> Cars { get; set; }
}