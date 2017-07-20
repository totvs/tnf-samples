using Tnf.App.Carol.Repositories;
using Tnf.Architecture.Common.ValueObjects;

namespace Tnf.Architecture.Carol.Entities
{
    public class PresidentPoco : CarolEntity
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
