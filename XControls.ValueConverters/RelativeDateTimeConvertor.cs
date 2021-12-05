using System;
using System.Globalization;
using System.Windows.Data;

namespace XControls.ValueConverters
{
    public class RelativeDateTimeConvertor : IValueConverter
    {
        const int SecondsInAMinute = 60;
        const int SecondsInAHour = 60 * SecondsInAMinute;
        const int SecondsInADay = 24 * SecondsInAHour;
        const int SecondsInAMonth = 30 * SecondsInADay;
        const int SecondsInAYear = 365 * SecondsInADay;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            if (value is DateTime input)
            {
                var ts = new TimeSpan(DateTime.Now.Ticks - input.Ticks);
                double totalSeconds = Math.Abs(ts.TotalSeconds);

                if (totalSeconds < SecondsInAMinute)
                {
                    if (ts.Seconds < 0)
                    {
                        return "just now";
                    }
                    if (ts.Seconds < 2)
                    {
                        return "One second ago";
                    }
                    return $"{ts.Seconds} seconds ago";
                }

                if (totalSeconds < 2 * SecondsInAMinute)
                {
                    return "A minute ago";
                }

                if (totalSeconds < SecondsInAHour)
                {
                    if (ts.Minutes < 0)
                    {
                        return "Some minutes ago";
                    }
                    return $"{ts.Minutes} minutes ago";
                }

                if (totalSeconds <= 2 * SecondsInAHour)
                {
                    return "An hour ago";
                }

                if (totalSeconds < SecondsInADay)
                {
                    if (ts.Hours < 0)
                    {
                        return "Some hours ago";
                    }
                    return $"{ts.Hours} hours ago";
                }

                if (totalSeconds < 2 * SecondsInADay)
                {
                    return "Yesterday";
                }

                if (totalSeconds < SecondsInAMonth)
                {
                    if (ts.Days < 0)
                    {
                        return "Some days ago";
                    }
                    return $"{ts.Days} days ago";
                }

                if (totalSeconds < 2 * SecondsInAMonth)
                {
                    return "A month ago";
                }

                if (totalSeconds < SecondsInAYear)
                {
                    var months = (int)Math.Floor(ts.Days / 30.0);
                    if (months < 0)
                    {
                        return "Some months ago";
                    }
                    return $"{months} months ago";
                }

                if (totalSeconds < 2 * SecondsInAYear)
                {
                    return "A year ago";
                }

                var years = (int)Math.Floor(ts.Days / 365.0);
                if (years < 0)
                {
                    return "Some years ago";
                }
                return $"{years} years ago";
            }
            return "Some time ago";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
