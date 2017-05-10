using Newtonsoft.Json;
using Tnf.App.Carol.Repositories;
using Tnf.Architecture.Dto.ValueObjects;

namespace Tnf.Architecture.Data.Entities
{
    [JsonObject("president")]
    public class PresidentPoco : CarolEntity
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public override object GetStagingMapping()
        {
            return new
            {
                name = "string",
                address = new
                {
                    street = "string",
                    number = "string",
                    complement = "string",
                    zipCode = new
                    {
                        number = "string"
                    }
                }
            };
        }
    }
}
