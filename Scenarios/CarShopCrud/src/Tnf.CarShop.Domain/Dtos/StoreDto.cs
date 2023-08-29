namespace Tnf.CarShop.Domain.Dtos;

public class StoreDto
{
    public StoreDto(Guid id, string name, string location, string cnpj)
    {
        Id = id;
        Name = name;
        Location = location;
        Cnpj = cnpj;
    }

    public StoreDto()
    {
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Cnpj { get; set; }
}
