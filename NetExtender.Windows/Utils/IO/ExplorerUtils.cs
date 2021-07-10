// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Utils.IO;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.Windows.IO
{
    public static class ExplorerUtils
    {
        [DllImport("shell32.dll")]
        private static extern Int32 SHOpenFolderAndSelectItems(IntPtr pidlFolder, UInt32 cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, UInt32 dwFlags);

        [DllImport("shell32.dll")]
        private static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] String name, IntPtr bindingContext, [Out] out IntPtr pidl, UInt32 sfgaoIn, [Out] out UInt32 psfgaoOut);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IntPtr SHParseDisplayName(String path)
        {
            SHParseDisplayName(path, IntPtr.Zero, out IntPtr ptrfile, 0, out _);
            return ptrfile;
        }

        public static Boolean OpenExplorer(this DirectoryInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return OpenExplorer(info.FullName);
        }
        
        public static Boolean OpenExplorer(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (PathUtils.IsExistAsFile(path))
            {
                return OpenExplorerWithSelection(path);
            }

            String? fullpath = PathUtils.GetFullPath(path);

            if (fullpath is null)
            {
                return false;
            }
            
            path = fullpath;

            if (!PathUtils.IsExistAsFolder(path))
            {
                return false;
            }
            
            IntPtr directory = SHParseDisplayName(path);

            if (directory == IntPtr.Zero)
            {
                return false;
            }

            IntPtr[] array = Array.Empty<IntPtr>();
            Int32 hresult = SHOpenFolderAndSelectItems(directory, (UInt32) array.Length, array, 0);
            Marshal.FreeCoTaskMem(directory);

            return hresult == 0;
        }
        
        public static Boolean OpenExplorerWithSelection(this FileInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.Exists)
            {
                return false;
            }

            return info.Directory is not null && OpenExplorerWithSelection(info.Directory.FullName, info.Name);
        }
        
        public static Boolean OpenExplorerWithSelection(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            String? directory = Path.GetDirectoryName(path);
            return directory is not null && OpenExplorerWithSelection(directory, Path.GetFileName(path));
        }

        public static Boolean OpenExplorerWithSelection(this DirectoryInfo info, String? filename)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.Exists && OpenExplorerWithSelection(info.FullName, filename);
        }

        public static Boolean OpenExplorerWithSelection(this IEnumerable<FileInfo?> files)
        {
            return OpenExplorerWithSelection(files, StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean OpenExplorerWithSelection(this IEnumerable<FileInfo?> files, StringComparison comparison)
        {
            if (files is null)
            {
                throw new ArgumentNullException(nameof(files));
            }
            
            FileInfo[] values = files.Where(item => item?.Directory?.FullName is not null)
                .WhereSameBy(item => item!.Directory!.FullName, StringComparer.FromComparison(comparison))
                .DistinctBy(item => item!.FullName).ToArray()!;

            return values.Length > 0 && OpenExplorerWithSelection(values[0].Directory!.FullName, values.Select(item => item.Name), comparison);
        }
        
        public static Boolean OpenExplorerWithSelection(params FileInfo[] files)
        {
            if (files is null)
            {
                throw new ArgumentNullException(nameof(files));
            }

            return files.Length > 0 && OpenExplorerWithSelection((IEnumerable<FileInfo>) files);
        }

        public static Boolean OpenExplorerWithSelection(String directory, String? filename)
        {
            return OpenExplorerWithSelection(directory, filename, StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean OpenExplorerWithSelection(String directory, String? filename, StringComparison comparison)
        {
            return OpenExplorerWithSelection(directory, EnumerableUtils.GetEnumerableFrom(filename), comparison);
        }

        public static Boolean OpenExplorerWithSelection(this DirectoryInfo info, IEnumerable<String?>? files)
        {
            return OpenExplorerWithSelection(info, files, StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean OpenExplorerWithSelection(this DirectoryInfo info, IEnumerable<String?>? files, StringComparison comparison)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.Exists && OpenExplorerWithSelection(info.FullName, files, comparison);
        }

        public static Boolean OpenExplorerWithSelection(this DirectoryInfo info, params String[]? files)
        {
            return OpenExplorerWithSelection(info, StringComparison.OrdinalIgnoreCase, files);
        }

        public static Boolean OpenExplorerWithSelection(this DirectoryInfo info, StringComparison comparison, params String?[]? files)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.Exists)
            {
                return false;
            }

            return files?.Length > 0 ? OpenExplorerWithSelection(info, files, comparison) : OpenExplorer(info);
        }

        public static Boolean OpenExplorerWithSelection(String directory, params String?[]? files)
        {
            return OpenExplorerWithSelection(directory, StringComparison.OrdinalIgnoreCase, files);
        }

        public static Boolean OpenExplorerWithSelection(String directory, StringComparison comparison, params String?[]? files)
        {
            return OpenExplorerWithSelection(directory, files, comparison);
        }

        public static Boolean OpenExplorerWithSelection(String directory, IEnumerable<String?>? files)
        {
            return OpenExplorerWithSelection(directory, files, StringComparison.OrdinalIgnoreCase);
        }

        private static IEnumerable<String> OpenExplorerWithSelectionFilter(IEnumerable<String?> files, String directory, StringComparison comparison)
        {
            foreach (String? file in files)
            {
                if (String.IsNullOrEmpty(file))
                {
                    continue;
                }

                if (!PathUtils.IsFullPath(file))
                {
                    yield return file;
                    continue;
                }

                String? path = PathUtils.GetFullPath(file);

                if (path is null || !String.Equals(directory, PathUtils.GetDirectoryName(path), comparison) || !PathUtils.IsExistAsFile(path))
                {
                    continue;
                }
                
                yield return PathUtils.GetFileName(path);
            }
        }

        public static Boolean OpenExplorerWithSelection(String directory, IEnumerable<String?>? files, StringComparison comparison)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }
            
            String? fullpath = PathUtils.GetFullPath(directory);

            if (fullpath is null)
            {
                return false;
            }

            directory = fullpath;

            if (!PathUtils.IsExistAsFolder(directory))
            {
                return false;
            }

            if (files is null)
            {
                return OpenExplorer(directory);
            }

            String[] filename = OpenExplorerWithSelectionFilter(files, directory, comparison).Distinct().ToArray();
            if (filename.Length <= 0)
            {
                return OpenExplorer(directory);
            }

            IntPtr ptrdirectory = SHParseDisplayName(directory);

            if (ptrdirectory == IntPtr.Zero)
            {
                return false;
            }

            IntPtr[] ptrfiles = filename.Select(file => SHParseDisplayName(Path.Combine(directory, file))).Where(IntPtrUtils.IsNotNull).ToArray();

            Int32 hresult = SHOpenFolderAndSelectItems(ptrdirectory, (UInt32) ptrfiles.Length, ptrfiles, 0);
            
            Marshal.FreeCoTaskMem(ptrdirectory);
            ptrfiles.ForEach(Marshal.FreeCoTaskMem);
            
            return hresult == 0;
        }
    }
}