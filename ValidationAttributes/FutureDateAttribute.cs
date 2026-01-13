using System;
using System.ComponentModel.DataAnnotations;

namespace HCAMiniEHR.ValidationAttributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime <= DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Appointment date must be in the future.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
