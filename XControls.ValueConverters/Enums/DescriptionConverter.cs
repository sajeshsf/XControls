using System;
using System.ComponentModel;
using System.Globalization;

namespace XControls.ValueConverters
{
    public class DescriptionConverter : EnumConverter
    {
        public DescriptionConverter(Type type) : base(type) => _ = true;
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            if (value == null)
            {
                return string.Empty;
            }
            return GetDescription(value);
        }
        private static object GetDescription(object value)
        {
            var name = value.ToString();
            if (name == null)
            {
                return string.Empty;
            }
            var fi = value.GetType().GetField(name);
            if (fi == null)
            {
                return name;
            }
            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes)
            {
                if (attributes.Length > 0)
                {
                    var description = attributes[0]?.Description;
                    if (description != null && !string.IsNullOrEmpty(description))
                    {
                        return description;
                    }
                }
            }
            return name;
        }
    }
}
