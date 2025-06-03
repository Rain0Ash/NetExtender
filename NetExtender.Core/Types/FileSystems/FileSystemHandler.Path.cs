// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Utilities.Core;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandler
    {
        private static partial class Handler
        {
            private delegate ReadOnlySpan<Char> GetVolumeNameDelegate(ReadOnlySpan<Char> path);
            private delegate Int32 GetUncRootLengthDelegate(ReadOnlySpan<Char> path);
            
            private static GetVolumeNameDelegate? GetVolumeNameHandler { get; }
            private static GetUncRootLengthDelegate? GetUncRootLengthHandler { get; }

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
                return System.IO.Path.PathSeparator;
            }
        }

        public virtual Char VolumeSeparatorChar
        {
            get
            {
                return System.IO.Path.VolumeSeparatorChar;
            }
        }

        public virtual Char DirectorySeparatorChar
        {
            get
            {
                return System.IO.Path.DirectorySeparatorChar;
            }
        }

        public virtual Char AltDirectorySeparatorChar
        {
            get
            {
                return System.IO.Path.AltDirectorySeparatorChar;
            }
        }

        [Obsolete($"{nameof(InvalidPathChars)} has been deprecated. Use {nameof(GetInvalidPathChars)} instead.")]
        public virtual Char[] InvalidPathChars
        {
            get
            {
                return System.IO.Path.InvalidPathChars;
            }
        }

        private ImmutableArray<Char> _path;
        public virtual ImmutableArray<Char> InvalidPathCharacters
        {
            get
            {
                return _path.IsDefaultOrEmpty ? _path = GetInvalidPathChars().ToImmutableArray() : _path;
            }
        }

        [Obsolete($"{nameof(InvalidFileNameChars)} has been deprecated. Use {nameof(GetInvalidFileNameChars)} instead.")]
        public virtual Char[] InvalidFileNameChars
        {
            get
            {
                return System.IO.Path.GetInvalidFileNameChars();
            }
        }

        private ImmutableArray<Char> _filename;
        public virtual ImmutableArray<Char> InvalidFileNameCharacters
        {
            get
            {
                return _filename.IsDefaultOrEmpty ? _filename = GetInvalidFileNameChars().ToImmutableArray() : _filename;
            }
        }

        public virtual Char[] GetInvalidPathChars()
        {
            return System.IO.Path.GetInvalidPathChars();
        }

        public virtual Char[] GetInvalidFileNameChars()
        {
            return System.IO.Path.GetInvalidFileNameChars();
        }

        public virtual String GetVolumeName(String path)
        {
            return new String(Handler.GetVolumeName((ReadOnlySpan<Char>) path));
        }

        public virtual ReadOnlySpan<Char> GetVolumeName(ReadOnlySpan<Char> path)
        {
            return Handler.GetVolumeName(path);
        }

        public virtual ReadOnlyMemory<Char> GetVolumeName(ReadOnlyMemory<Char> path)
        {
            return new String(GetVolumeName(path.Span)).AsMemory();
        }

        public virtual Boolean IsPathFullyQualified(String path)
        {
            return System.IO.Path.IsPathFullyQualified(path);
        }

        public virtual Boolean IsPathFullyQualified(ReadOnlySpan<Char> path)
        {
            return System.IO.Path.IsPathFullyQualified(path);
        }

        public virtual Boolean IsPathFullyQualified(ReadOnlyMemory<Char> path)
        {
            return IsPathFullyQualified(path.Span);
        }

        public virtual String GetFullPath(String path)
        {
            return System.IO.Path.GetFullPath(path);
        }

        public virtual String GetFullPath(String path, String @base)
        {
            return System.IO.Path.GetFullPath(path, @base);
        }

        public virtual String GetRelativePath(String relative, String path)
        {
            return System.IO.Path.GetRelativePath(relative, path);
        }

        public virtual Boolean IsPathRooted([NotNullWhen(true)] String? path)
        {
            return System.IO.Path.IsPathRooted(path);
        }

        public virtual Boolean IsPathRooted(ReadOnlySpan<Char> path)
        {
            return System.IO.Path.IsPathRooted(path);
        }

        public virtual Boolean IsPathRooted(ReadOnlyMemory<Char> path)
        {
            return IsPathRooted(path.Span);
        }

        public virtual String? GetPathRoot(String? path)
        {
            return System.IO.Path.GetPathRoot(path);
        }

        public virtual ReadOnlySpan<Char> GetPathRoot(ReadOnlySpan<Char> path)
        {
            return System.IO.Path.GetPathRoot(path);
        }

        public virtual ReadOnlyMemory<Char> GetPathRoot(ReadOnlyMemory<Char> path)
        {
            return new String(GetPathRoot(path.Span)).AsMemory();
        }

        public virtual Int32 GetUncRootLength(String path)
        {
            return Handler.GetUncRootLength(path);
        }

        public virtual Int32 GetUncRootLength(ReadOnlySpan<Char> path)
        {
            return Handler.GetUncRootLength(path);
        }

        public virtual Int32 GetUncRootLength(ReadOnlyMemory<Char> path)
        {
            return GetUncRootLength(path.Span);
        }

        public virtual Boolean EndsInDirectorySeparator(String path)
        {
            return System.IO.Path.EndsInDirectorySeparator(path);
        }

        public virtual Boolean EndsInDirectorySeparator(ReadOnlySpan<Char> path)
        {
            return System.IO.Path.EndsInDirectorySeparator(path);
        }

        public virtual Boolean EndsInDirectorySeparator(ReadOnlyMemory<Char> path)
        {
            return EndsInDirectorySeparator(path.Span);
        }

        public virtual String TrimEndingDirectorySeparator(String path)
        {
            return System.IO.Path.TrimEndingDirectorySeparator(path);
        }

        public virtual ReadOnlySpan<Char> TrimEndingDirectorySeparator(ReadOnlySpan<Char> path)
        {
            return System.IO.Path.TrimEndingDirectorySeparator(path);
        }

        public virtual ReadOnlyMemory<Char> TrimEndingDirectorySeparator(ReadOnlyMemory<Char> path)
        {
            return new String(TrimEndingDirectorySeparator(path.Span)).AsMemory();
        }

        public virtual Boolean HasExtension([NotNullWhen(true)] String? path)
        {
            return System.IO.Path.HasExtension(path);
        }

        public virtual Boolean HasExtension(ReadOnlySpan<Char> path)
        {
            return System.IO.Path.HasExtension(path);
        }

        public virtual Boolean HasExtension(ReadOnlyMemory<Char> path)
        {
            return HasExtension(path.Span);
        }

        [return: NotNullIfNotNull("path")]
        public virtual String? GetExtension(String? path)
        {
            return System.IO.Path.GetExtension(path);
        }

        public virtual ReadOnlySpan<Char> GetExtension(ReadOnlySpan<Char> path)
        {
            return System.IO.Path.GetExtension(path);
        }

        public virtual ReadOnlyMemory<Char> GetExtension(ReadOnlyMemory<Char> path)
        {
            return new String(GetExtension(path.Span)).AsMemory();
        }

        [return: NotNullIfNotNull("path")]
        public virtual String? ChangeExtension(String? path, String? extension)
        {
            return System.IO.Path.ChangeExtension(path, extension);
        }

        [return: NotNullIfNotNull("path")]
        public virtual String? GetFileName(String? path)
        {
            return System.IO.Path.GetFileName(path);
        }

        public virtual ReadOnlySpan<Char> GetFileName(ReadOnlySpan<Char> path)
        {
            return System.IO.Path.GetFileName(path);
        }

        public virtual ReadOnlyMemory<Char> GetFileName(ReadOnlyMemory<Char> path)
        {
            return new String(GetFileName(path.Span)).AsMemory();
        }

        public virtual String? GetDirectoryName(String? path)
        {
            return System.IO.Path.GetDirectoryName(path);
        }

        public virtual ReadOnlySpan<Char> GetDirectoryName(ReadOnlySpan<Char> path)
        {
            return System.IO.Path.GetDirectoryName(path);
        }

        public virtual ReadOnlyMemory<Char> GetDirectoryName(ReadOnlyMemory<Char> path)
        {
            return new String(GetDirectoryName(path.Span)).AsMemory();
        }

        public virtual String GetTempPath()
        {
            return System.IO.Path.GetTempPath();
        }

        public virtual String GetTempFileName()
        {
            return System.IO.Path.GetTempFileName();
        }

        public virtual String GetRandomFileName()
        {
            return System.IO.Path.GetRandomFileName();
        }

        [return: NotNullIfNotNull("path")]
        public virtual String? GetFileNameWithoutExtension(String? path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }

        public virtual ReadOnlySpan<Char> GetFileNameWithoutExtension(ReadOnlySpan<Char> path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }

        public virtual ReadOnlyMemory<Char> GetFileNameWithoutExtension(ReadOnlyMemory<Char> path)
        {
            return new String(GetFileNameWithoutExtension(path.Span)).AsMemory();
        }

        public virtual String Combine(String path1, String path2)
        {
            return System.IO.Path.Combine(path1, path2);
        }

        public virtual String Combine(String path1, String path2, String path3)
        {
            return System.IO.Path.Combine(path1, path2, path3);
        }

        public virtual String Combine(String path1, String path2, String path3, String path4)
        {
            return System.IO.Path.Combine(path1, path2, path3, path4);
        }

        public virtual String Combine(params String[] paths)
        {
            return System.IO.Path.Combine(paths);
        }

        public virtual String Join(String? path1, String? path2)
        {
            return System.IO.Path.Join(path1, path2);
        }

        public virtual String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2)
        {
            return System.IO.Path.Join(path1, path2);
        }

        public virtual String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2)
        {
            return Join(path1.Span, path2.Span);
        }

        public virtual String Join(String? path1, String? path2, String? path3)
        {
            return System.IO.Path.Join(path1, path2, path3);
        }

        public virtual String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3)
        {
            return System.IO.Path.Join(path1, path2, path3);
        }

        public virtual String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3)
        {
            return Join(path1.Span, path2.Span, path3.Span);
        }

        public virtual String Join(String? path1, String? path2, String? path3, String? path4)
        {
            return System.IO.Path.Join(path1, path2, path3, path4);
        }

        public virtual String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, ReadOnlySpan<Char> path4)
        {
            return System.IO.Path.Join(path1, path2, path3, path4);
        }

        public virtual String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, ReadOnlyMemory<Char> path4)
        {
            return Join(path1.Span, path2.Span, path3.Span, path4.Span);
        }

        public virtual String Join(params String?[] paths)
        {
            return System.IO.Path.Join(paths);
        }

        public virtual Int32? TryJoin(String path1, String path2, Span<Char> destination)
        {
            return System.IO.Path.TryJoin(path1, path2, destination, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(String path1, String path2, Span<Char> destination, out Int32 written)
        {
            return System.IO.Path.TryJoin(path1, path2, destination, out written);
        }

        public virtual Int32? TryJoin(String path1, String path2, Memory<Char> destination)
        {
            return TryJoin(path1, path2, destination.Span, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(String path1, String path2, Memory<Char> destination, out Int32 written)
        {
            return TryJoin(path1, path2, destination.Span, out written);
        }

        public virtual Int32? TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination)
        {
            return TryJoin(path1, path2, destination, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination, out Int32 written)
        {
            return System.IO.Path.TryJoin(path1, path2, destination, out written);
        }

        public virtual Int32? TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, Memory<Char> destination)
        {
            return TryJoin(path1.Span, path2.Span, destination.Span, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, Memory<Char> destination, out Int32 written)
        {
            return TryJoin(path1.Span, path2.Span, destination.Span, out written);
        }

        public virtual Int32? TryJoin(String path1, String path2, String path3, Span<Char> destination)
        {
            return TryJoin(path1, path2, path3, destination, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(String path1, String path2, String path3, Span<Char> destination, out Int32 written)
        {
            return System.IO.Path.TryJoin(path1, path2, path3, destination, out written);
        }

        public virtual Int32? TryJoin(String path1, String path2, String path3, Memory<Char> destination)
        {
            return TryJoin(path1, path2, path3, destination.Span, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(String path1, String path2, String path3, Memory<Char> destination, out Int32 written)
        {
            return TryJoin(path1, path2, path3, destination.Span, out written);
        }

        public virtual Int32? TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination)
        {
            return TryJoin(path1, path2, path3, destination, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination, out Int32 written)
        {
            return System.IO.Path.TryJoin(path1, path2, path3, destination, out written);
        }

        public virtual Int32? TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, Memory<Char> destination)
        {
            return TryJoin(path1.Span, path2.Span, path3.Span, destination.Span, out Int32 written) ? written : null;
        }

        public virtual Boolean TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, Memory<Char> destination, out Int32 written)
        {
            return TryJoin(path1.Span, path2.Span, path3.Span, destination.Span, out written);
        }
    }
}