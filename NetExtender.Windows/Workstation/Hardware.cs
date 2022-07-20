// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using NetExtender.Utilities.Types;
using NetExtender.Windows.Utilities;

namespace NetExtender.Workstation
{
    public enum ProcessorArchitecture
    {
        Unknown,
        Bit32,
        Bit64,
        Itanium64,
        Arm,
        Arm64
    }

    public static partial class Hardware
    {
        [DllImport("kernel32.dll")]
        private static extern void GetSystemInfo([MarshalAs(UnmanagedType.Struct)] ref SystemInfo lpSystemInfo);

        [DllImport("kernel32.dll")]
        private static extern void GetNativeSystemInfo([MarshalAs(UnmanagedType.Struct)] ref SystemInfo lpSystemInfo);

        private static String? GetWmiPropertyValueAsString(String query, String property)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            SelectQuery selector = new SelectQuery(query);
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher(selector);
            using ManagementObjectCollection collection = searcher.Get();

            return collection.AsEnumerable().Select(management => management[property]).FirstOrDefault()?.ToString();
        }
        
        public static DateTime GetWmiPropertyValueAsDateTime(String query, String property)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            String? value = GetWmiPropertyValueAsString(query, property);
            return !String.IsNullOrEmpty(value) ? ManagementDateTimeConverter.ToDateTime(value) : throw new ManagementException();
        } 
        
        public static DateTime GetBootDateTime()
        {
            return GetWmiPropertyValueAsDateTime("SELECT * FROM Win32_OperatingSystem WHERE Primary='true'", "LastBootUpTime");
        }
        
        public static String? GetProcessorId()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT processorID FROM win32_processor");
            using ManagementObjectCollection collection = searcher.Get();
            return collection.AsEnumerable().Select(management => management["processorID"]).FirstOrDefault()?.ToString();
        }
        
        public static ProcessorArchitecture GetProcessorBits()
        {
            try
            {
                SystemInfo info = new SystemInfo();
                GetNativeSystemInfo(ref info);

                return info.ProcessorInfo.Architecture switch
                {
                    12 => ProcessorArchitecture.Arm64, // ARM64
                    9 => ProcessorArchitecture.Bit64, // AMD64
                    6 => ProcessorArchitecture.Itanium64, // IA64
                    5 => ProcessorArchitecture.Arm, // ARM
                    0 => ProcessorArchitecture.Bit32, // Intel
                    _ => ProcessorArchitecture.Unknown
                };
            }
            catch (Exception)
            {
                return ProcessorArchitecture.Unknown;
            }
        }

        public static String? GetHDDSerialNumber()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT VolumeSerialNumber FROM Win32_LogicalDisk");
            using ManagementObjectCollection collection = searcher.Get();
            
            StringBuilder builder = new StringBuilder();
            
            foreach (ManagementBaseObject management in collection)
            {
                builder.Append(Convert.ToString(management["VolumeSerialNumber"]));
            }

            return builder.Length > 0 ? builder.ToString() : null;
        }

        public static String? GetMACAddress()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT IPEnabled, MacAddress FROM Win32_NetworkAdapterConfiguration");
            using ManagementObjectCollection collection = searcher.Get();

            return collection.AsEnumerable()
                .Where(management => management["IPEnabled"] is Boolean ip && ip)
                .Select(management => management["MacAddress"]).FirstOrDefault()?.ToString();
        }

        private static String? GetStringFromSearcher(String property, String table)
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT {property} FROM {table}");
            using ManagementObjectCollection collection = searcher.Get();
            
            return collection.AsEnumerable().TrySelect(management => management[property]).FirstOrDefault()?.ToString();
        }

        public static String? GetMotherboardManufacturer()
        {
            return GetStringFromSearcher("Manufacturer", "Win32_BaseBoard");
        }

        public static String? GetMotherboardProductId()
        {
            return GetStringFromSearcher("Product", "Win32_BaseBoard");
        }

        public static String? GetDiskRomDrive()
        {
            return GetStringFromSearcher("Drive", "Win32_CDROMDrive");
        }

        public static String? GetBIOSManufacturer()
        {
            return GetStringFromSearcher("Manufacturer", "Win32_BIOS");
        }

        public static String? GetBIOSSerialNumber()
        {
            return GetStringFromSearcher("SerialNumber", "Win32_BIOS");
        }

        public static String? GetBIOSCaption()
        {
            return GetStringFromSearcher("Caption", "Win32_BIOS");
        }

        public static String? GetAccountName()
        {
            return GetStringFromSearcher("Name", "Win32_UserAccount");
        }

        public static Int64 GetPhysicalMemory()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory");
            using ManagementObjectCollection collection = searcher.Get();

            return collection.AsEnumerable().TrySelect(management => Convert.ToInt64(management["Capacity"])).Sum();
        }

        public static Int32 GetRAMSlots()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT MemoryDevices FROM Win32_PhysicalMemoryArray");
            using ManagementObjectCollection collection = searcher.Get();
            
            Int32 slots = 0;
            foreach (ManagementBaseObject management in collection)
            {
                slots = Convert.ToInt32(management["MemoryDevices"]);
            }

            return slots;
        }

        public static String? GetCPUManufacturer()
        {
            return GetStringFromSearcher("Manufacturer", "Win32_Processor");
        }

        public static Int32? GetCPUCurrentClockSpeed()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CurrentClockSpeed FROM Win32_Processor");
            using ManagementObjectCollection collection = searcher.Get();
            
            return collection.AsEnumerable().TrySelect(management => (Int32?) Convert.ToInt32(management["CurrentClockSpeed"])).FirstOrDefault();
        }

        public static String? GetDefaultIPGateway()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT IPEnabled, DefaultIPGateway FROM Win32_NetworkAdapterConfiguration");
            using ManagementObjectCollection collection = searcher.Get();
            
            return collection.AsEnumerable()
                .Where(management => management["IPEnabled"] is Boolean ip && ip)
                .Select(management => management["DefaultIPGateway"]).FirstOrDefault()?.ToString();
        }

        public static UInt32? GetCPUSpeedInMHz()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CurrentClockSpeed FROM Win32_Processor");
            using ManagementObjectCollection collection = searcher.Get();
            
            return collection.AsEnumerable().TrySelect(management => (UInt32?) Convert.ToUInt32(management["CurrentClockSpeed"])).FirstOrDefault();
        }

        public static String? GetCurrentLanguage()
        {
            return GetStringFromSearcher("CurrentLanguage", "Win32_BIOS");
        }

        public static String? GetProcessorInformation()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Name, Caption, SocketDesignation FROM Win32_Processor");
            using ManagementObjectCollection collection = searcher.Get();
            
            StringBuilder result = new StringBuilder();
            foreach (ManagementBaseObject management in collection)
            {
                String? name = management["Name"].ToString()?
                    .Replace("(TM)", "™", StringComparison.OrdinalIgnoreCase)
                    .Replace("(R)", "®", StringComparison.OrdinalIgnoreCase)
                    .Replace("(C)", "©", StringComparison.OrdinalIgnoreCase)
                    .Replace("    ", " ").Replace("  ", " ");

                result.Append(name);
                result.Append(", ".Join(management["Caption"], management["SocketDesignation"]));
            }

            return result.Length > 0 ? result.ToString() : null;
        }

        internal static PhysicalDriveInfo GetPhysicalDrive(ManagementBaseObject management)
        {
            if (management is null)
            {
                throw new ArgumentNullException(nameof(management));
            }

            return new PhysicalDriveInfo
            {
                UniqueIdFormat = management.TryGetValue(nameof(PhysicalDriveInfo.UniqueIdFormat), out UInt16 id) ? id : null,
                DeviceId = management.TryGetValue(nameof(PhysicalDriveInfo.DeviceId), out String? deviceid) ? deviceid : null,
                FriendlyName = management.TryGetValue(nameof(PhysicalDriveInfo.FriendlyName), out String? friendlyname) ? friendlyname : null,
                Description = management.TryGetValue(nameof(PhysicalDriveInfo.Description), out String? description) ? description : null,
                MediaType = management.TryGetValue(nameof(PhysicalDriveInfo.MediaType), out HardwareDriveType mediatype) ? mediatype : null,
                Size = management.TryGetValue(nameof(PhysicalDriveInfo.Size), out UInt64 size) ? size : null,
                AllocatedSize = management.TryGetValue(nameof(PhysicalDriveInfo.AllocatedSize), out UInt64 allocatesize) ? allocatesize : null,
                BusType = management.TryGetValue(nameof(PhysicalDriveInfo.BusType), out BusType bustype) ? bustype : null,
                PhysicalSectorSize = management.TryGetValue(nameof(PhysicalDriveInfo.PhysicalSectorSize), out UInt64 physicalsectorsize) ? physicalsectorsize : null,
                LogicalSectorSize = management.TryGetValue(nameof(PhysicalDriveInfo.LogicalSectorSize), out UInt64 logicalsectorsize) ? logicalsectorsize : null,
                SpindleSpeed = management.TryGetValue(nameof(PhysicalDriveInfo.SpindleSpeed), out UInt32 spindlespeed) ? spindlespeed : null
            };
        }
        
        public static PhysicalDriveInfo[] GetPhysicalDrives()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"\\.\root\microsoft\windows\storage", "SELECT * FROM MSFT_PhysicalDisk");
            using ManagementObjectCollection collection = searcher.Get();

            return collection.AsEnumerable().Select(GetPhysicalDrive).ToArray();
        }

        internal static HardwareDriveInfo GetHardwareDrive(ManagementBaseObject management)
        {
            if (management is null)
            {
                throw new ArgumentNullException(nameof(management));
            }

            return new HardwareDriveInfo
            {
                DeviceId = management.TryGetValue(nameof(HardwareDriveInfo.DeviceId), out String? deviceid) ? deviceid : null,
                Name = management.TryGetValue(nameof(HardwareDriveInfo.Name), out String? name) ? name : null,
                Index = management.TryGetValue(nameof(HardwareDriveInfo.Index), out UInt32 index) ? index : null,
                Model = management.TryGetValue(nameof(HardwareDriveInfo.Model), out String? model) ? model : null,
                Caption = management.TryGetValue(nameof(HardwareDriveInfo.Caption), out String? caption) ? caption : null,
                Description = management.TryGetValue(nameof(HardwareDriveInfo.Description), out String? description) ? description : null,
                InterfaceType = management.TryGetValue(nameof(HardwareDriveInfo.InterfaceType), out BusType interfacetype) ? interfacetype : null,
                Manufacturer = management.TryGetValue(nameof(HardwareDriveInfo.Manufacturer), out String? manufacturer) ? manufacturer : null,
                MediaType = management.TryGetValue(nameof(HardwareDriveInfo.MediaType), out String? mediatype) ? mediatype : null,
                SerialNumber = management.TryGetValue(nameof(HardwareDriveInfo.SerialNumber), out String? serialnumber) ? serialnumber.Trim() : null,
                FirmwareRevision = management.TryGetValue(nameof(HardwareDriveInfo.FirmwareRevision), out String? firmwarerevision) ? firmwarerevision : null,
                Size = management.TryGetValue(nameof(HardwareDriveInfo.Size), out UInt64 size) ? size : null,
                Partitions = management.TryGetValue(nameof(HardwareDriveInfo.Partitions), out UInt32 partitions) ? partitions : null,
                TotalHeads = management.TryGetValue(nameof(HardwareDriveInfo.TotalHeads), out UInt32 totalheads) ? totalheads : null,
                TotalCylinders = management.TryGetValue(nameof(HardwareDriveInfo.TotalCylinders), out UInt32 totalcylinders) ? totalcylinders : null,
                TotalTracks = management.TryGetValue(nameof(HardwareDriveInfo.TotalTracks), out UInt32 totaltracks) ? totaltracks : null,
                TotalSectors = management.TryGetValue(nameof(HardwareDriveInfo.TotalSectors), out UInt32 totalsectors) ? totalsectors : null,
                TracksPerCylinder = management.TryGetValue(nameof(HardwareDriveInfo.TracksPerCylinder), out UInt32 trackspercylinder) ? trackspercylinder : null,
                SectorsPerTrack = management.TryGetValue(nameof(HardwareDriveInfo.SectorsPerTrack), out UInt32 sectorspertrack) ? sectorspertrack : null,
                BytesPerSector = management.TryGetValue(nameof(HardwareDriveInfo.BytesPerSector), out UInt32 bytespersector) ? bytespersector : null,
                InstallDate = management.TryGetValue(nameof(HardwareDriveInfo.InstallDate), out DateTime? installdate) ? installdate : null
            };
        }

        public static HardwareDriveInfo[] GetHardwareDrives()
        {
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            using ManagementObjectCollection collection = searcher.Get();

            return collection.AsEnumerable().Select(GetHardwareDrive).ToArray();
        }

        internal static LogicalDriveInfo GetLogicalDrive(ManagementBaseObject management)
        {
            if (management is null)
            {
                throw new ArgumentNullException(nameof(management));
            }

            return new LogicalDriveInfo
            {
                DeviceId = management.TryGetValue(nameof(LogicalDriveInfo.DeviceId), out String? deviceid) ? deviceid : null,
                Name = management.TryGetValue(nameof(LogicalDriveInfo.Name), out String? name) ? name : null,
                Caption = management.TryGetValue(nameof(LogicalDriveInfo.Caption), out String? caption) ? caption : null,
                Description = management.TryGetValue(nameof(LogicalDriveInfo.Description), out String? description) ? description : null,
                DriveType = management.TryGetValue(nameof(LogicalDriveInfo.DriveType), out DriveType drivetype) ? drivetype : null,
                FileSystem = management.TryGetValue(nameof(LogicalDriveInfo.FileSystem), out String? filesystem) ? filesystem : null,
                Size = management.TryGetValue(nameof(LogicalDriveInfo.Size), out UInt64 size) ? size : null,
                VolumeName = management.TryGetValue(nameof(LogicalDriveInfo.VolumeName), out String? volumename) ? volumename : null,
                VolumeSerialNumber = management.TryGetValue(nameof(LogicalDriveInfo.VolumeSerialNumber), out String? volumeserialnumber) ? volumeserialnumber : null
            };
        }

        public static LogicalDriveInfo[] GetLogicalDrives()
        {
            return GetLogicalDrives(null);
        }

        public static LogicalDriveInfo[] GetLogicalDrives(params Char[]? letter)
        {
            if (letter is not null && letter.Length <= 0)
            {
                return Array.Empty<LogicalDriveInfo>();
            }
            
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher(letter is not null ? $"SELECT * FROM Win32_LogicalDisk WHERE {letter.Select(character => $"DeviceId='{Char.ToUpperInvariant(character)}:'").Distinct().Join(" OR ")}" : "SELECT * FROM Win32_LogicalDisk");
            using ManagementObjectCollection collection = searcher.Get();

            return collection.AsEnumerable().Select(GetLogicalDrive).ToArray();
        }

        public static String? GetComputerName()
        {
            return GetStringFromSearcher("Name", "Win32_ComputerSystem");
        }
        
        public static String? GetMacAddress()
        {
            return GetPhysicalAddress()?.ToString();
        }

        public static PhysicalAddress? GetPhysicalAddress()
        {
            return GetNetworkInterface()?.GetPhysicalAddress();
        }

        public static NetworkInterface? GetNetworkInterface()
        {
            return GetNetworkInterfaces().FirstOrDefault();
        }

        public static IEnumerable<NetworkInterface> GetNetworkInterfaces()
        {
            return NetworkInterface.GetAllNetworkInterfaces().Where(network => network.OperationalStatus == OperationalStatus.Up && network.NetworkInterfaceType != NetworkInterfaceType.Loopback);
        }
    }
}