using System;
using Tnf.Dto;

namespace BasicCrud.Dto.Customer
{
    public class CustomerDto : BaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
