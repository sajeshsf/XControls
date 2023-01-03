using System;
using System.Globalization;
using System.Windows.Data;

namespace XControls.Utilities.ValueConverters
{
    public class TicksToTimeSpanConverter : IValueConverter
    {
        private const string Format = "dd/MM/yyyy  HH:mm:ss:fff";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Note: converting Data-Value to Local Time 
            if (value is long ticks)
            {
                if (TimeSpan.MinValue.Ticks < ticks && ticks > TimeSpan.MaxValue.Ticks)
                {
                    var span = new TimeSpan(ticks);
                    return span.ToString(Format);
                }
            }
            return TimeSpan.MinValue.Ticks.ToString(Format);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string input)
            {
                if (TimeSpan.TryParse(input, out TimeSpan timeSpan))
                {
                    return timeSpan.Ticks;
                }
            }
            return TimeSpan.MinValue.Ticks;
        }
    }
}
