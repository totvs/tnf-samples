using Tnf.App.Carol.Repositories;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Data.Entities
{
    public class PresidentPoco : CarolEntity
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
