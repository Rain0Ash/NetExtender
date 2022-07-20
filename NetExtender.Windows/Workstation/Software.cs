// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Windows.Registry;
using NetExtender.Registry;
using NetExtender.Utilities.Configuration;
using NetExtender.Utilities.Types;

namespace NetExtender.Workstation
{
    public enum OperationSystemVersion
    {
        Unknown,
        Win32S,
        Win95,
        Win98,
        WinME,
        NT351,
        NT40,
        Win2000,
        WinXP,
        Win2003,
        Vista,
        Win7,
        Win8,
        Win81,
        Win10,
        WinCE,
        Unix,
        MacOS,
        XBox
    }

    public enum OperationSystemType
    {
        Unknown,
        WindowsVeryOld,
        WindowsOld,
        Windows,
        Unix,
        MacOS,
        Xbox
    }

    public enum SoftwareArchitecture
    {
        Unknown,
        Bit32,
        Bit64
    }

    public readonly struct OperationSystemInfo
    {
        public OperationSystemType Type { get; }
        public OperationSystemVersion Version { get; }
        public SoftwareArchitecture Architecture { get; }

        public OperationSystemInfo(OperationSystemType type, OperationSystemVersion version, SoftwareArchitecture architecture)
        {
            Type = type;
            Version = version;
            Architecture = architecture;
        }
    }
    
    public static partial class Software
    {
        public static OperationSystemInfo Information { get; } = GetOperatingSystemInfo();

        public static OperatingSystem SystemVersion
        {
            get
            {
                return Environment.OSVersion;
            }
        }

