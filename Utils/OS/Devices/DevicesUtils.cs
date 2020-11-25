// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace NetExtender.Utils.OS.Devices
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
    
    public static class DevicesUtils
    {
        public static DriveInfo GetDriveInfo(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            return DriveInfo.GetDrives().FirstOrDefault(drive => drive.IsReady && drive.Name == name);
        }
        
        // Pinvoke for API function
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetDiskFreeSpaceEx(String lpDirectoryName, out UInt64 lpFreeBytesAvailable, out UInt64 lpTotalNumberOfBytes, out UInt64 lpTotalNumberOfFreeBytes);

        public static DiskSpace DriveFreeBytes(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!path.EndsWith("\\"))
            {
                path += '\\';
            }

            if (!GetDiskFreeSpaceEx(path, out UInt64 free, out UInt64 total, out UInt64 totalfree))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return new DiskSpace(path, total, totalfree, free);
        }
    }
}