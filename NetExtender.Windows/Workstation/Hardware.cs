// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using NetExtender.Utilities.Types;

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
        /// <summary>
        /// Retrieving Processor Id.
        /// </summary>
        /// <returns></returns>
        /// 
        public static String? GetProcessorID()
        {
            String? id = String.Empty;
            
            ManagementClass management = new ManagementClass("win32_processor");
            ManagementObjectCollection collection = management.GetInstances();
            
            foreach (ManagementBaseObject obj in collection)
            {
                id = obj.Properties["processorID"].Value.ToString();
                break;
            }

            return id;
        }

        [DllImport("kernel32.dll")]
        private static extern void GetSystemInfo([MarshalAs(UnmanagedType.Struct)] ref SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll")]
        private static extern void GetNativeSystemInfo([MarshalAs(UnmanagedType.Struct)] ref SYSTEM_INFO lpSystemInfo);

        private static String? GetWmiPropertyValueAsString(String query, String property)
        {
            SelectQuery selector = new SelectQuery(query);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(selector);
            
            foreach (ManagementBaseObject obj in searcher.Get())
            {
                if (obj is ManagementObject management)
                {
                    return management.Properties[property].Value.ToString();
                }
            }

            return null;
        }
        
        public static DateTime GetWmiPropertyValueAsDateTime(String query, String property)
        {
            String? value = GetWmiPropertyValueAsString(query, property);

            if (String.IsNullOrEmpty(value))
            {
                throw new ManagementException();
            }
            
            return ManagementDateTimeConverter.ToDateTime(value);
        } 
        
        public static DateTime GetBootDateTime()
        {
            return GetWmiPropertyValueAsDateTime("SELECT * FROM Win32_OperatingSystem WHERE Primary='true'", "LastBootUpTime");
        }
        
        public static ProcessorArchitecture GetProcessorBits()
        {
            ProcessorArchitecture pbits = ProcessorArchitecture.Unknown;

            try
            {
                SYSTEM_INFO lSystemInfo = new SYSTEM_INFO();
                GetNativeSystemInfo(ref lSystemInfo);

                pbits = lSystemInfo.uProcessorInfo.wProcessorArchitecture switch
                {
                    12 => ProcessorArchitecture.Arm64, // ARM64
                    9 => ProcessorArchitecture.Bit64, // AMD64
                    6 => ProcessorArchitecture.Itanium64, // IA64
                    5 => ProcessorArchitecture.Arm, // ARM
                    0 => ProcessorArchitecture.Bit32, // Intel
                    _ => ProcessorArchitecture.Unknown
                };
            }
            catch
            {
                // Ignore        
            }

            return pbits;
        }

        /// <summary>
        /// Retrieving HDD Serial Number.
        /// </summary>
        /// <returns></returns>
        public static String GetHDDSerialNumber()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            String result = String.Empty;
            foreach (ManagementBaseObject strt in mcol)
            {
                result += Convert.ToString(strt["VolumeSerialNumber"]);
            }

            return result;
        }

        /// <summary>
        /// Retrieving System MAC Address.
        /// </summary>
        /// <returns></returns>
        public static String? GetMACAddress()
        {
            ManagementObjectCollection collection = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
            
            String? mac = String.Empty;
            foreach (ManagementObject management in collection.OfType<ManagementObject>())
            {
                if (mac.IsEmpty())
                {
                    if ((Boolean) management["IPEnabled"])
                    {
                        mac = management["MacAddress"].ToString();
                    }
                }

                management.Dispose();
            }

            return mac;
        }

        /// <summary>
        /// Retrieving Motherboard Manufacturer.
        /// </summary>
        /// <returns></returns>
        public static String? GetBoardMaker()
        {
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

            foreach (ManagementBaseObject management in searcher.Get())
            {
                ManagementObject wmi = (ManagementObject) management;
                
                try
                {
                    return wmi.GetPropertyValue("Manufacturer").ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieving Motherboard Product Id.
        /// </summary>
        /// <returns></returns>
        public static String? GetBoardProductID()
        {
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");

            foreach (ManagementBaseObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Product").ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieving CD-DVD Drive Path.
        /// </summary>
        /// <returns></returns>
        public static String? GetCDRomDrive()
        {
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_CDROMDrive");

            foreach (ManagementBaseObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Drive").ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieving BIOS Maker.
        /// </summary>
        /// <returns></returns>
        public static String? GetBIOSMaker()
        {
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");

            foreach (ManagementBaseObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Manufacturer").ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieving BIOS Serial Number.
        /// </summary>
        /// <returns></returns>
        public static String? GetBIOSSerialNumber()
        {
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");

            foreach (ManagementBaseObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("SerialNumber").ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieving BIOS Caption.
        /// </summary>
        /// <returns></returns>
        public static String? GetBIOSCaption()
        {
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");

            foreach (ManagementBaseObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Caption").ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieving System Account Name.
        /// </summary>
        /// <returns></returns>
        public static String? GetAccountName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_UserAccount");

            foreach (ManagementBaseObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Name").ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieving Physical RAM Memory in bytes.
        /// </summary>
        /// <returns></returns>
        public static Int64 GetPhysicalMemory()
        {
            ManagementScope scope = new ManagementScope();
            ObjectQuery query = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection collection = searcher.Get();

            Int64 memSize = 0;

            // In case more than one Memory sticks are installed
            foreach (ManagementBaseObject obj in collection)
            {
                Int64 mCap = Convert.ToInt64(obj["Capacity"]);
                memSize += mCap;
            }

            return memSize;
        }

        /// <summary>
        /// Retrieving Number of RAM Slot on Motherboard.
        /// </summary>
        /// <returns></returns>
        public static Int32 GetRAMSlots()
        {
            Int32 slots = 0;
            
            ManagementScope scope = new ManagementScope();
            ObjectQuery query = new ObjectQuery("SELECT MemoryDevices FROM Win32_PhysicalMemoryArray");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection collection = searcher.Get();
            
            foreach (ManagementBaseObject obj in collection)
            {
                slots = Convert.ToInt32(obj["MemoryDevices"]);
            }

            return slots;
        }

        //Get CPU Temprature.
        /// <summary>
        /// method for retrieving the CPU Manufacturer
        /// using the WMI class
        /// </summary>
        /// <returns>CPU Manufacturer</returns>
        public static String? GetCPUManufacturer()
        {
            //create an instance of the Managemnet class with the
            //Win32_Processor class
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            ManagementObjectCollection collection = mgmt.GetInstances();
            
            String? cpu = String.Empty;
            //start our loop for all processors found
            foreach (ManagementBaseObject management in collection)
            {
                if (cpu.IsEmpty())
                {
                    // only return manufacturer from first CPU
                    cpu = management.Properties["Manufacturer"].Value.ToString();
                }
            }

            return cpu;
        }

        /// <summary>
        /// method to retrieve the CPU's current
        /// clock speed using the WMI class
        /// </summary>
        /// <returns>Clock speed</returns>
        public static Int32 GetCPUCurrentClockSpeed()
        {
            Int32 cpuClockSpeed = 0;
            //create an instance of the Managemnet class with the
            //Win32_Processor class
            ManagementClass mgmt = new ManagementClass("Win32_Processor");
            //create a ManagementObjectCollection to loop through
            ManagementObjectCollection objCol = mgmt.GetInstances();
            //start our loop for all processors found
            foreach (ManagementBaseObject obj in objCol)
            {
                if (cpuClockSpeed == 0)
                {
                    // only return cpuStatus from first CPU
                    cpuClockSpeed = Convert.ToInt32(obj.Properties["CurrentClockSpeed"].Value.ToString());
                }
            }

            //return the status
            return cpuClockSpeed;
        }

        /// <summary>
        /// method to retrieve the network adapters
        /// default IP gateway using WMI
        /// </summary>
        /// <returns>adapters default IP gateway</returns>
        public static String? GetDefaultIPGateway()
        {
            //create out management class object using the
            //Win32_NetworkAdapterConfiguration class to get the attributes
            //of the network adapter
            ManagementClass mgmt = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //create our ManagementObjectCollection to get the attributes with
            ManagementObjectCollection collection = mgmt.GetInstances();
            
            String? gateway = String.Empty;
            
            //loop through all the objects we find
            foreach (ManagementBaseObject management in collection)
            {
                if (gateway.IsEmpty()) // only return MAC Address from first card
                {
                    //grab the value from the first network adapter we find
                    //you can change the string to an array and get all
                    //network adapters found as well
                    //check to see if the adapter's IPEnabled
                    //equals true
                    if ((Boolean) management["IPEnabled"])
                    {
                        gateway = management["DefaultIPGateway"].ToString();
                    }
                }

                //dispose of our object
                management.Dispose();
            }

            //return the mac address
            return gateway?.Replace(":", String.Empty);
        }

        /// <summary>
        /// Retrieve CPU Speed.
        /// </summary>
        /// <returns></returns>
        public static Double? GetCPUSpeedInGHz()
        {
            Double? gHz = null;
            using ManagementClass mc = new ManagementClass("Win32_Processor");
            foreach (ManagementBaseObject mo in mc.GetInstances())
            {
                gHz = 0.001 * (UInt32) mo.Properties["CurrentClockSpeed"].Value;
                break;
            }

            return gHz;
        }

        /// <summary>
        /// Retrieving Current Language
        /// </summary>
        /// <returns></returns>
        public static String? GetCurrentLanguage()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");

            foreach (ManagementBaseObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("CurrentLanguage").ToString();
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieving Current Language.
        /// </summary>
        /// <returns></returns>
        public static String? GetOSInformation()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");

            foreach (ManagementBaseObject wmi in searcher.Get())
            {
                try
                {
                    return ((String) wmi["Caption"]).Trim() + ", " + (String) wmi["Version"] + ", " + (String) wmi["OSArchitecture"];
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieving Processor Information.
        /// </summary>
        /// <returns></returns>
        public static String? GetProcessorInformation()
        {
            String? info = null;
            
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();
            
            foreach (ManagementBaseObject obj in moc)
            {
                String name = (String) obj["Name"];
                name = name.Replace("(TM)", "™").Replace("(tm)", "™").Replace("(R)", "®").Replace("(r)", "®")
                    .Replace("(C)", "©").Replace("(c)", "©").Replace("    ", " ").Replace("  ", " ");

                info = name + ", " + (String) obj["Caption"] + ", " + (String) obj["SocketDesignation"];
                //mo.Properties["Name"].Value.ToString();
                //break;
            }

            return info;
        }

        public static IOrderedEnumerable<HardwareDiskInfo> GetDisks()
        {
            ManagementScope scope = new ManagementScope(@"\\.\root\microsoft\windows\storage");
            
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM MSFT_PhysicalDisk");
            
            scope.Connect();
            
            searcher.Scope = scope;

            return searcher.Get().OfType<ManagementBaseObject>()
                .Select(disk => new HardwareDiskInfo(
                    Convert.ToUInt16(disk["UniqueIdFormat"]), Convert.ToString(disk["DeviceId"]),
                    Convert.ToString(disk["FriendlyName"]), Convert.ToString(disk["Description"]),
                    Convert.ToUInt16(disk["MediaType"]), Convert.ToUInt64(disk["Size"]),
                    Convert.ToUInt64(disk["AllocatedSize"]), Convert.ToUInt16(disk["BusType"]),
                    Convert.ToUInt64(disk["PhysicalSectorSize"]), Convert.ToUInt64(disk["LogicalSectorSize"]),
                    Convert.ToUInt32(disk["SpindleSpeed"])))
                .OrderBy(disk => disk.DeviceID);
        }
        
        /// <summary>
        /// Retrieving Computer Name.
        /// </summary>
        /// <returns></returns>
        public static String? GetComputerName()
        {
            String? info = null;
            
            ManagementClass management = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection collection = management.GetInstances();
            
            foreach (ManagementBaseObject obj in collection)
            {
                info = obj["Name"].ToString();
            }

            return info;
        }
        
        /// <summary>
        /// Returns the MAC (Media Access Control or physical address) for the first currently active (can transmit data packets) network interface (adapter) on the local computer.
        /// <para>If you want a <see cref="PhysicalAddress"/>, call <see cref="GetPhysicalAddress"/>.</para>
        /// </summary>
        public static String? GetMacAddress()
        {
            return GetPhysicalAddress()?.ToString();
        }

        /// <summary>
        /// Returns the physical address (or Media Access Control (MAC)) for the first currently active (can transmit data packets) network interface (adapter) on the local computer.
        /// <para>If you want a string, call <see cref="GetMacAddress"/>.</para>
        /// </summary>
        public static PhysicalAddress? GetPhysicalAddress()
        {
            return GetNetworkInterface()?.GetPhysicalAddress();
        }

        /// <summary>
        /// Returns the first currently active (can transmit data packets) network interface (adapter) on the local computer.
        /// </summary>
        public static NetworkInterface? GetNetworkInterface()
        {
            return GetNetworkInterfaces().FirstOrDefault();
        }

        /// <summary>
        /// Returns the currently active (can transmit data packets) network interfaces (adapters) on the local computer.
        /// </summary>
        public static IEnumerable<NetworkInterface> GetNetworkInterfaces()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.OperationalStatus == OperationalStatus.Up
                             && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback);
        }
    }
}