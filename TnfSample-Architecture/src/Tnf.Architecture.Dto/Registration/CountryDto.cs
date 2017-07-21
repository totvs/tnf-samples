using Tnf.App.Dto.Response;

namespace Tnf.Architecture.Dto.Registration
{
    public class CountryDto : DtoBase
    {
        public string Name { get; set; }

        //public override void AddValidationErrors(CustomValidationContext context)
        //{
        //    if (string.IsNullOrWhiteSpace(Name))
        //    {
        //        context.Results.Add(new ValidationResult("Name is required"));
        //    }
        //}
        
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
