// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetExtender.Utilities.IO
{
    public static class DriveUtilities
    {
        public static DriveInfo[] Drives
        {
            get
            {
                return DriveInfo.GetDrives();
            }
        }

        public static DriveInfo[] Ready
        {
            get
            {
                return Drives.Where(drive => drive.IsReady).ToArray();
            }
        }
        
        public static DriveInfo[] Removable
        {
            get
            {
                return Drives.Where(DriveType.Removable).ToArray();
            }
        }

        public static IEnumerable<DriveInfo> Where(this IEnumerable<DriveInfo> source, DriveType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(drive => drive.DriveType == type);
        }

        public static IEnumerable<DriveInfo> WhereReady(this IEnumerable<DriveInfo> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(drive => drive.IsReady);
        }
    }
}