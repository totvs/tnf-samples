using System;
using Tnf.Domain.Entities;

namespace Tnf.Architecture.Domain.Registration
{
    /// <summary>
    /// Por default a entidade recebe uma propriedade "int Id" pela herança
    /// Para casos onde o nome seja especifico onde a PK seja "SYS009_PROFESSIONAL_ID" fazendo o mapeamento
    /// da entidade normalmente mas ao configurar ela via anotations ou via modelbuilder (olhar dentro do LegacyDbContext)
    /// fazemos o uso do metodo Ignore
    /// </summary>
    public class Professional : Entity
    {
        public Professional()
        {
            Code = Guid.NewGuid();
        }

        [Obsolete("Id property excluded from entity.")]
        public override int Id { get; set; }
        public decimal ProfessionalId { get; set; }
        public string Name { get; set; }
        public Guid Code { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
