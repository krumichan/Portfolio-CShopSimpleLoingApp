using System;
using System.Globalization;
using System.Windows.Data;

namespace LoginApp.Converter
{
    /// <summary>
    /// Create Account시, Multi Binding을 지원하기 위한 Converter Class
    /// </summary>
    class CreateAccountConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new Tuple<string, string>(values[0] as string, values[1] as string);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
