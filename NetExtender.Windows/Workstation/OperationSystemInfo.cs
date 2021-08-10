// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace NetExtender.Workstation
{
    /// <summary>
    /// Provides detailed information about the host operating system.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    public static partial class Software
    {
        private static String? edition;

        /// <summary>
        /// Gets the edition of the operating system running on this computer.
        /// </summary>
        public static String Edition
        {
            get
            {
                if (edition is not null)
                {
                    return edition;
                }

                OperatingSystem version = Environment.OSVersion;
                OSVERSIONINFOEX versioninfo = new OSVERSIONINFOEX
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX))
                };

                if (!GetVersionEx(ref versioninfo))
                {
                    return edition ??= String.Empty;
                }

                Int32 major = version.Version.Major;
                Int32 minor = version.Version.Minor;
                Byte product = versioninfo.wProductType;
                Int16 suitemask = versioninfo.wSuiteMask;

                #region VERSION 4

                switch (major)
                {
                    case 4 when product == VER_NT_WORKSTATION:
                        // Windows NT 4.0 Workstation
                        edition = "Workstation";
                        break;
                    case 4:
                    {
                        if (product == VER_NT_SERVER)
                        {
                            edition = (suitemask & VER_SUITE_ENTERPRISE) != 0 ? "Enterprise Server" : "Standard Server";
                        }

                        break;
                    }
                    case 5 when product == VER_NT_WORKSTATION:
                    {
                        if ((suitemask & VER_SUITE_PERSONAL) != 0)
                        {
                            edition = "Home";
                        }
                        else
                        {
                            edition = GetSystemMetrics(86) == 0 ? "Professional" : "Tablet Edition";
                        }

                        break;
                    }
                    case 5:
                    {
                        if (product == VER_NT_SERVER)
                        {
                            if (minor == 0)
                            {
                                if ((suitemask & VER_SUITE_DATACENTER) != 0)
                                {
                                    // Windows 2000 Datacenter Server
                                    edition = "Datacenter Server";
                                }
                                else if ((suitemask & VER_SUITE_ENTERPRISE) != 0)
                                {
                                    // Windows 2000 Advanced Server
                                    edition = "Advanced Server";
                                }
                                else
                                {
                                    // Windows 2000 Server
                                    edition = "Server";
                                }
                            }
                            else
                            {
                                if ((suitemask & VER_SUITE_DATACENTER) != 0)
                                {
                                    // Windows Server 2003 Datacenter Edition
                                    edition = "Datacenter";
                                }
                                else if ((suitemask & VER_SUITE_ENTERPRISE) != 0)
                                {
                                    // Windows Server 2003 Enterprise Edition
                                    edition = "Enterprise";
                                }
                                else if ((suitemask & VER_SUITE_BLADE) != 0)
                                {
                                    // Windows Server 2003 Web Edition
                                    edition = "Web Edition";
                                }
                                else
                                {
                                    // Windows Server 2003 Standard Edition
                                    edition = "Standard";
                                }
                            }
                        }

                        break;
                    }
                    case 6:
                    {
                        if (GetProductInfo(major, minor,
                            versioninfo.wServicePackMajor, versioninfo.wServicePackMinor,
                            out Int32 ed))
                        {
                            edition = ed switch
                            {
                                PRODUCT_BUSINESS => "Business",
                                PRODUCT_BUSINESS_N => "Business N",
                                PRODUCT_CLUSTER_SERVER => "HPC Edition",
                                PRODUCT_CLUSTER_SERVER_V => "HPC Edition without Hyper-V",
                                PRODUCT_DATACENTER_SERVER => "Datacenter Server",
                                PRODUCT_DATACENTER_SERVER_CORE => "Datacenter Server (core installation)",
                                PRODUCT_DATACENTER_SERVER_V => "Datacenter Server without Hyper-V",
                                PRODUCT_DATACENTER_SERVER_CORE_V => "Datacenter Server without Hyper-V (core installation)",
                                PRODUCT_EMBEDDED => "Embedded",
                                PRODUCT_ENTERPRISE => "Enterprise",
                                PRODUCT_ENTERPRISE_N => "Enterprise N",
                                PRODUCT_ENTERPRISE_E => "Enterprise E",
                                PRODUCT_ENTERPRISE_SERVER => "Enterprise Server",
                                PRODUCT_ENTERPRISE_SERVER_CORE => "Enterprise Server (core installation)",
                                PRODUCT_ENTERPRISE_SERVER_CORE_V => "Enterprise Server without Hyper-V (core installation)",
                                PRODUCT_ENTERPRISE_SERVER_IA64 => "Enterprise Server for Itanium-based Systems",
                                PRODUCT_ENTERPRISE_SERVER_V => "Enterprise Server without Hyper-V",
                                PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT => "Essential Business Server MGMT",
                                PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL => "Essential Business Server ADDL",
                                PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC => "Essential Business Server MGMTSVC",
                                PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC => "Essential Business Server ADDLSVC",
                                PRODUCT_HOME_BASIC => "Home Basic",
                                PRODUCT_HOME_BASIC_N => "Home Basic N",
                                PRODUCT_HOME_BASIC_E => "Home Basic E",
                                PRODUCT_HOME_PREMIUM => "Home Premium",
                                PRODUCT_HOME_PREMIUM_N => "Home Premium N",
                                PRODUCT_HOME_PREMIUM_E => "Home Premium E",
                                PRODUCT_HOME_PREMIUM_SERVER => "Home Premium Server",
                                PRODUCT_HYPERV => "Microsoft Hyper-V Server",
                                PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT => "Windows Essential Business Management Server",
                                PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING => "Windows Essential Business Messaging Server",
                                PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY => "Windows Essential Business Security Server",
                                PRODUCT_PROFESSIONAL => "Professional",
                                PRODUCT_PROFESSIONAL_N => "Professional N",
                                PRODUCT_PROFESSIONAL_E => "Professional E",
                                PRODUCT_SB_SOLUTION_SERVER => "SB Solution Server",
                                PRODUCT_SB_SOLUTION_SERVER_EM => "SB Solution Server EM",
                                PRODUCT_SERVER_FOR_SB_SOLUTIONS => "Server for SB Solutions",
                                PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM => "Server for SB Solutions EM",
                                PRODUCT_SERVER_FOR_SMALLBUSINESS => "Windows Essential Server Solutions",
                                PRODUCT_SERVER_FOR_SMALLBUSINESS_V => "Windows Essential Server Solutions without Hyper-V",
                                PRODUCT_SERVER_FOUNDATION => "Server Foundation",
                                PRODUCT_SMALLBUSINESS_SERVER => "Windows Small Business Server",
                                PRODUCT_SMALLBUSINESS_SERVER_PREMIUM => "Windows Small Business Server Premium",
                                PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE => "Windows Small Business Server Premium (core installation)",
                                PRODUCT_SOLUTION_EMBEDDEDSERVER => "Solution Embedded Server",
                                PRODUCT_SOLUTION_EMBEDDEDSERVER_CORE => "Solution Embedded Server (core installation)",
                                PRODUCT_STANDARD_SERVER => "Standard Server",
                                PRODUCT_STANDARD_SERVER_CORE => "Standard Server (core installation)",
                                PRODUCT_STANDARD_SERVER_SOLUTIONS => "Standard Server Solutions",
                                PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE => "Standard Server Solutions (core installation)",
                                PRODUCT_STANDARD_SERVER_CORE_V => "Standard Server without Hyper-V (core installation)",
                                PRODUCT_STANDARD_SERVER_V => "Standard Server without Hyper-V",
                                PRODUCT_STARTER => "Starter",
                                PRODUCT_STARTER_N => "Starter N",
                                PRODUCT_STARTER_E => "Starter E",
                                PRODUCT_STORAGE_ENTERPRISE_SERVER => "Enterprise Storage Server",
                                PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE => "Enterprise Storage Server (core installation)",
                                PRODUCT_STORAGE_EXPRESS_SERVER => "Express Storage Server",
                                PRODUCT_STORAGE_EXPRESS_SERVER_CORE => "Express Storage Server (core installation)",
                                PRODUCT_STORAGE_STANDARD_SERVER => "Standard Storage Server",
                                PRODUCT_STORAGE_STANDARD_SERVER_CORE => "Standard Storage Server (core installation)",
                                PRODUCT_STORAGE_WORKGROUP_SERVER => "Workgroup Storage Server",
                                PRODUCT_STORAGE_WORKGROUP_SERVER_CORE => "Workgroup Storage Server (core installation)",
                                PRODUCT_UNDEFINED => "Unknown product",
                                PRODUCT_ULTIMATE => "Ultimate",
                                PRODUCT_ULTIMATE_N => "Ultimate N",
                                PRODUCT_ULTIMATE_E => "Ultimate E",
                                PRODUCT_WEB_SERVER => "Web Server",
                                PRODUCT_WEB_SERVER_CORE => "Web Server (core installation)",
                                _ => edition
                            };
                        }

                        break;
                    }
                }

                #endregion VERSION 6

                return edition ??= String.Empty;
            }
        }

        private static String? name;

        /// <summary>
        /// Gets the name of the operating system running on this computer.
        /// </summary>
        public static String Name
        {
            get
            {
                if (name is not null)
                {
                    return name;
                }

                OperatingSystem version = Environment.OSVersion;
                OSVERSIONINFOEX versioninfo = new OSVERSIONINFOEX
                {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX))
                };

                if (!GetVersionEx(ref versioninfo))
                {
                    return name ??= String.Empty;
                }

                Int32 major = version.Version.Major;
                Int32 minor = version.Version.Minor;

                if (major == 6 && minor == 2)
                {
                    //The registry read workaround is by Scott Vickery. Thanks a lot for the help!

                    //http://msdn.microsoft.com/en-us/library/windows/desktop/ms724832(v=vs.85).aspx

                    // For applications that have been manifested for Windows 8.1 & Windows 10. Applications not manifested for 8.1 or 10 will return the Windows 8 OS version value (6.2). 
                    // By reading the registry, we'll get the exact version - meaning we can even compare against  Win 8 and Win 8.1.
                    String? exactversion = OSVersionRegistry;
                    if (!String.IsNullOrEmpty(exactversion))
                    {
                        String[] splitResult = exactversion.Split('.');
                        major = Convert.ToInt32(splitResult[0]);
                        minor = Convert.ToInt32(splitResult[1]);
                    }

                    if (IsWindows10())
                    {
                        major = 10;
                        minor = 0;
                    }
                }

                switch (version.Platform)
                {
                    case PlatformID.Win32S:
                        name = "Windows 3.1";
                        break;
                    case PlatformID.WinCE:
                        name = "Windows CE";
                        break;
                    case PlatformID.Win32Windows:
                    {
                        if (major == 4)
                        {
                            String csdVersion = versioninfo.szCSDVersion;
                            switch (minor)
                            {
                                case 0:
                                    if (csdVersion == "B" || csdVersion == "C")
                                    {
                                        name = "Windows 95 OSR2";
                                    }
                                    else
                                    {
                                        name = "Windows 95";
                                    }

                                    break;
                                case 10:
                                    name = csdVersion == "A" ? "Windows 98 Second Edition" : "Windows 98";
                                    break;
                                case 90:
                                    name = "Windows Me";
                                    break;
                            }
                        }

                        break;
                    }
                    case PlatformID.Win32NT:
                    {
                        Byte productType = versioninfo.wProductType;

                        name = major switch
                        {
                            3 => "Windows NT 3.51",
                            4 => productType switch
                            {
                                1 => "Windows NT 4.0",
                                3 => "Windows NT 4.0 Server",
                                _ => name
                            },
                            5 => minor switch
                            {
                                0 => "Windows 2000",
                                1 => "Windows XP",
                                2 => "Windows Server 2003",
                                _ => name
                            },
                            6 => minor switch
                            {
                                0 => productType switch
                                {
                                    1 => "Windows Vista",
                                    3 => "Windows Server 2008",
                                    _ => name
                                },
                                1 => productType switch
                                {
                                    1 => "Windows 7",
                                    3 => "Windows Server 2008 R2",
                                    _ => name
                                },
                                2 => productType switch
                                {
                                    1 => "Windows 8",
                                    3 => "Windows Server 2012",
                                    _ => name
                                },
                                3 => productType switch
                                {
                                    1 => "Windows 8.1",
                                    3 => "Windows Server 2012 R2",
                                    _ => name
                                },
                                _ => name
                            },
                            10 => minor switch
                            {
                                0 => productType switch
                                {
                                    1 => "Windows 10",
                                    3 => "Windows Server 2016",
                                    _ => name
                                },
                                _ => name
                            },
                            _ => name
                        };

                        break;
                    }
                }

                return name ??= String.Empty;
            }
        }

        private const Int32 PRODUCT_UNDEFINED = 0x00000000;
        private const Int32 PRODUCT_ULTIMATE = 0x00000001;
        private const Int32 PRODUCT_HOME_BASIC = 0x00000002;
        private const Int32 PRODUCT_HOME_PREMIUM = 0x00000003;
        private const Int32 PRODUCT_ENTERPRISE = 0x00000004;
        private const Int32 PRODUCT_HOME_BASIC_N = 0x00000005;
        private const Int32 PRODUCT_BUSINESS = 0x00000006;
        private const Int32 PRODUCT_STANDARD_SERVER = 0x00000007;
        private const Int32 PRODUCT_DATACENTER_SERVER = 0x00000008;
        private const Int32 PRODUCT_SMALLBUSINESS_SERVER = 0x00000009;
        private const Int32 PRODUCT_ENTERPRISE_SERVER = 0x0000000A;
        private const Int32 PRODUCT_STARTER = 0x0000000B;
        private const Int32 PRODUCT_DATACENTER_SERVER_CORE = 0x0000000C;
        private const Int32 PRODUCT_STANDARD_SERVER_CORE = 0x0000000D;
        private const Int32 PRODUCT_ENTERPRISE_SERVER_CORE = 0x0000000E;
        private const Int32 PRODUCT_ENTERPRISE_SERVER_IA64 = 0x0000000F;
        private const Int32 PRODUCT_BUSINESS_N = 0x00000010;
        private const Int32 PRODUCT_WEB_SERVER = 0x00000011;
        private const Int32 PRODUCT_CLUSTER_SERVER = 0x00000012;
        private const Int32 PRODUCT_HOME_SERVER = 0x00000013;
        private const Int32 PRODUCT_STORAGE_EXPRESS_SERVER = 0x00000014;
        private const Int32 PRODUCT_STORAGE_STANDARD_SERVER = 0x00000015;
        private const Int32 PRODUCT_STORAGE_WORKGROUP_SERVER = 0x00000016;
        private const Int32 PRODUCT_STORAGE_ENTERPRISE_SERVER = 0x00000017;
        private const Int32 PRODUCT_SERVER_FOR_SMALLBUSINESS = 0x00000018;
        private const Int32 PRODUCT_SMALLBUSINESS_SERVER_PREMIUM = 0x00000019;
        private const Int32 PRODUCT_HOME_PREMIUM_N = 0x0000001A;
        private const Int32 PRODUCT_ENTERPRISE_N = 0x0000001B;
        private const Int32 PRODUCT_ULTIMATE_N = 0x0000001C;
        private const Int32 PRODUCT_WEB_SERVER_CORE = 0x0000001D;
        private const Int32 PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = 0x0000001E;
        private const Int32 PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = 0x0000001F;
        private const Int32 PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = 0x00000020;
        private const Int32 PRODUCT_SERVER_FOUNDATION = 0x00000021;
        private const Int32 PRODUCT_HOME_PREMIUM_SERVER = 0x00000022;
        private const Int32 PRODUCT_SERVER_FOR_SMALLBUSINESS_V = 0x00000023;
        private const Int32 PRODUCT_STANDARD_SERVER_V = 0x00000024;
        private const Int32 PRODUCT_DATACENTER_SERVER_V = 0x00000025;
        private const Int32 PRODUCT_ENTERPRISE_SERVER_V = 0x00000026;
        private const Int32 PRODUCT_DATACENTER_SERVER_CORE_V = 0x00000027;
        private const Int32 PRODUCT_STANDARD_SERVER_CORE_V = 0x00000028;
        private const Int32 PRODUCT_ENTERPRISE_SERVER_CORE_V = 0x00000029;
        private const Int32 PRODUCT_HYPERV = 0x0000002A;
        private const Int32 PRODUCT_STORAGE_EXPRESS_SERVER_CORE = 0x0000002B;
        private const Int32 PRODUCT_STORAGE_STANDARD_SERVER_CORE = 0x0000002C;
        private const Int32 PRODUCT_STORAGE_WORKGROUP_SERVER_CORE = 0x0000002D;
        private const Int32 PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE = 0x0000002E;
        private const Int32 PRODUCT_STARTER_N = 0x0000002F;
        private const Int32 PRODUCT_PROFESSIONAL = 0x00000030;
        private const Int32 PRODUCT_PROFESSIONAL_N = 0x00000031;
        private const Int32 PRODUCT_SB_SOLUTION_SERVER = 0x00000032;
        private const Int32 PRODUCT_SERVER_FOR_SB_SOLUTIONS = 0x00000033;
        private const Int32 PRODUCT_STANDARD_SERVER_SOLUTIONS = 0x00000034;
        private const Int32 PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE = 0x00000035;
        private const Int32 PRODUCT_SB_SOLUTION_SERVER_EM = 0x00000036;
        private const Int32 PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM = 0x00000037;
        private const Int32 PRODUCT_SOLUTION_EMBEDDEDSERVER = 0x00000038;

        private const Int32 PRODUCT_SOLUTION_EMBEDDEDSERVER_CORE = 0x00000039;

        //private const int ???? = 0x0000003A;
        private const Int32 PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT = 0x0000003B;
        private const Int32 PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL = 0x0000003C;
        private const Int32 PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC = 0x0000003D;
        private const Int32 PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC = 0x0000003E;
        private const Int32 PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE = 0x0000003F;
        private const Int32 PRODUCT_CLUSTER_SERVER_V = 0x00000040;
        private const Int32 PRODUCT_EMBEDDED = 0x00000041;
        private const Int32 PRODUCT_STARTER_E = 0x00000042;
        private const Int32 PRODUCT_HOME_BASIC_E = 0x00000043;
        private const Int32 PRODUCT_HOME_PREMIUM_E = 0x00000044;
        private const Int32 PRODUCT_PROFESSIONAL_E = 0x00000045;
        private const Int32 PRODUCT_ENTERPRISE_E = 0x00000046;

        private const Int32 PRODUCT_ULTIMATE_E = 0x00000047;
        //private const int PRODUCT_UNLICENSED = 0xABCDABCD;


        private const Int32 VER_NT_WORKSTATION = 1;
        private const Int32 VER_NT_DOMAIN_CONTROLLER = 2;
        private const Int32 VER_NT_SERVER = 3;
        private const Int32 VER_SUITE_SMALLBUSINESS = 1;
        private const Int32 VER_SUITE_ENTERPRISE = 2;
        private const Int32 VER_SUITE_TERMINAL = 16;
        private const Int32 VER_SUITE_DATACENTER = 128;
        private const Int32 VER_SUITE_SINGLEUSERTS = 256;
        private const Int32 VER_SUITE_PERSONAL = 512;
        private const Int32 VER_SUITE_BLADE = 1024;
    }
}