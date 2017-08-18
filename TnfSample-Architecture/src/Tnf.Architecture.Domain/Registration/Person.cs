using Tnf.Domain.Entities;

namespace Tnf.Architecture.Domain.Registration
{
    public class Person : Entity
    {
        public string Name { get; set; }

        public Person()
        {
        }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public enum Error
        {
            PersonNameMustHaveValue = 1
        }
    }
}
