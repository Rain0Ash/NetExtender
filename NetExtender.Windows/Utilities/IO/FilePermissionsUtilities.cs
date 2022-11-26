// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;

namespace NetExtender.Utilities.Windows.IO
{
    public static class FilePermissionsUtilities
    {
        public static Boolean HasPermissions(String path, FileSystemRights access)
        {
            return HasPermissions(new FileInfo(path), access);
        }

        public static Boolean IsHasPermissions(String path, FileSystemRights access)
        {
            return IsHasPermissions(new FileInfo(path), access);
        }

        public static Boolean HasPermissions(this FileInfo info, FileSystemRights access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (info.Exists)
            {
                return info
                    .GetAccessControl()
                    .GetAccessRules(true, true, typeof(NTAccount))
                    .OfType<FileSystemAccessRule>()
                    .Any(rule => (rule.FileSystemRights & access) == access);
            }

            return info.Directory?.HasPermissions(access) ?? false;
        }

        public static Boolean IsHasPermissions(this FileInfo info, FileSystemRights access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            try
            {
                return HasPermissions(info, access);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}