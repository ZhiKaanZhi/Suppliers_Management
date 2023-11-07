using System.ComponentModel.DataAnnotations;

namespace WebApplication1.CustomValidators
{
    public class TidValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? tid = value as string;

            if (string.IsNullOrWhiteSpace(tid))
            {
                return new ValidationResult("Taxpayer ID is required.");
            }

            if (!IsValidTaxpayerId(tid))
            {
                return new ValidationResult("Invalid Taxpayer ID.");
            }

            return ValidationResult.Success;
        }


        private bool IsValidTaxpayerId(string taxpayerId)
        {
            int sum = 0;
            foreach (char c in taxpayerId)
            {
                if (!char.IsDigit(c))
                    return false;  
                sum += c - '0';
            }
        
            int firstDigit = taxpayerId[0] - '0';
            return (sum % 11 == 10 && firstDigit == 0) || (sum % 11 == firstDigit);
        }
    }
}
