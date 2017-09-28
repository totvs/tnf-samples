using System.Collections.Generic;
using Tnf.Domain.Entities;

namespace Tnf.Architecture.Domain.Registration
{
    public class Person : Entity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Person Parent { get; set; }
        public virtual List<Person> Children { get; set; }

        public Person()
        {
        }

        public Person(int id, string name)
            : this(id, name, 0)
        {
        }

        public Person(int id, string name, int parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
        }

        public enum Error
        {
            PersonNameMustHaveValue = 1
        }
    }
}
