using System;
using System.Globalization;
using System.Windows.Data;

namespace login_page.Helper
{
    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value).ToString("{#,###}", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result;
            if (Double.TryParse(value.ToString(), System.Globalization.NumberStyles.Any,
                         culture, out result))
            {
                return result;
            }
            else if (Double.TryParse(value.ToString().Replace(" руб.", ""), System.Globalization.NumberStyles.Any,
                         culture, out result))
            {
                return result;
            }
            return value;
        }
    }
}
