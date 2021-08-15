// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;

namespace NetExtender.Utilities.IO
{
    public static class FileSystemInfoUtilities
    {
        public static Boolean TryDelete(this FileSystemInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            try
            {
                info.Delete();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}