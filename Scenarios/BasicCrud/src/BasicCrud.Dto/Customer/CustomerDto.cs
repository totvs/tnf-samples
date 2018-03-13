using System;
using Tnf.Dto;

namespace BasicCrud.Dto.Customer
{
    public class CustomerDto : DtoBase<Guid>
    {
        public static CustomerDto NullInstance = new CustomerDto().AsNullable<CustomerDto, Guid>();

        public string Name { get; set; }
    }
}
