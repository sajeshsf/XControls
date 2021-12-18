using System;
using System.Globalization;
using System.Windows.Controls;


namespace XControls.NumberStepper
{
    public class NumericValidationRule : ValidationRule
    {
        public Type? ValidationType { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var strValue = Convert.ToString(value);
            if (string.IsNullOrEmpty(strValue))
            {
                return new ValidationResult(false, $"Value cannot be converted to string.");
            }
            var validationResult = new ValidationResult(false, $"Parameter Error");
            var sucess = new ValidationResult(true, null);
            switch (Type.GetTypeCode(ValidationType))
            {
                case TypeCode.Boolean:
                    return bool.TryParse(strValue, out _) ? sucess : validationResult;
                case TypeCode.Int32:
                case TypeCode.Byte:
                    return int.TryParse(strValue, out _) ? sucess :
                        new ValidationResult(false, $"Parameter Error! Please enter values between 0 and 255.");
                case TypeCode.Double:
                    return double.TryParse(strValue, out _) ? sucess : validationResult;
                case TypeCode.Int64:
                    return long.TryParse(strValue, out _) ? sucess : validationResult;
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
                    throw new InvalidCastException($"{ValidationType?.Name} is not supported");
            }
        }
    }
}
