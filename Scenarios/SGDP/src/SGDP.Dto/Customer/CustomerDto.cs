using System;
using Tnf.Dto;

namespace SGDP.Dto.Customer
{
    public class CustomerDto : BaseDto
    {
        public Guid Id { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public string Rg { get; set; }
    }
}
