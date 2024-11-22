using System.ComponentModel.DataAnnotations;

namespace BookStore.Persistence.EFCore.Validation;

public class DateBeforeToday : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime date)
        {
            if (date >= DateTime.Today)
                return new ValidationResult("Date must be before today.");
        }

        return ValidationResult.Success;
    }
}