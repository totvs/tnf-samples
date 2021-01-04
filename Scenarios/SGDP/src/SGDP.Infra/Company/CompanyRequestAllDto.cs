using Tnf.Dto;

namespace SGDP.Dto.Company
{
    public class CompanyRequestAllDto : RequestAllDto
    {
        public string Cnpj { get; set; }
        public string Email { get; set; }
    }
}
