using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.Utilities.IO
{
    public static class DriveInfoUtilities
    {
        public static IEnumerable<DriveInfo> Where(this IEnumerable<DriveInfo> source, DriveType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (DriveInfo info in source)
            {
                if (info.DriveType == type)
                {
                    yield return info;
                }
            }
        }

        public static IEnumerable<IDriveInfo> Where(this IEnumerable<IDriveInfo> source, DriveType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (IDriveInfo info in source)
            {
                if (info.DriveType == type)
                {
                    yield return info;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<DriveInfo> Ready(this IEnumerable<DriveInfo> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(static drive => drive.IsReady);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IDriveInfo> Ready(this IEnumerable<IDriveInfo> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(static drive => drive.IsReady);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<DriveInfo> Removable(this IEnumerable<DriveInfo> source)
        {
            return Where(source, DriveType.Removable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IDriveInfo> Removable(this IEnumerable<IDriveInfo> source)
        {
            return Where(source, DriveType.Removable);
        }
    }
}