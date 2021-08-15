// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NetExtender.Configuration.Interfaces;
using NetExtender.Utilities.Types;
using NetExtender.Configuration.Windows.Registry;
using NetExtender.Registry;

namespace NetExtender.Workstation
{
    public enum OSVersion
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

    public enum OSType
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

    public readonly struct OSData
    {
        public OSType OSType { get; }
        public OSVersion OSVersion { get; }
        public SoftwareArchitecture Architecture { get; }

        public OSData(OSType type, OSVersion version, SoftwareArchitecture architecture)
        {
            OSType = type;
            OSVersion = version;
            Architecture = architecture;
        }
    }
    
    public static partial class Software
    {
        public static OSData OperatingSystem { get; } = GetOSVersion();

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

        public static SoftwareArchitecture GetOSArchitecture()
        {
            SoftwareArchitecture bit = IntPtr.Size switch
            {
                8 => SoftwareArchitecture.Bit64,
                4 => Is32BitProcessOn64BitProcessor() ? SoftwareArchitecture.Bit64 : SoftwareArchitecture.Bit32,
                _ => SoftwareArchitecture.Unknown
            };

            return bit;
        }

        private static readonly IReadOnlyConfig Config = new Configuration.Config(new RegistryConfigBehavior(RegistryKeys.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion"));

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

                String? exact = OSVersionRegistry;

                if (String.IsNullOrEmpty(exact))
                {
                    return Environment.OSVersion.Version.Major;
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

                String? exact = OSVersionRegistry;

                if (String.IsNullOrEmpty(exact))
                {
                    return Environment.OSVersion.Version.Minor;
                }

                String split = exact.Split('.').TryGetValue(1, "0");
                
                return Int32.TryParse(split, out Int32 value) ? value : 0;
            }
        }

        private static String? OSVersionRegistry
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
                return Config.GetValue("CurrentBuildNumber", 0);
            }
        }

        private static Int32 RevisionVersion
        {
            get
            {
                return IsWindows10() ? 0 : Environment.OSVersion.Version.Revision;
            }
        }

        public static String ServicePack
        {
            get
            {
                OSVERSIONINFOEX info = new OSVERSIONINFOEX {dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX))};

                return GetVersionEx(ref info) ? info.szCSDVersion : String.Empty;
            }
        }

        // ReSharper disable once CognitiveComplexity
        public static OSData GetOSVersion()
        {
            SoftwareArchitecture architecture = GetOSArchitecture();
            
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32S:
                    return new OSData(OSType.WindowsVeryOld, OSVersion.Win32S, architecture);
                case PlatformID.Win32Windows:
                    switch (Environment.OSVersion.Version.Minor)
                    {
                        case 0:
                            return new OSData(OSType.WindowsVeryOld, OSVersion.Win95, architecture);
                        case 10:
                            return new OSData(OSType.WindowsVeryOld, OSVersion.Win98, architecture);
                        case 90:
                            return new OSData(OSType.WindowsVeryOld, OSVersion.WinME, architecture);
                    }

                    break;

                case PlatformID.Win32NT:
                    switch (Environment.OSVersion.Version.Major)
                    {
                        case 3:
                            return new OSData(OSType.WindowsVeryOld, OSVersion.NT351, architecture);
                        case 4:
                            return new OSData(OSType.WindowsVeryOld, OSVersion.NT40, architecture);
                        case 5:
                            switch (Environment.OSVersion.Version.Minor)
                            {
                                case 0:
                                    return new OSData(OSType.WindowsVeryOld, OSVersion.Win2000, architecture);
                                case 1:
                                    return new OSData(OSType.WindowsOld, OSVersion.WinXP, architecture);
                                case 2:
                                    return new OSData(OSType.WindowsVeryOld, OSVersion.Win2003, architecture);
                            }

                            break;

                        case 6:
                            switch (Environment.OSVersion.Version.Minor)
                            {
                                case 0:
                                    return new OSData(OSType.WindowsOld, OSVersion.Vista, architecture);
                                case 1:
                                    return new OSData(OSType.Windows, OSVersion.Win7, architecture);
                                case 2:
                                    return new OSData(OSType.Windows, OSVersion.Win8, architecture);
                                case 3:
                                    return new OSData(OSType.Windows, OSVersion.Win81, architecture);
                            }

                            break;
                        case 10: //this will only show up if the application has a manifest file allowing W10, otherwise a 6.2 version will be used
                            return new OSData(OSType.Windows, OSVersion.Win10, architecture);
                    }

                    break;

                case PlatformID.WinCE:
                    return new OSData(OSType.Windows, OSVersion.WinCE, architecture);
                case PlatformID.Unix:
                    return new OSData(OSType.Unix, OSVersion.Unix, architecture);
                case PlatformID.Xbox:
                    return new OSData(OSType.Xbox, OSVersion.XBox, architecture);
                case PlatformID.MacOSX:
                    return new OSData(OSType.MacOS, OSVersion.MacOS, architecture);
                default:
                    break;
            }

            return new OSData(OSType.Unknown, OSVersion.Unknown, architecture);
        }
    }
}