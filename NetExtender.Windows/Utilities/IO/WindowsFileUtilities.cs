// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using NetExtender.Utilities.IO;
using NetExtender.Windows.IO;

namespace NetExtender.Utilities.Windows.IO
{
    public static class WindowsFileUtilities
    {
        public static UInt64 GetFileSize(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!PathUtilities.IsExistAsFile(path))
            {
                throw new FileNotFoundException(null, path);
            }

            using SafeFileHandle handle = WindowsPathUtilities.Safe.CreateFile(path, NativeFileAccess.GenericRead, FileShare.Read, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            UInt64 result = WindowsPathUtilities.Safe.GetFileSize(path, handle);

            return result;
        }
    }
}