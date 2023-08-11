using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public class Customer : IHasCreationTime, IHasModificationTime, IMustHaveTenant
{        
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Address { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public DateTime DateOfBirth { get; private set; }

    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public Guid TenantId { get; set; }
    public Store Store { get; set; }

    public Customer(string fullName, string address, string phone, string email, DateTime dateOfBirth)
    {
        FullName = fullName;
        Address = address;
        Phone = phone;
        Email = email;
        DateOfBirth = dateOfBirth;
    }

    public void UpdateFullName(string fullName)
    {
        FullName = fullName;
    }

    public void UpdateAddress(string address)
    {
        Address = address;
    }

    public void UpdatePhone(string phone)
    {
        Phone = phone;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public void UpdateDateOfBirth(DateTime dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }
}