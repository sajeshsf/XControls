namespace XControls.ValueConverters.Enums
{
    public static class EnumExtensions
    {
        public static object GetDescription(object value) => DescriptionConverter.GetDescription(value).ToString();
    }
}
