using System;
using System.Globalization;
using System.Windows.Data;

namespace XControls.ValueConverters
{
    public class FlagNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Negate(value);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Negate(value);
        private object Negate(object value) => value is bool flag ? !flag : value;
    }
}
