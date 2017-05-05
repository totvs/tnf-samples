using System.ComponentModel.DataAnnotations;
using Tnf.Runtime.Validation;

namespace Tnf.Architecture.Dto
{
    public class CountryDto : CustomValidate
    {
        public string Name { get; set; }

        public override void AddValidationErrors(CustomValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                context.Results.Add(new ValidationResult("Name is required"));
            }
        }
    }
}
