// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using Microsoft.Win32.SafeHandles;
using NetExtender.Utils.IO;
using NetExtender.Windows.IO;

namespace NetExtender.Utils.Windows.IO
{
    public static class WindowsFileUtils
    {
        public static UInt64 GetFileSize(String path)
        {
            if (!PathUtils.IsExistAsFile(path))
            {
                throw new FileNotFoundException($"File '{path}' not found!");
            }

            using SafeFileHandle handle = WindowsPathUtils.Safe.CreateFile(path, NativeFileAccess.GenericRead, FileShare.Read, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            UInt64 result = WindowsPathUtils.Safe.GetFileSize(path, handle);

            return result;
        }
    }
}