using System;
using System.Globalization;
using System.Windows.Controls;


namespace XControls.NumberStepper
{
    public class NumericValidationRule : ValidationRule
    {
        public Type ValidationType { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var strValue = Convert.ToString(value);
            if (!string.IsNullOrEmpty(strValue))
            {
                switch (ValidationType.Name)
                {
                    case "Boolean":
                        return bool.TryParse(strValue, out _) ? new ValidationResult(true, null) : new ValidationResult(false, $"Parameter Error");
                    case "Int32":
                        return int.TryParse(strValue, out _) ? new ValidationResult(true, null) :
                            new ValidationResult(false, $"Parameter Error! Please enter values between 0 and 255.");
                    case "Double":
                        return double.TryParse(strValue, out _) ? new ValidationResult(true, null) : new ValidationResult(false, $"Parameter Error");
                    case "Int64":
                        return long.TryParse(strValue, out _) ? new ValidationResult(true, null) : new ValidationResult(false, $"Parameter Error");
                    default:
                        throw new InvalidCastException($"{ValidationType.Name} is not supported");
                }
            }
            return new ValidationResult(false, $"Value cannot be converted to string.");
        }
    }
}
