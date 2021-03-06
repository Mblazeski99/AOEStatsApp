using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AOEStatsApp.ValueConverters
{
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;

            var res = value is bool boolValue && boolValue ? Visibility.Collapsed : Visibility.Visible;
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
