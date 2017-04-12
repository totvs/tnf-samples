using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Domain.Entities;

namespace Acme.SimpleTaskApp.People
{
    [Table("Persons")]
    public class Person : Entity
    {
        public virtual string PersonName { get; set; }

        public Person() { }

        public Person(string personName)
        {
            PersonName = personName;
        }
    }
}
