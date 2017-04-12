using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Domain.Entities;

namespace Acme.SimpleTaskApp.Crud
{
    [Table("Customer")]
    public class Customer : Entity
    {
        public string Nome { get; set; }

        public string NomeFantasia { get; set; }
    }
}
