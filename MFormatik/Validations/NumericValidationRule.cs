using System.Globalization;
using System.Windows.Controls;

namespace MFormatik.Validations
{
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (value ?? "").ToString();
            if (double.TryParse(input, out _))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Numbers Only!");
        }
    }
}
