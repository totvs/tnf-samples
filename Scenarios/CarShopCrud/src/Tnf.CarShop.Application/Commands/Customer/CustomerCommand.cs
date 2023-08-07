namespace Tnf.CarShop.Host.Commands.Customer
{
    public class CustomerCommand
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public DateOnly DateOfBirthDay { get; set; }
    }

    public class CustomerResult
    {
        public CustomerResult(Guid createdCustomerId, bool success)
        {
            CustomerId = createdCustomerId;
            Success = success;
        }

        public Guid CustomerId { get; set; }
        public bool Success { get; set; }
    }
}