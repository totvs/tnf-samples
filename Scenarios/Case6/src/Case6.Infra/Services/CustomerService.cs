using System.Collections.Generic;

namespace Case6.Infra.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDbConnector connector;

        public CustomerService(IDbConnector connector)
        {
            this.connector = connector;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = connector.CreateReader<Customer>(@"select * from customerfromado");

            return customers;
        }
    }
}
