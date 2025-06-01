using System.Globalization;
using System.Windows.Controls;

namespace MFormatik.Validations
{
    public class TextBoxValidationRule : ValidationRule
    {
        public int MaxLength { get; set; } = 60; // Default maximum length

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var text = value as string;

            if (string.IsNullOrEmpty(text))
            {
                return new ValidationResult(false, "Required Field.");
            }

            if (text.Length > MaxLength)
            {
                return new ValidationResult(false, $"Max {MaxLength} characters");
            }

            return ValidationResult.ValidResult;
        }
    }
}
