// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.FileSystems.Interfaces;

#pragma warning disable CA1041

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        [SuppressMessage("ReSharper", "NotAccessedField.Global")]
        public static class Path
        {
            public static readonly Char PathSeparator;
            public static readonly Char VolumeSeparatorChar;
            public static readonly Char DirectorySeparatorChar;
            public static readonly Char AltDirectorySeparatorChar;

            [Obsolete]
            public static readonly Char[] InvalidPathChars;

            private static IInterceptPathHandler Interceptor { get; }

#pragma warning disable CS0612
            static Path()
            {
                Interceptor = new InterceptHarmonyFileSystem(typeof(Path));
                PathSeparator = Interceptor.PathSeparator;
                VolumeSeparatorChar = Interceptor.VolumeSeparatorChar;
                DirectorySeparatorChar = Interceptor.DirectorySeparatorChar;
                AltDirectorySeparatorChar = Interceptor.AltDirectorySeparatorChar;
                InvalidPathChars = Interceptor.InvalidPathChars;
            }
#pragma warning restore CS0612

            /// <inheritdoc cref="System.IO.Path.GetInvalidPathChars()" />
            public static Char[] GetInvalidPathChars()
            {
                return Interceptor.GetInvalidPathChars();
            }

            /// <inheritdoc cref="System.IO.Path.GetInvalidFileNameChars()" />
            public static Char[] GetInvalidFileNameChars()
            {
                return Interceptor.GetInvalidFileNameChars();
            }

            /// <inheritdoc cref="System.IO.Path.IsPathFullyQualified(System.String)" />
            public static Boolean IsPathFullyQualified(String path)
            {
                return Interceptor.IsPathFullyQualified(path);
            }

            /// <inheritdoc cref="System.IO.Path.IsPathFullyQualified(System.ReadOnlySpan{System.Char})" />
            public static Boolean IsPathFullyQualified(ReadOnlySpan<Char> path)
            {
                return Interceptor.IsPathFullyQualified(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetFullPath(System.String)" />
            public static String GetFullPath(String path)
            {
                return Interceptor.GetFullPath(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetFullPath(System.String, System.String)" />
            public static String GetFullPath(String path, String basePath)
            {
                return Interceptor.GetFullPath(path, basePath);
            }

            /// <inheritdoc cref="System.IO.Path.GetRelativePath(System.String, System.String)" />
            public static String GetRelativePath(String relativeTo, String path)
            {
                return Interceptor.GetRelativePath(relativeTo, path);
            }

            /// <inheritdoc cref="System.IO.Path.IsPathRooted(System.String)" />
            public static Boolean IsPathRooted([NotNullWhen(true)] String? path)
            {
                return Interceptor.IsPathRooted(path);
            }

            /// <inheritdoc cref="System.IO.Path.IsPathRooted(System.ReadOnlySpan{System.Char})" />
            public static Boolean IsPathRooted(ReadOnlySpan<Char> path)
            {
                return Interceptor.IsPathRooted(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetPathRoot(System.String?)" />
            public static String? GetPathRoot(String? path)
            {
                return Interceptor.GetPathRoot(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetPathRoot(System.ReadOnlySpan{System.Char})" />
            public static ReadOnlySpan<Char> GetPathRoot(ReadOnlySpan<Char> path)
            {
                return Interceptor.GetPathRoot(path);
            }

            /// <inheritdoc cref="System.IO.Path.EndsInDirectorySeparator(System.ReadOnlySpan{System.Char})" />
            public static Boolean EndsInDirectorySeparator(ReadOnlySpan<Char> path)
            {
                return Interceptor.EndsInDirectorySeparator(path);
            }

            /// <inheritdoc cref="System.IO.Path.EndsInDirectorySeparator(System.String)" />
            public static Boolean EndsInDirectorySeparator(String path)
            {
                return Interceptor.EndsInDirectorySeparator(path);
            }

            /// <inheritdoc cref="System.IO.Path.TrimEndingDirectorySeparator(System.String)" />
            public static String TrimEndingDirectorySeparator(String path)
            {
                return Interceptor.TrimEndingDirectorySeparator(path);
            }

            /// <inheritdoc cref="System.IO.Path.TrimEndingDirectorySeparator(System.ReadOnlySpan{System.Char})" />
            public static ReadOnlySpan<Char> TrimEndingDirectorySeparator(ReadOnlySpan<Char> path)
            {
                return Interceptor.TrimEndingDirectorySeparator(path);
            }

            /// <inheritdoc cref="System.IO.Path.HasExtension(System.String)" />
            public static Boolean HasExtension([NotNullWhen(true)] String? path)
            {
                return Interceptor.HasExtension(path);
            }

            /// <inheritdoc cref="System.IO.Path.HasExtension(System.ReadOnlySpan{System.Char})" />
            public static Boolean HasExtension(ReadOnlySpan<Char> path)
            {
                return Interceptor.HasExtension(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetExtension(System.String?)" />
            [return: NotNullIfNotNull("path")]
            public static String? GetExtension(String? path)
            {
                return Interceptor.GetExtension(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetExtension(System.ReadOnlySpan{System.Char})" />
            public static ReadOnlySpan<Char> GetExtension(ReadOnlySpan<Char> path)
            {
                return Interceptor.GetExtension(path);
            }

            /// <inheritdoc cref="System.IO.Path.ChangeExtension(System.String?, System.String?)" />
            [return: NotNullIfNotNull("path")]
            public static String? ChangeExtension(String? path, String? extension)
            {
                return Interceptor.ChangeExtension(path, extension);
            }

            /// <inheritdoc cref="System.IO.Path.GetFileName(System.String?)" />
            [return: NotNullIfNotNull("path")]
            public static String? GetFileName(String? path)
            {
                return Interceptor.GetFileName(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetFileName(System.ReadOnlySpan{System.Char})" />
            public static ReadOnlySpan<Char> GetFileName(ReadOnlySpan<Char> path)
            {
                return Interceptor.GetFileName(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetDirectoryName(System.String?)" />
            public static String? GetDirectoryName(String? path)
            {
                return Interceptor.GetDirectoryName(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetDirectoryName(System.ReadOnlySpan{System.Char})" />
            public static ReadOnlySpan<Char> GetDirectoryName(ReadOnlySpan<Char> path)
            {
                return Interceptor.GetDirectoryName(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetTempPath()" />
            public static String GetTempPath()
            {
                return Interceptor.GetTempPath();
            }

            /// <inheritdoc cref="System.IO.Path.GetTempFileName()" />
            public static String GetTempFileName()
            {
                return Interceptor.GetTempFileName();
            }

            /// <inheritdoc cref="System.IO.Path.GetRandomFileName()" />
            public static String GetRandomFileName()
            {
                return Interceptor.GetRandomFileName();
            }

            /// <inheritdoc cref="System.IO.Path.GetFileNameWithoutExtension(System.String?)" />
            [return: NotNullIfNotNull("path")]
            public static String? GetFileNameWithoutExtension(String? path)
            {
                return Interceptor.GetFileNameWithoutExtension(path);
            }

            /// <inheritdoc cref="System.IO.Path.GetFileNameWithoutExtension(System.ReadOnlySpan{System.Char})" />
            public static ReadOnlySpan<Char> GetFileNameWithoutExtension(ReadOnlySpan<Char> path)
            {
                return Interceptor.GetFileNameWithoutExtension(path);
            }

            /// <inheritdoc cref="System.IO.Path.Combine(System.String, System.String)" />
            public static String Combine(String path1, String path2)
            {
                return Interceptor.Combine(path1, path2);
            }

            /// <inheritdoc cref="System.IO.Path.Combine(System.String, System.String, System.String)" />
            public static String Combine(String path1, String path2, String path3)
            {
                return Interceptor.Combine(path1, path2, path3);
            }

            /// <inheritdoc cref="System.IO.Path.Combine(System.String, System.String, System.String, System.String)" />
            public static String Combine(String path1, String path2, String path3, String path4)
            {
                return Interceptor.Combine(path1, path2, path3, path4);
            }

            /// <inheritdoc cref="System.IO.Path.Combine(System.String[])" />
            public static String Combine(params String[] paths)
            {
                return Interceptor.Combine(paths);
            }

            /// <inheritdoc cref="System.IO.Path.Join(System.String?, System.String?)" />
            public static String Join(String? path1, String? path2)
            {
                return Interceptor.Join(path1, path2);
            }

            /// <inheritdoc cref="System.IO.Path.Join(System.String?, System.String?, System.String?)" />
            public static String Join(String? path1, String? path2, String? path3)
            {
                return Interceptor.Join(path1, path2, path3);
            }

            /// <inheritdoc cref="System.IO.Path.Join(System.String?, System.String?, System.String?, System.String?)" />
            public static String Join(String? path1, String? path2, String? path3, String? path4)
            {
                return Interceptor.Join(path1, path2, path3, path4);
            }

            /// <inheritdoc cref="System.IO.Path.Join(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char})" />
            public static String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2)
            {
                return Interceptor.Join(path1, path2);
            }

            /// <inheritdoc cref="System.IO.Path.Join(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char})" />
            public static String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3)
            {
                return Interceptor.Join(path1, path2, path3);
            }

            /// <inheritdoc cref="System.IO.Path.Join(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char})" />
            public static String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, ReadOnlySpan<Char> path4)
            {
                return Interceptor.Join(path1, path2, path3, path4);
            }

            /// <inheritdoc cref="System.IO.Path.Join(System.String?[])" />
            public static String Join(params String?[] paths)
            {
                return Interceptor.Join(paths);
            }

            /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
            public static Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination, out Int32 charsWritten)
            {
                return Interceptor.TryJoin(path1, path2, destination, out charsWritten);
            }

            /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
            public static Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination, out Int32 charsWritten)
            {
                return Interceptor.TryJoin(path1, path2, path3, destination, out charsWritten);
            }
        }
    }
}