﻿using Tnf.Commands;

namespace Tnf.CarShop.Application.Commands.Customer;

public class CustomerCommandCreate: ICommand<CustomerResult>, ITransactionCommand
{
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Guid StoreId { get; set; }
}
