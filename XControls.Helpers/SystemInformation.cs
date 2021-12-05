using System;
using System.Collections;
using System.Management;

namespace XControls.Helpers
{
    public class SystemInformation
    {
        public ArrayList Win32_ComputerSystem => GetInformation("Win32_ComputerSystem");
        public ArrayList Win32_DiskDrive => GetInformation("Win32_DiskDrive");
        public ArrayList Win32_OperatingSystem => GetInformation("Win32_OperatingSystem");
        public ArrayList Win32_Processor => GetInformation("Win32_Processor");
        public ArrayList Win32_ProgramGroup => GetInformation("Win32_ProgramGroup");
        public ArrayList Win32_SystemDevices => GetInformation("Win32_SystemDevices");
        public ArrayList Win32_StartupCommand => GetInformation("Win32_StartupCommand");
        private ArrayList GetInformation(string qry)
        {
            ArrayList arrayListInformationCollactor = new ArrayList();
            try
            {
                int i = 0;
                using ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM {qry}");
                foreach (ManagementObject mo in searcher.Get())
                {
                    i++;
                    PropertyDataCollection searcherProperties = mo.Properties;
                    foreach (PropertyData sp in searcherProperties)
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
