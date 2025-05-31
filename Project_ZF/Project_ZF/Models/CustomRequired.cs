using System.ComponentModel.DataAnnotations;

namespace Project_ZF.Models
{
    public class CustomRequired : ValidationAttribute
    {
        public CustomRequired(string sepcialType)
        {
            this.SepcialType = sepcialType;
        }
        public string SepcialType { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorType;
            if (value == null)
            {
                errorType = "verplicht";
            }
            else if (!string.IsNullOrEmpty(SepcialType) && !IsValidEmail(value.ToString()))
            {
                errorType = "geen geldig e-mailadres";
            }
            else
            {
                return ValidationResult.Success;
            }
            ErrorMessage = $"{validationContext.DisplayName} is {errorType}.";
            return new ValidationResult(ErrorMessage);
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
