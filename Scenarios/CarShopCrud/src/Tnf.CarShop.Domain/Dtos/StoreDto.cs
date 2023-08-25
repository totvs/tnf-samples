namespace Tnf.CarShop.Domain.Dtos;

public class StoreDto
{
    public StoreDto(Guid id, string name, string location)
    {
        Id = id;
        Name = name;
        Location = location;
    }

    public StoreDto()
    {
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
}
