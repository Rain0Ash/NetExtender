// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using NetExtender.Utilities.IO;

namespace NetExtender.Utilities.Windows.IO
{
    public static class DirectoryPermissionsUtilities
    {
        public static Boolean HasPermissions(String path, FileSystemRights access)
        {
            return HasPermissions(new DirectoryInfo(path), access);
        }
        
        public static Boolean IsHasPermissions(String path, FileSystemRights access)
        {
            return IsHasPermissions(new DirectoryInfo(path), access);
        }

        public static Boolean HasPermissions(this DirectoryInfo info, FileSystemRights access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.LatestExist()
                .GetAccessControl()
                .GetAccessRules(true, true, typeof(NTAccount))
                .OfType<FileSystemAccessRule>()
                .Any(rule => (rule.FileSystemRights & access) == access);
        }

        public static Boolean IsHasPermissions(this DirectoryInfo info, FileSystemRights access)
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