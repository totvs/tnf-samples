﻿namespace Tnf.CarShop.Domain.Dtos;

public class CustomerDto
{
    public CustomerDto(
        Guid id,
        string fullName,
        string address,
        string phone,
        string email,
        DateTime dateOfBirth,
        Guid storeId)
    {
        Id = id;
        FullName = fullName;
        Address = address;
        Phone = phone;
        Email = email;
        DateOfBirth = dateOfBirth;
        StoreId = storeId;
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
    public Guid StoreId { get; set; }
}
