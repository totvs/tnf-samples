using Tnf.App.Dto.Request;

namespace Tnf.Architecture.Dto.Registration
{
    public class GetAllCountriesDto : RequestAllDto
    {
        public GetAllCountriesDto()
            : base()
        {
        }

        public string Name { get; set; }
    }
}
