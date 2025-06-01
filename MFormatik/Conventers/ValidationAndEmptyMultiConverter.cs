using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MFormatik.Converters
{
    public class ValidationAndEmptyMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var value in values)
            {
                if (value == DependencyProperty.UnsetValue)
                {
                    continue; // Skip if the binding is not resolved yet
                }

                if (value == null)
                {
                    return false; // Disable the button if any value is null
                }

                if (value is string str && string.IsNullOrWhiteSpace(str))
                {
                    return false; // Disable the button if any text box is empty
                }

                if (value is bool hasError && hasError)
                {
                    return false; // Disable the button if any control has validation errors
                }
            }
            return true; // Enable the button if all text boxes are filled and have no validation errors
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}