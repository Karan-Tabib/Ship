using System.ComponentModel.DataAnnotations;

namespace ShipAPI.Validators
{
    public class DateCheckAttribute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if (date < DateTime.Now) {
                return new ValidationResult("The Date must be grater than or equal to Today");
            }
            return ValidationResult.Success;
        }
    }
}
