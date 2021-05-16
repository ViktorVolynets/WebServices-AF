using System;

namespace ConsoleApp
{
    class MachineData
    {
        public string MachineName { get; set; }
        public string TimeZone { get; set; }
        public string OsVersion { get; set; }
        public string DotNetVersion { get; set; }
        public MachineData()
        {
            MachineName = Environment.MachineName.ToString();
            TimeZone = TimeZoneInfo.Local.DisplayName;
            OsVersion = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            DotNetVersion = Environment.Version.ToString();
        }
    }
}
