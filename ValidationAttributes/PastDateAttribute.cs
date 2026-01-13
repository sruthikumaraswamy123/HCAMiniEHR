using System;
using System.ComponentModel.DataAnnotations;

namespace HCAMiniEHR.ValidationAttributes
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime > DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Date must be in the past.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
