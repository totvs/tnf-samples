using System;
using Tnf.Sgdp;

namespace SGDP.Domain.Entities
{
    [SgdpAuditedEntity]
    [SgdpDescription("Classe que contem os dados do cliente")]
    public class Customer
    {
        public Guid Id { get; set; }

        [SgdpData(true, SgdpDataTypes.CPF, false, false)]
        [SgdpPurpose(SgdpClassification.CREDIT_PROTECTION, "Usado para consultar nos sistemas de credito")]
        [SgdpDescription("CPF do cliente")]
        public string Cpf { get; set; }

        [SgdpData(false, SgdpDataTypes.Email, true, true)]
        [SgdpPurpose(SgdpClassification.CONSENTMENT, "Client consentiu em fornecer o email")]
        [SgdpDescription("Email do cliente")]
        public string Email { get; set; }

        [SgdpData(false, SgdpDataTypes.RG, true, true)]
        [SgdpPurpose(SgdpClassification.LEGAL_OBLIGATION, "Somos obrigados a guardar o RG dos clientes")]
        [SgdpDescription("RG do cliente")]
        public string Rg { get; set; }
    }
}
