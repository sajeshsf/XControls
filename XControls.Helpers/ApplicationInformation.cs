using System;
using Microsoft.Win32;
using System.Globalization;
using System.Reflection;

namespace XControls.Helpers
{
    public static class ApplicationInformation
    {
        public static string ApplicationTitle => Assembly.GetEntryAssembly().GetName().Name;
        public static string ApplicationCulture => Assembly.GetEntryAssembly().GetName().CultureInfo.DisplayName.ToString(CultureInfo.InvariantCulture);
        public static string ApplicationVersion => Assembly.GetEntryAssembly().GetName().Version.ToString();
        public static string ApplicationDescription => ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(Assembly.GetEntryAssembly(), typeof(AssemblyDescriptionAttribute))).Description;
        public static string ApplicationCompany => ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetEntryAssembly(), typeof(AssemblyCompanyAttribute))).Company;
        public static string ApplicationProduct => ((AssemblyProductAttribute)Attribute.GetCustomAttribute(Assembly.GetEntryAssembly(), typeof(AssemblyProductAttribute))).Product;
        public static string ApplicationCopyright => ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetEntryAssembly(), typeof(AssemblyCopyrightAttribute))).Copyright;
        public static string ApplicationTrademark => ((AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(Assembly.GetEntryAssembly(), typeof(AssemblyTrademarkAttribute))).Trademark;
        public static string ProgramDataFolder => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ApplicationTitle);
        public static string AppDataFolder => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationTitle);
        public static string DocumentsFolder => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ApplicationTitle);
        public static string ProgramFilesFolder => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), ApplicationTitle);
        public static RegistryKey Key => Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + ApplicationTitle);
        public static bool CompareVersion(this string v1) => new Version(v1 ?? " 0.0.0.0") == new Version(ApplicationVersion);
        public static bool CompareVersion(this string v1, string v2) => new Version(v1 ?? " 0.0.0.0") == new Version(v2 ?? " 0.0.0.0");
    }
}