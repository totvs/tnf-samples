using System;
using Tnf.Sgdp;

namespace SGDP.Domain.Entities
{
    [SgdpAuditedEntity]
    [SgdpDescription("Classe que contem os dados da empresa")]
    public class Company
    {
        public Guid Id { get; set; }

        public string Cnpj { get; set; }

        [SgdpData(false, SgdpDataTypes.Email, true, true)]
        [SgdpPurpose(SgdpClassification.CONSENTMENT, "Empresa consentiu em fornecer o email")]
        [SgdpDescription("Email da empresa")]
        public string Email { get; set; }
    }
}
