using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace XControls.Utilities
{
    public class LocalisationBase : Bindable
    {
        private ResourceManager? resourceManager;
        private string resourcesBaseName;
        private Assembly assembly;
        protected void SetResourcesBaseName(string value)
        {
            if (resourcesBaseName != value)
            {
                resourcesBaseName = value;
                resourceManager = null;
            }
        }
        protected void SetAssembly(Assembly value)
        {
            if (assembly != value)
            {
                assembly = value;
                resourceManager = null;
            }
        }
        protected ResourceManager ResourceManager => resourceManager ??=
            new ResourceManager($"{assembly.GetName().Name}.{resourcesBaseName}", assembly);
        public LocalisationBase()
        {
            resourcesBaseName = "Resources.Resources";
            assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        }
        public string? GetText(string text) => GetText(text, CultureInfo.CurrentUICulture);
        public string? GetText(string text, CultureInfo culture) => ResourceManager.GetString(text, culture);
    }
}
