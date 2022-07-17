using AOEStatsApp.Helpers;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace AOEStatsApp.ValueConverters
{
    public class EnumToCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return EnumHelper.GetAllValuesAndDescriptions(value.GetType());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
