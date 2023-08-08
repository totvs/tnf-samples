namespace Tnf.CarShop.Application.Dtos;


public sealed record CustomerDto
{
    public CustomerDto(Guid id, string fullName, string address, string phone, List<CarDto> cars, string email, DateOnly dateOfBirth)
    {
        Id = id;
        FullName = fullName;
        Address = address;
        Phone = phone;
        Cars = cars;
        Email = email;
        DateOfBirth = dateOfBirth;
    }

    public CustomerDto(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
    public string FullName { get; init; }
    public string Address { get; init; }
    public string Phone { get; init; }
    public string Email { get; init; }
    
    public DateOnly DateOfBirth { get; init; }
    public List<CarDto> Cars { get; init; }
}
