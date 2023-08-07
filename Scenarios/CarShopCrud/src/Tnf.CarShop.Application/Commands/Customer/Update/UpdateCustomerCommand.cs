using Tnf.CarShop.Application.Dtos;

public class UpdateCustomerCommand
{
    public CustomerDto Customer { get; set; }
}

public class UpdateCustomerResult
{
    public UpdateCustomerResult(CustomerDto customer)
    {
        Customer = customer;
    }

    public CustomerDto Customer { get; set; }
}