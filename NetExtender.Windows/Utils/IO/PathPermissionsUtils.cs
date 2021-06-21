// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using NetExtender.Utils.IO;

namespace NetExtender.Utils.Windows.IO
{
    public static class PathPermissionsUtils
    {
        public static Boolean HasPermissions(this FileSystemInfo info, FileSystemRights access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info switch
            {
                DirectoryInfo directory => directory.HasPermissions(access),
                FileInfo file => file.HasPermissions(access),
                _ => throw new NotSupportedException()
            };
        }

        public static Boolean IsHasPermissions(this FileSystemInfo info, FileSystemRights access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info switch
            {
                DirectoryInfo directory => directory.IsHasPermissions(access),
                FileInfo file => file.IsHasPermissions(access),
                _ => throw new NotSupportedException()
            };
        }
    }
}