using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace XControls.ValueConverters
{
    public class DecimalToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return $"0x{0:X}";
            }
            int.TryParse(value.ToString(), out int number);
            return $"0x{number:X}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string input)
            {
                if (string.IsNullOrEmpty(input))
                {
                    return 0;
                }
                //if (input.Length > 10)
                //    return input.Substring(input.Length - 9);
                switch (parameter.ToString())
                {
                    case "0": //int
                        return System.Convert.ToInt32(input, 16);
                    case "1": //double
                        return (double)System.Convert.ToInt32(input, 16);
                    case "2": //byte
                        return System.Convert.ToByte(input, 16);
                    case "3":
                    default:
                        return System.Convert.ToInt32(input, 16).ToString();
                } 
            }
            return 0;
        }
    }
}
