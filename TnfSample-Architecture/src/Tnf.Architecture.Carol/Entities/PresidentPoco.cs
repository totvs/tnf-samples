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
                properties = new
                {
                    name = "string",
                    address = new
                    {
                        properties = new
                        {
                            street = "string",
                            number = "string",
                            complement = "string",
                            zipCode = new
                            {
                                properties = new
                                {
                                    number = "string"
                                },
                                type = "nested"
                            }
                        },
                        type = "nested"
                    }
                }
            };
        }
    }
}
