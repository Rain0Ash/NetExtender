// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using NetExtender.Types.Native.Windows;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.IO;

namespace NetExtender.Utilities.Windows
{
    public readonly struct DiskSpace
    {
        public String Disk { get; }
        public UInt64 TotalBytes { get; }
        public UInt64 TotalFreeBytes { get; }
        public UInt64 FreeBytes { get; }

        public DiskSpace(String disk, UInt64 total, UInt64 totalfree, UInt64 free)
        {
            if (String.IsNullOrEmpty(disk))
            {
                throw new ArgumentNullException(nameof(disk));
            }

            Disk = disk;
            TotalBytes = total;
            TotalFreeBytes = totalfree;
            FreeBytes = free;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    public class MonitorInfo
    {
        public Int32 Size = Marshal.SizeOf(typeof(MonitorInfo));
        public WinRectangle Area = new WinRectangle();
        public WinRectangle WorkingArea = new WinRectangle();
        public Int32 Flags = 0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public Char[] Device = new Char[32];
    }

    public enum DefaultMonitorType : UInt32
    {
        Null,
        Primary,
        Nearest
    }

    public static class WindowsDeviceUtilities
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, UInt32 dwFlags);

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean GetMonitorInfo(IntPtr handle, [In, Out] MonitorInfo info);

        public static MonitorInfo GetMonitorInfoFromWindow(IntPtr handle)
        {
            return GetMonitorInfoFromWindow(handle, DefaultMonitorType.Primary);
        }

        public static MonitorInfo GetMonitorInfoFromWindow(IntPtr handle, DefaultMonitorType type)
        {
            if (handle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(handle));
            }
            
            IntPtr monitor = MonitorFromWindow(handle, (UInt32) type);

            if (monitor == IntPtr.Zero)
            {
                WindowsInteropUtilities.ThrowLastWin32Exception();
            }
            
            MonitorInfo screen = new MonitorInfo();
            if (!GetMonitorInfo(monitor, screen))
            {
                WindowsInteropUtilities.ThrowLastWin32Exception();
            }

            return screen;
        }

        public static DriveInfo? GetDriveInfo(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return DriveInfo.GetDrives().FirstOrDefault(drive => drive.IsReady && drive.Name == name);
        }

        // Pinvoke for API function
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetDiskFreeSpaceEx(String lpDirectoryName, out UInt64 lpFreeBytesAvailable, out UInt64 lpTotalNumberOfBytes, out UInt64 lpTotalNumberOfFreeBytes);

        public static DiskSpace DriveFreeBytes(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            path = PathUtilities.AddEndSeparator(path);

            if (!GetDiskFreeSpaceEx(path, out UInt64 free, out UInt64 total, out UInt64 totalfree))
            {
                WindowsInteropUtilities.ThrowLastWin32Exception();
            }

            return new DiskSpace(path, total, totalfree, free);
        }
    }
}