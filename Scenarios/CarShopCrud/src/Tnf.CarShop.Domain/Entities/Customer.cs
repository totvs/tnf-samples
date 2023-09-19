using Tnf.CarShop.Domain.Dtos;
using Tnf.Repositories.Entities;
using Tnf.Repositories.Entities.Auditing;

namespace Tnf.CarShop.Domain.Entities;

public class Customer : IHasCreationTime, IHasModificationTime, IMustHaveTenant
{
    public Guid Id { get; set; }
    public string FullName { get; private set; }
    public string Address { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public DateTime DateOfBirth { get; private set; }

    public Guid StoreId { get; set; }
    public Store Store { get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
    
    public Guid TenantId { get; set; }

    public Customer(string fullName, string address, string phone, string email, DateTime dateOfBirth, Guid storeId)
    {
        FullName = fullName;
        Address = address;
        Phone = phone;
        Email = email;
        DateOfBirth = dateOfBirth;
        StoreId = storeId;
    }    

    public void UpdateFullName(string fullName)
    {
        if (fullName.IsNullOrEmpty())
            return;

        FullName = fullName;
    }

    public void UpdateAddress(string address)
    {
        if (address.IsNullOrEmpty())
            return;

        Address = address;
    }

    public void UpdatePhone(string phone)
    {
        if (phone.IsNullOrEmpty())
            return;

        Phone = phone;
    }

    public void UpdateEmail(string email)
    {
        if (email.IsNullOrEmpty())
            return;

        Email = email;
    }

    public void UpdateDateOfBirth(DateTime dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }

    public CustomerDto ToDto()
    {
        return new CustomerDto(
            Id,
            FullName,
            Address,
            Phone,
            Email,
            DateOfBirth,
            StoreId);
    }
}
