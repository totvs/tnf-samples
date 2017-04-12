using Tnf.Sample.Dto.ValueObjects;
using Tnf.Dto;

namespace Tnf.Sample.Core.WhiteHouse
{
    public class President
    {
        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public ZipCode ZipCode { get; internal set; }

        public enum Error
        {
            Unexpected = 0,
            [Description("Invalid Id: {0}")]
            InvalidId = 1,
            [Description("Invalid name: {0}")]
            NameMustHaveValue = 2,
            [Description("Invalid zip code: {0}")]
            ZipCodeMustHaveValue = 3,
        }
    }
}
