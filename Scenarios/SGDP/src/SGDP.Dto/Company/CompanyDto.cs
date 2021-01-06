using System;
using Tnf.Dto;

namespace SGDP.Dto.Company
{
    public class CompanyDto : BaseDto
    {
        public Guid Id { get; set; }

        public string Cnpj { get; set; }

        public string Email { get; set; }
    }
}
