using Newtonsoft.Json;
using Tnf.App.Carol.Repositories;

namespace Tnf.Architecture.Data.Entities
{
    [JsonObject("president")]
    public class PresidentPoco : CarolEntity
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }

        public override object GetStagingMapping()
        {
            return new
            {
                name = "string",
                zipCode = "string"
            };
        }
    }
}
