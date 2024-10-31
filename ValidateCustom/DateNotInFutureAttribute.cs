using System.ComponentModel.DataAnnotations;

namespace ManageEmployee.ValidateCustom
{
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.Now)
                {
                    return new ValidationResult("The issued date cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
