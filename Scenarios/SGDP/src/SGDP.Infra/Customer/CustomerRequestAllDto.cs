using Tnf.Dto;

namespace SGDP.Dto.Customer
{
    public class CustomerRequestAllDto : RequestAllDto
    {
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Rg { get; set; }
    }
}
