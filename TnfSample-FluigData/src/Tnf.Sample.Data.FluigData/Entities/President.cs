using Newtonsoft.Json;
using Tnf.App.FluigData.FluigData.Repositories;

namespace Tnf.Sample.FluigData.Entities
{
    [JsonObject("president")]
    public class PresidentEntity : FluigDataEntity
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
