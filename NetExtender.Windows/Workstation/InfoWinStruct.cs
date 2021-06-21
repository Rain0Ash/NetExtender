// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;

namespace NetExtender.Workstation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "InconsistentNaming")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnassignedField.Global")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "NotAccessedField.Global")]
    public static partial class Hardware
    {
        [StructLayout(LayoutKind.Explicit)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "InconsistentNaming")]
        public struct _PROCESSOR_INFO_UNION
        {
            [FieldOffset(0)] internal UInt32 dwOemId;
            [FieldOffset(0)] internal UInt16 wProcessorArchitecture;
            [FieldOffset(2)] internal UInt16 wReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "InconsistentNaming")]
        public struct SYSTEM_INFO
        {
            internal _PROCESSOR_INFO_UNION uProcessorInfo;
            public UInt32 dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public UInt32 dwNumberOfProcessors;
            public UInt32 dwProcessorType;
            public UInt32 dwAllocationGranularity;
            public UInt16 dwProcessorLevel;
            public UInt16 dwProcessorRevision;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "InconsistentNaming")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnassignedField.Global")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
    public static partial class Software
    {
        [DllImport("Kernel32.dll")]
        internal static extern Boolean GetProductInfo(
            Int32 osMajorVersion,
            Int32 osMinorVersion,
            Int32 spMajorVersion,
            Int32 spMinorVersion,
            out Int32 edition);

        [DllImport("kernel32.dll")]
        private static extern Boolean GetVersionEx(ref OSVERSIONINFOEX osVersionInfo);

        [DllImport("user32")]
        private static extern Int32 GetSystemMetrics(Int32 nIndex);

        [StructLayout(LayoutKind.Sequential)]
        private struct OSVERSIONINFOEX
        {
            public Int32 dwOSVersionInfoSize;
            public Int32 dwMajorVersion;
            public Int32 dwMinorVersion;
            public Int32 dwBuildNumber;
            public Int32 dwPlatformId;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public String szCSDVersion;

            public Int16 wServicePackMajor;
            public Int16 wServicePackMinor;
            public Int16 wSuiteMask;
            public Byte wProductType;
            public Byte wReserved;
        }
    }
}