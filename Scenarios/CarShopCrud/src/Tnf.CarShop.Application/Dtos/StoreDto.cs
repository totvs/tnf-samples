namespace Tnf.CarShop.Application.Dtos;

public class StoreDto
{
    public StoreDto(Guid id, string name, string location, List<CarDto> cars)
    {
        Id = id;
        Name = name;
        Location = location;
        Cars = cars;
    }

    public StoreDto(Guid id, string name, string location)
    {
        Id = id;
        Name = name;
        Location = location;
    }

    public StoreDto(Guid id)
    {
        Id = id;
    }

    public StoreDto()
    {
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public List<CarDto> Cars { get; set; }
}
