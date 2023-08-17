namespace Tnf.CarShop.Application.Dtos;

public sealed record CustomerDto
{
    public CustomerDto(Guid id, string fullName, string address, string phone, List<CarDto> cars, string email,
        DateTime dateOfBirth)
    {
        Id = id;
        FullName = fullName;
        Address = address;
        Phone = phone;
        Cars = cars;
        Email = email;
        DateOfBirth = dateOfBirth;
    }

    public CustomerDto(Guid id, string fullName, string address, string phone, string email,
        DateTime dateOfBirth)
    {
        Id = id;
        FullName = fullName;
        Address = address;
        Phone = phone;
        Email = email;
        DateOfBirth = dateOfBirth;
    }

    public CustomerDto(Guid id)
    {
        Id = id;
    }

    public CustomerDto()
    {
    }

    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public DateTime DateOfBirth { get; set; }
    public List<CarDto> Cars { get; set; }
}