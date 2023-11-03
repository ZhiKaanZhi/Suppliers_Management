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
                    return false;  // TaxpayerId should only have digits
                sum += c - '0';
            }

            if((sum % 11 == 10 && taxpayerId[0] == 0) || (sum % 11 == taxpayerId[0]))
            {
               return true;
            }
            else
            {
                return false;
            }

          
        }
    }
}