        public static SoftwareArchitecture GetSoftwareArchitecture()
        {
            return IntPtr.Size switch
            {
                8 => SoftwareArchitecture.Bit64,
                4 => SoftwareArchitecture.Bit32,
                _ => SoftwareArchitecture.Unknown
            };
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(String name);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
        private static extern IntPtr GetProcAddress(IntPtr hwnd, String procedure);

        [DllImport("kernel32.dll")]
        private static extern Boolean IsWow64Process([In] IntPtr handle, [Out] out Boolean isWow64Process);

        private static Boolean Is32BitProcessOn64BitProcessor()
        {
            return IsWow64Process(Process.GetCurrentProcess().Handle, out Boolean isWow64) && isWow64;
        }

        public static SoftwareArchitecture GetOperationSystemArchitecture()
        {
            SoftwareArchitecture bit = IntPtr.Size switch
            {
                8 => SoftwareArchitecture.Bit64,
                4 => Is32BitProcessOn64BitProcessor() ? SoftwareArchitecture.Bit64 : SoftwareArchitecture.Bit32,
                _ => SoftwareArchitecture.Unknown
            };

            return bit;
        }

        private static IConfig Config { get; } = new RegistryConfigBehavior(RegistryKeys.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion").Create();

        private static Boolean IsWindows10()
        {
            String? product = Config.GetValue("ProductName");
            return product is not null && product.StartsWith("Windows 10", StringComparison.OrdinalIgnoreCase);
        }

        private static Int32 MajorVersion
        {
            get
            {
                if (IsWindows10())
                {
                    return 10;
                }

                String? exact = SystemVersionRegistry;

                if (String.IsNullOrEmpty(exact))
                {
                    return SystemVersion.Version.Major;
                }

                String split = exact.Split('.').TryGetValue(1, "0");
                
                return Int32.TryParse(split, out Int32 value) ? value : 0;
            }
        }

        private static Int32 MinorVersion
        {
            get
            {
                if (IsWindows10())
                {
                    return 0;
                }

                String? exact = SystemVersionRegistry;

                if (String.IsNullOrEmpty(exact))
                {
                    return SystemVersion.Version.Minor;
                }

                String split = exact.Split('.').TryGetValue(1, "0");
                
                return Int32.TryParse(split, out Int32 value) ? value : 0;
            }
        }

        private static String? SystemVersionRegistry
        {
            get
            {
                return Config.GetValue("CurrentVersion", String.Empty);
            }
        }

        public static Version Version
        {
            get
            {
                return new Version(MajorVersion, MinorVersion, BuildVersion, RevisionVersion);
            }
        }

        private static Int32 BuildVersion
        {
            get
            {
                return Int32.TryParse(Config.GetValue("CurrentBuildNumber"), out Int32 version) ? version : 0;
            }
        }

        private static Int32 RevisionVersion
        {
            get
            {
                return IsWindows10() ? 0 : SystemVersion.Version.Revision;
            }
        }

        public static String ServicePack
        {
            get
            {
                WindowsSystemVersionInfo info = new WindowsSystemVersionInfo {SystemVersionInfoSize = Marshal.SizeOf(typeof(WindowsSystemVersionInfo))};

                return GetVersionEx(ref info) ? info.Version : String.Empty;
            }
        }

        // ReSharper disable once CognitiveComplexity
        public static OperationSystemInfo GetOperatingSystemInfo()
        {
            SoftwareArchitecture architecture = GetOperationSystemArchitecture();
            
            switch (SystemVersion.Platform)
            {
                case PlatformID.Win32S:
                    return new OperationSystemInfo(OperationSystemType.WindowsVeryOld, OperationSystemVersion.Win32S, architecture);
                case PlatformID.Win32Windows:
                    switch (SystemVersion.Version.Minor)
                    {
                        case 0:
                            return new OperationSystemInfo(OperationSystemType.WindowsVeryOld, OperationSystemVersion.Win95, architecture);
                        case 10:
                            return new OperationSystemInfo(OperationSystemType.WindowsVeryOld, OperationSystemVersion.Win98, architecture);
                        case 90:
                            return new OperationSystemInfo(OperationSystemType.WindowsVeryOld, OperationSystemVersion.WinME, architecture);
                    }

                    break;

                case PlatformID.Win32NT:
                    switch (SystemVersion.Version.Major)
                    {
                        case 3:
                            return new OperationSystemInfo(OperationSystemType.WindowsVeryOld, OperationSystemVersion.NT351, architecture);
                        case 4:
                            return new OperationSystemInfo(OperationSystemType.WindowsVeryOld, OperationSystemVersion.NT40, architecture);
                        case 5:
                            switch (SystemVersion.Version.Minor)
                            {
                                case 0:
                                    return new OperationSystemInfo(OperationSystemType.WindowsVeryOld, OperationSystemVersion.Win2000, architecture);
                                case 1:
                                    return new OperationSystemInfo(OperationSystemType.WindowsOld, OperationSystemVersion.WinXP, architecture);
                                case 2:
                                    return new OperationSystemInfo(OperationSystemType.WindowsVeryOld, OperationSystemVersion.Win2003, architecture);
                            }

                            break;

                        case 6:
                            switch (SystemVersion.Version.Minor)
                            {
                                case 0:
                                    return new OperationSystemInfo(OperationSystemType.WindowsOld, OperationSystemVersion.Vista, architecture);
                                case 1:
                                    return new OperationSystemInfo(OperationSystemType.Windows, OperationSystemVersion.Win7, architecture);
                                case 2:
                                    return new OperationSystemInfo(OperationSystemType.Windows, OperationSystemVersion.Win8, architecture);
                                case 3:
                                    return new OperationSystemInfo(OperationSystemType.Windows, OperationSystemVersion.Win81, architecture);
                            }

                            break;
                        case 10: //this will only show up if the application has a manifest file allowing W10, otherwise a 6.2 version will be used
                            return new OperationSystemInfo(OperationSystemType.Windows, OperationSystemVersion.Win10, architecture);
                    }

                    break;

                case PlatformID.WinCE:
                    return new OperationSystemInfo(OperationSystemType.Windows, OperationSystemVersion.WinCE, architecture);
                case PlatformID.Unix:
                    return new OperationSystemInfo(OperationSystemType.Unix, OperationSystemVersion.Unix, architecture);
                case PlatformID.Xbox:
                    return new OperationSystemInfo(OperationSystemType.Xbox, OperationSystemVersion.XBox, architecture);
                case PlatformID.MacOSX:
                    return new OperationSystemInfo(OperationSystemType.MacOS, OperationSystemVersion.MacOS, architecture);
                default:
                    break;
            }

            return new OperationSystemInfo(OperationSystemType.Unknown, OperationSystemVersion.Unknown, architecture);
        }
    }
}