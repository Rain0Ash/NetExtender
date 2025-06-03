using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.IO
{
    public static class FileSystemUtilities
    {
        [ReflectionNaming]
        private static class FromSearchOption
        {
            private delegate EnumerationOptions Delegate(SearchOption option);
            private static Delegate Execute { get; }
            
            static FromSearchOption()
            {
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                Delegate? @delegate = typeof(EnumerationOptions).GetMethod(nameof(FromSearchOption), binding, new[] { typeof(SearchOption) })?.CreateDelegate<Delegate>();
                Execute = @delegate ?? throw new MissingMethodException(nameof(EnumerationOptions), nameof(FromSearchOption));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static EnumerationOptions Convert(SearchOption option)
            {
                return Execute(option);
            }
        }
        
        public static EnumerationOptions Options(this SearchOption option)
        {
            return FromSearchOption.Convert(option);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DriveInfo? GetDrive(this FileSystemInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            switch (info)
            {
                case FileInfo file:
                    return file.GetDrive();
                case DirectoryInfo directory:
                    return directory.GetDrive();
                default:
                {
                    try
                    {
                        String? root = Path.GetPathRoot(info.FullName);
                        return !String.IsNullOrEmpty(root) ? new DriveInfo(root) : null;
                    }
                    catch (ArgumentException)
                    {
                        return null;
                    }
                }
            }
        }
    }
}