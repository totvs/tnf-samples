using Tnf.App.Dto.Response;

namespace Tnf.Architecture.Dto.Registration
{
    public class CountryDto : DtoBase
    {
        public string Name { get; set; }
        
        public enum Error
        {
            GetAllCountry = 1,
            GetCountry = 2,
            PostCountry = 3,
            PutCountry = 4,
            DeleteCountry = 5,
            CountryNameMustHaveValue = 6
        }
    }
}
