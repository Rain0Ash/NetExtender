// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.Utilities.IO
{
    public static class DriveUtilities
    {
        private static IFileSystem NetExtender
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return IFileSystem.Default;
            }
        }

        public static DriveInfo[] Drives
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return DriveInfo.GetDrives();
            }
        }

        public static DriveInfo[] Ready
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Drives.Ready().ToArray();
            }
        }

        public static DriveInfo[] Removable
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Drives.Removable().ToArray();
            }
        }
    }
}