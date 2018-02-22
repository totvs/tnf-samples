using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Case6.Infra.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DbProviderFactory providerFactory;

        public CustomerService(DbProviderFactory providerFactory)
        {
            this.providerFactory = providerFactory;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            using (var connection = providerFactory.CreateConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT * FROM CustomerFromADO";

                    using (var reader = command.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        while (reader.Read())
                        {
                            customers.Add(reader.MapTo<Customer>());
                        }
                    }
                }
            }

            return customers;
        }
    }
}
