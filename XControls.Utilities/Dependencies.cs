using System.IO;
using System.Management;
using static System.Reflection.Assembly;
using static System.Diagnostics.Process;
using static System.IO.Path;

namespace XControls.Utilities
{
    public static class Dependencies
    {
        public static bool IsCurrentProcessRunning => IsProcessRunning(GetFileNameWithoutExtension(GetEntryAssembly().Location));
        public static bool CleanUpOldProcess(string[] proceses)
        {
            foreach (var s in proceses)
            {
                var p = GetProcessesByName(s);
                foreach (var item in p)
                {
                    item.Kill();
                }
            }
            return true;
        }
        public static bool CheckForDependencies(out string msg, string[]? assemblies = null, string[]? drivers = null, string[]? proceses = null)
        {
            var retBool = true;
            if (assemblies != null)
            {
                foreach (var item in assemblies)
                {
                    if (SearchForAssembly(item))
                    {
                        msg = $"{GetFileNameWithoutExtension(item)} is missing.!";
                    }
                }
            }
            if (drivers != null)
            {
                foreach (var item in drivers)
                {
                    if (SearchForDriver(item))
                    {
                        msg = $"{item} driver is missing.!";
                    }
                }
            }
            if (proceses != null)
            {
                foreach (var item in proceses)
                {
                    if (SearchForAssembly(item))
                    {
                        msg = $"{GetFileNameWithoutExtension(item)} Assembly is missing.!";
                    }
                }
            }
            msg = string.Empty;
            return retBool;
        }
        public static bool SelectQuery(string className, string condition)
        {
            var query = new SelectQuery(className, condition);
            var searcher = new ManagementObjectSearcher(query);
            var drivers = searcher.Get();
            return drivers.Count > 0;
        }
        public static bool SearchForDriver(string driverName) => SelectQuery("Win32_SystemDriver", $"Name = '{driverName}'");
        public static bool SearchForAssembly(string assemblyName) => File.Exists(assemblyName);
        public static bool IsProcessRunning(string name) => GetProcessesByName(name).Length > 0;
    }
}