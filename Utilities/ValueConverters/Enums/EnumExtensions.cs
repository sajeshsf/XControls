namespace XControls.Utilities.ValueConverters.Enums
{
    public static class EnumExtensions
    {
        public static object GetDescription(this object value) => DescriptionConverter.GetDescription(value).ToString();
    }
}
