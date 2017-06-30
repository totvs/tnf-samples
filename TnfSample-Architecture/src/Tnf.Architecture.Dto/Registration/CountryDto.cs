using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tnf.App.Dto.Response;
using Tnf.Runtime.Validation;

namespace Tnf.Architecture.Dto
{
    public class CountryDto : CustomValidate, IDto
    {
        public string Name { get; set; }

        public IList<string> _expandables { get; set; }

        public CountryDto()
        {
            _expandables = new List<string>();
        }

        public override void AddValidationErrors(CustomValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                context.Results.Add(new ValidationResult("Name is required"));
            }
        }
        
        public enum Error
        {
            GetAllCountry = 1,
            GetCountry = 2,
            PostCountry = 3,
            PutCountry = 4,
            DeleteCountry = 5
        }
    }
}
