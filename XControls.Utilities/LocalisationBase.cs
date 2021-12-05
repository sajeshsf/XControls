using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace XControls.Utilities
{
    public class LocalisationBase : ObservableObject
    {
        protected readonly string resourcesBaseName;
        protected readonly Assembly assembly;
        protected readonly ResourceManager resourceManager;
        public LocalisationBase()
        {
            resourcesBaseName = "Resources.Resources";
            assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            resourceManager = new ResourceManager(assembly.GetName().Name + resourcesBaseName, assembly);
        }
        public string? GetText(string text) => GetText(text, CultureInfo.CurrentUICulture);
        public string? GetText(string text, CultureInfo? culture) => resourceManager.GetString(text, culture);
    }
}
