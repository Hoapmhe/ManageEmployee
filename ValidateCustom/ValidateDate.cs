using ManageEmployee.Models;
using System.ComponentModel.DataAnnotations;

namespace ManageEmployee.ValidateCustom
{
    public class ValidateDate : ValidationAttribute
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

    public class ExpiryDateAfterIssuedDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var diploma = (Diploma)validationContext.ObjectInstance;

            if (diploma.ExpiryDate.HasValue && diploma.IssuedDate > diploma.ExpiryDate.Value)
            {
                return new ValidationResult("Expiry date must be greater than the issued date.");
            }

            return ValidationResult.Success;
        }
    }
}
