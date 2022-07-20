// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace NetExtender.Workstation
{
    public static partial class Hardware
    {
        [StructLayout(LayoutKind.Explicit)]
        private readonly struct ProcessorInfo
        {
            [field: FieldOffset(0)]
            public UInt32 OemId { get; init; }
            
            [field: FieldOffset(0)]
            public UInt16 Architecture { get; init; }
            
            [field: FieldOffset(2)]
            public UInt16 Reserved { get; init; }
        }

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct SystemInfo
        {
            public ProcessorInfo ProcessorInfo { get; init; }
            public UInt32 PageSize { get; init; }
            public IntPtr MinimumApplicationAddress { get; init; }
            public IntPtr MaximumApplicationAddress { get; init; }
            public IntPtr ActiveProcessorMask { get; init; }
            public UInt32 NumberOfProcessors { get; init; }
            public UInt32 ProcessorType { get; init; }
            public UInt32 AllocationGranularity { get; init; }
            public UInt16 ProcessorLevel { get; init; }
            public UInt16 ProcessorRevision { get; init; }
        }
    }

    public static partial class Software
    {
        [DllImport("Kernel32.dll")]
        private static extern Boolean GetProductInfo(Int32 major, Int32 minor, Int32 servicemajor, Int32 serviceminor, out Int32 edition);

        [DllImport("kernel32.dll")]
        private static extern Boolean GetVersionEx(ref WindowsSystemVersionInfo info);

        [DllImport("user32.dll")]
        private static extern Int32 GetSystemMetrics(Int32 index);

        [StructLayout(LayoutKind.Sequential)]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private readonly struct WindowsSystemVersionInfo
        {
            public Int32 SystemVersionInfoSize { get; init; }
            public Int32 MajorVersion { get; init; }
            public Int32 MinorVersion { get; init; }
            public Int32 BuildNumber { get; init; }
            public Int32 PlatformId { get; init; }

            [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public String Version { get; init; }
            public Int16 ServicePackMajor { get; init; }
            public Int16 ServicePackMinor { get; init; }
            public Int16 SuiteMask { get; init; }
            public Byte ProductType { get; init; }
            public Byte Reserved { get; init; }
        }
    }
}