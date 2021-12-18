using System;
using System.Collections;
using System.Management;

namespace XControls.Helpers
{
    public class SystemInformation
    {
        public static ArrayList Win32_ComputerSystem => SystemInformation.GetInformation("Win32_ComputerSystem");
        public static ArrayList Win32_DiskDrive => SystemInformation.GetInformation("Win32_DiskDrive");
        public static ArrayList Win32_OperatingSystem => SystemInformation.GetInformation("Win32_OperatingSystem");
        public static ArrayList Win32_Processor => SystemInformation.GetInformation("Win32_Processor");
        public static ArrayList Win32_ProgramGroup => SystemInformation.GetInformation("Win32_ProgramGroup");
        public static ArrayList Win32_SystemDevices => SystemInformation.GetInformation("Win32_SystemDevices");
        public static ArrayList Win32_StartupCommand => SystemInformation.GetInformation("Win32_StartupCommand");
        private static ArrayList GetInformation(string qry)
        {
            var arrayListInformationCollactor = new ArrayList();
            try
            {
                int i = 0;
                using var searcher = new ManagementObjectSearcher($"SELECT * FROM {qry}");
                foreach (var mo in searcher.Get())
                {
                    i++;
                    var searcherProperties = mo.Properties;
                    foreach (var sp in searcherProperties)
                    {
                        arrayListInformationCollactor.Add(sp);
                    }
                }
            }
            catch (Exception)
            {
            }
            return arrayListInformationCollactor;
        }
    }
}
