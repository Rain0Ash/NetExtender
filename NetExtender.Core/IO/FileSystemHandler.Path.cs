using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.IO
{
    public partial class FileSystemHandler
    {
        protected static class PathHandler
        {
            private delegate ReadOnlySpan<Char> GetVolumeNameDelegate(ReadOnlySpan<Char> path);
            private delegate Int32 GetUncRootLengthDelegate(ReadOnlySpan<Char> path);
            
            private static GetVolumeNameDelegate? GetVolumeNameHandler { get; }
            private static GetUncRootLengthDelegate? GetUncRootLengthHandler { get; }

            static PathHandler()
            {
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                GetVolumeNameHandler = typeof(Path).GetMethod(nameof(GetVolumeName), binding, new[] { typeof(ReadOnlySpan<Char>) })?.CreateDelegate<GetVolumeNameDelegate>();
                GetUncRootLengthHandler = typeof(Path).GetMethod(nameof(GetUncRootLength), binding, new[] { typeof(ReadOnlySpan<Char>) })?.CreateDelegate<GetUncRootLengthDelegate>();
            }
            
            [ReflectionSignature]
            public static ReadOnlySpan<Char> GetVolumeName(ReadOnlySpan<Char> path)
            {
                return GetVolumeNameHandler is not null ? GetVolumeNameHandler.Invoke(path) : throw new NotSupportedException();
            }
            
            [ReflectionSignature]
            public static Int32 GetUncRootLength(ReadOnlySpan<Char> path)
            {
                return GetUncRootLengthHandler?.Invoke(path) ?? throw new NotSupportedException();
            }
        }

        public virtual Char PathSeparator
        {
            get
            {
                return Path.PathSeparator;
            }
        }

        public virtual Char VolumeSeparatorChar
        {
            get
            {
                return Path.VolumeSeparatorChar;
            }
        }

        public virtual Char DirectorySeparatorChar
        {
            get
            {
                return Path.DirectorySeparatorChar;
            }
        }

        public virtual Char AltDirectorySeparatorChar
        {
            get
            {
                return Path.AltDirectorySeparatorChar;
            }
        }

#pragma warning disable CA1041
        [Obsolete]
        public virtual Char[] InvalidPathChars
        {
            get
            {
                return Path.InvalidPathChars;
            }
        }
#pragma warning restore CA1041

        public virtual Char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }

        public virtual Char[] GetInvalidFileNameChars()
        {
            return Path.GetInvalidFileNameChars();
        }

        public virtual String GetVolumeName(String path)
        {
            return new String(PathHandler.GetVolumeName((ReadOnlySpan<Char>) path));
        }

        public virtual ReadOnlySpan<Char> GetVolumeName(ReadOnlySpan<Char> path)
        {
            return PathHandler.GetVolumeName(path);
        }

        public virtual ReadOnlyMemory<Char> GetVolumeName(ReadOnlyMemory<Char> path)
        {
            return new String(PathHandler.GetVolumeName(path.Span)).AsMemory();
        }

        public virtual Boolean IsPathFullyQualified(String path)
        {
            return Path.IsPathFullyQualified(path);
        }

        public virtual Boolean IsPathFullyQualified(ReadOnlySpan<Char> path)
        {
            return Path.IsPathFullyQualified(path);
        }

        public virtual Boolean IsPathFullyQualified(ReadOnlyMemory<Char> path)
        {
            return Path.IsPathFullyQualified(path.Span);
        }

        public virtual String GetFullPath(String path)
        {
            return Path.GetFullPath(path);
        }

        public virtual String GetFullPath(String path, String @base)
        {
            return Path.GetFullPath(path, @base);
        }

        public virtual String GetRelativePath(String relative, String path)
        {
            return Path.GetRelativePath(relative, path);
        }

        public virtual Boolean IsPathRooted([NotNullWhen(true)] String? path)
        {
            return Path.IsPathRooted(path);
        }

        public virtual Boolean IsPathRooted(ReadOnlySpan<Char> path)
        {
            return Path.IsPathRooted(path);
        }

        public virtual Boolean IsPathRooted(ReadOnlyMemory<Char> path)
        {
            return Path.IsPathRooted(path.Span);
        }

        public virtual String? GetPathRoot(String? path)
        {
            return Path.GetPathRoot(path);
        }

        public virtual ReadOnlySpan<Char> GetPathRoot(ReadOnlySpan<Char> path)
        {
            return Path.GetPathRoot(path);
        }

        public virtual ReadOnlyMemory<Char> GetPathRoot(ReadOnlyMemory<Char> path)
        {
            return Path.GetPathRoot(new String(GetPathRoot(path.Span))).AsMemory();
        }

        public virtual Int32 GetUncRootLength(String path)
        {
            return PathHandler.GetUncRootLength(path);
        }

        public virtual Int32 GetUncRootLength(ReadOnlySpan<Char> path)
        {
            return PathHandler.GetUncRootLength(path);
        }

        public virtual Int32 GetUncRootLength(ReadOnlyMemory<Char> path)
        {
            return PathHandler.GetUncRootLength(path.Span);
        }

        public virtual Boolean EndsInDirectorySeparator(String path)
        {
            return Path.EndsInDirectorySeparator(path);
        }

        public virtual Boolean EndsInDirectorySeparator(ReadOnlySpan<Char> path)
        {
            return Path.EndsInDirectorySeparator(path);
        }

        public virtual Boolean EndsInDirectorySeparator(ReadOnlyMemory<Char> path)
        {
            return Path.EndsInDirectorySeparator(path.Span);
        }

        public virtual String TrimEndingDirectorySeparator(String path)
        {
            return Path.TrimEndingDirectorySeparator(path);
        }

        public virtual ReadOnlySpan<Char> TrimEndingDirectorySeparator(ReadOnlySpan<Char> path)
        {
            return Path.TrimEndingDirectorySeparator(path);
        }

        public virtual ReadOnlyMemory<Char> TrimEndingDirectorySeparator(ReadOnlyMemory<Char> path)
        {
            return Path.TrimEndingDirectorySeparator(new String(TrimEndingDirectorySeparator(path.Span))).AsMemory();
        }

        public virtual Boolean HasExtension([NotNullWhen(true)] String? path)
        {
            return Path.HasExtension(path);
        }

        public virtual Boolean HasExtension(ReadOnlySpan<Char> path)
        {
            return Path.HasExtension(path);
        }

        public virtual Boolean HasExtension(ReadOnlyMemory<Char> path)
        {
            return Path.HasExtension(path.Span);
        }

        [return: NotNullIfNotNull("path")]
        public virtual String? GetExtension(String? path)
        {
            return Path.GetExtension(path);
        }

        public virtual ReadOnlySpan<Char> GetExtension(ReadOnlySpan<Char> path)
        {
            return Path.GetExtension(path);
        }

        public virtual ReadOnlyMemory<Char> GetExtension(ReadOnlyMemory<Char> path)
        {
            return Path.GetExtension(new String(GetExtension(path.Span))).AsMemory();
        }

        [return: NotNullIfNotNull("path")]
        public virtual String? ChangeExtension(String? path, String? extension)
        {
            return Path.ChangeExtension(path, extension);
        }

        [return: NotNullIfNotNull("path")]
        public virtual String? GetFileName(String? path)
        {
            return Path.GetFileName(path);
        }

        public virtual ReadOnlySpan<Char> GetFileName(ReadOnlySpan<Char> path)
        {
            return Path.GetFileName(path);
        }

        public virtual ReadOnlyMemory<Char> GetFileName(ReadOnlyMemory<Char> path)
        {
            return Path.GetFileName(new String(GetFileName(path.Span))).AsMemory();
        }

        public virtual String? GetDirectoryName(String? path)
        {
            return Path.GetDirectoryName(path);
        }

        public virtual ReadOnlySpan<Char> GetDirectoryName(ReadOnlySpan<Char> path)
        {
            return Path.GetDirectoryName(path);
        }

        public virtual ReadOnlyMemory<Char> GetDirectoryName(ReadOnlyMemory<Char> path)
        {
            return Path.GetDirectoryName(new String(GetDirectoryName(path.Span))).AsMemory();
        }

        public virtual String GetTempPath()
        {
            return Path.GetTempPath();
        }

        public virtual String GetTempFileName()
        {
            return Path.GetTempFileName();
        }

        public virtual String GetRandomFileName()
        {
            return Path.GetRandomFileName();
        }

        [return: NotNullIfNotNull("path")]
        public virtual String? GetFileNameWithoutExtension(String? path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public virtual ReadOnlySpan<Char> GetFileNameWithoutExtension(ReadOnlySpan<Char> path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public virtual ReadOnlyMemory<Char> GetFileNameWithoutExtension(ReadOnlyMemory<Char> path)
        {
            return Path.GetFileNameWithoutExtension(new String(GetFileNameWithoutExtension(path.Span))).AsMemory();
        }

        public virtual String Combine(String path1, String path2)
        {
            return Path.Combine(path1, path2);
        }

        public virtual String Combine(String path1, String path2, String path3)
        {
            return Path.Combine(path1, path2, path3);
        }

        public virtual String Combine(String path1, String path2, String path3, String path4)
        {
            return Path.Combine(path1, path2, path3, path4);
        }

        public virtual String Combine(params String[] paths)
        {
            return Path.Combine(paths);
        }

        public virtual String Join(String? path1, String? path2)
        {
            return Path.Join(path1, path2);
        }

        public virtual String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2)
        {
            return Path.Join(path1, path2);
        }

        public virtual String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2)
        {
            return Path.Join(path1.Span, path2.Span);
        }

        public virtual String Join(String? path1, String? path2, String? path3)
        {
            return Path.Join(path1, path2, path3);
        }

        public virtual String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3)
        {
            return Path.Join(path1, path2, path3);
        }

        public virtual String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3)
        {
            return Path.Join(path1.Span, path2.Span, path3.Span);
        }

        public virtual String Join(String? path1, String? path2, String? path3, String? path4)
        {
            return Path.Join(path1, path2, path3, path4);
        }

        public virtual String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, ReadOnlySpan<Char> path4)
        {
            return Path.Join(path1, path2, path3, path4);
        }

        public virtual String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, ReadOnlyMemory<Char> path4)
        {
            return Path.Join(path1.Span, path2.Span, path3.Span, path4.Span);
        }

        public virtual String Join(params String?[] paths)
        {
            return Path.Join(paths);
        }

        public virtual Int32? TryJoin(String path1, String path2, Span<Char> destination)
        {
            return Path.TryJoin(path1, path2, destination, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(String path1, String path2, Span<Char> destination, out Int32 written)
        {
            return Path.TryJoin(path1, path2, destination, out written);
        }

        public virtual Int32? TryJoin(String path1, String path2, Memory<Char> destination)
        {
            return Path.TryJoin(path1, path2, destination.Span, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(String path1, String path2, Memory<Char> destination, out Int32 written)
        {
            return Path.TryJoin(path1, path2, destination.Span, out written);
        }

        public virtual Int32? TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination)
        {
            return Path.TryJoin(path1, path2, destination, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination, out Int32 written)
        {
            return Path.TryJoin(path1, path2, destination, out written);
        }

        public virtual Int32? TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, Memory<Char> destination)
        {
            return Path.TryJoin(path1.Span, path2.Span, destination.Span, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, Memory<Char> destination, out Int32 written)
        {
            return Path.TryJoin(path1.Span, path2.Span, destination.Span, out written);
        }

        public virtual Int32? TryJoin(String path1, String path2, String path3, Span<Char> destination)
        {
            return Path.TryJoin(path1, path2, path3, destination, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(String path1, String path2, String path3, Span<Char> destination, out Int32 written)
        {
            return Path.TryJoin(path1, path2, path3, destination, out written);
        }

        public virtual Int32? TryJoin(String path1, String path2, String path3, Memory<Char> destination)
        {
            return Path.TryJoin(path1, path2, path3, destination.Span, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(String path1, String path2, String path3, Memory<Char> destination, out Int32 written)
        {
            return Path.TryJoin(path1, path2, path3, destination.Span, out written);
        }

        public virtual Int32? TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination)
        {
            return Path.TryJoin(path1, path2, path3, destination, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination, out Int32 written)
        {
            return Path.TryJoin(path1, path2, path3, destination, out written);
        }

        public virtual Int32? TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, Memory<Char> destination)
        {
            return Path.TryJoin(path1.Span, path2.Span, path3.Span, destination.Span, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, Memory<Char> destination, out Int32 written)
        {
            return Path.TryJoin(path1.Span, path2.Span, path3.Span, destination.Span, out written);
        }
    }
}