using System;
using System.Globalization;
using System.Windows.Data;

namespace XControls.Utilities.ValueConverters
{
    public class DecimalToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                if (int.TryParse(value.ToString(), out int number))
                {
                    return $"0x{number:X}";
                }
            }
            return $"0x{0:X}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string input)
            {
                if (string.IsNullOrEmpty(input))
                {
                    return 0;
                }
                switch (Type.GetTypeCode(targetType))
                {
                    case TypeCode.Byte:
                    case TypeCode.Int32:
                        return System.Convert.ChangeType(input, targetType);
                    case TypeCode.Double:
                    case TypeCode.Int64:
                    case TypeCode.Boolean:
                    case TypeCode.Empty:
                    case TypeCode.Object:
                    case TypeCode.DBNull:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                    case TypeCode.String:
                    default:
                        throw new InvalidCastException($"{targetType?.Name} is not supported");
                }
            }
            return 0;
        }
    }
}
