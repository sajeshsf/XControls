using System;
using Microsoft.Win32;
using System.Globalization;
using System.Reflection;
using System.IO;

namespace XControls.Helpers
{
    public static class ApplicationInformation
    {

        public static string ApplicationTitle => GetEntryAssembly().GetName().Name ?? "App1";
        public static string? ApplicationCulture => GetEntryAssembly().GetName().CultureInfo?.DisplayName?.ToString(CultureInfo.InvariantCulture);
        public static string? ApplicationVersion => GetEntryAssembly().GetName().Version?.ToString();
        public static string? ApplicationDescription => (GetAtribute(typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute)?.Description;
        public static string? ApplicationCompany => (GetAtribute(typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute)?.Company;
        public static string? ApplicationProduct => (GetAtribute(typeof(AssemblyProductAttribute)) as AssemblyProductAttribute)?.Product;
        public static string? ApplicationCopyright => (GetAtribute(typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute)?.Copyright;
        public static string? ApplicationTrademark => (GetAtribute(typeof(AssemblyTrademarkAttribute)) as AssemblyTrademarkAttribute)?.Trademark;
        public static string ProgramDataFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ApplicationTitle);
        public static string AppDataFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationTitle);
        public static string DocumentsFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ApplicationTitle);
        public static string ProgramFilesFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), ApplicationTitle);
        public static RegistryKey Key => Registry.CurrentUser.CreateSubKey(@"SOFTWARE\" + ApplicationTitle);
        public static bool CompareVersion(this string? v1) => CompareVersion(v1, ApplicationVersion);
        public static bool CompareVersion(this string? v1, string? v2) => new Version(v1 ?? " 0.0.0.0") == new Version(v2 ?? " 0.0.0.0");
        private static Assembly GetEntryAssembly()
        {
            var assembly = Assembly.GetEntryAssembly();
            if (assembly != null)
            {
                return assembly;
            }
            throw new InvalidOperationException("Can't call from unmanaged code.");
        }
        private static Attribute? GetAtribute(Type attributeType) => Attribute.GetCustomAttribute(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly(), attributeType);
    }
}