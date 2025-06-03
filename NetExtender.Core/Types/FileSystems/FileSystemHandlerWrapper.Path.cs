// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandlerWrapper<T>
    {
        public override Char PathSeparator
        {
            get
            {
                return FileSystem.Path.PathSeparator;
            }
        }

        public override Char VolumeSeparatorChar
        {
            get
            {
                return FileSystem.Path.VolumeSeparatorChar;
            }
        }

        public override Char DirectorySeparatorChar
        {
            get
            {
                return FileSystem.Path.DirectorySeparatorChar;
            }
        }

        public override Char AltDirectorySeparatorChar
        {
            get
            {
                return FileSystem.Path.AltDirectorySeparatorChar;
            }
        }

        [Obsolete($"{nameof(InvalidPathChars)} has been deprecated. Use {nameof(GetInvalidPathChars)} instead.")]
        public override Char[] InvalidPathChars
        {
            get
            {
                return FileSystem.Path.InvalidPathChars;
            }
        }

        public override ImmutableArray<Char> InvalidPathCharacters
        {
            get
            {
                return FileSystem.Path.InvalidPathCharacters;
            }
        }

        [Obsolete($"{nameof(InvalidFileNameChars)} has been deprecated. Use {nameof(GetInvalidFileNameChars)} instead.")]
        public override Char[] InvalidFileNameChars
        {
            get
            {
                return FileSystem.Path.InvalidFileNameChars;
            }
        }

        public override ImmutableArray<Char> InvalidFileNameCharacters
        {
            get
            {
                return FileSystem.Path.InvalidFileNameCharacters;
            }
        }

        public override Char[] GetInvalidPathChars()
        {
            return FileSystem.Path.GetInvalidPathChars();
        }

        public override Char[] GetInvalidFileNameChars()
        {
            return FileSystem.Path.GetInvalidFileNameChars();
        }

        public override String GetVolumeName(String path)
        {
            return FileSystem.Path.GetVolumeName(path);
        }

        public override ReadOnlySpan<Char> GetVolumeName(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.GetVolumeName(path);
        }

        public override ReadOnlyMemory<Char> GetVolumeName(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.GetVolumeName(path);
        }

        public override Boolean IsPathFullyQualified(String path)
        {
            return FileSystem.Path.IsPathFullyQualified(path);
        }

        public override Boolean IsPathFullyQualified(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.IsPathFullyQualified(path);
        }

        public override Boolean IsPathFullyQualified(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.IsPathFullyQualified(path);
        }

        public override String GetFullPath(String path)
        {
            return FileSystem.Path.GetFullPath(path);
        }

        public override String GetFullPath(String path, String @base)
        {
            return FileSystem.Path.GetFullPath(path, @base);
        }

        public override String GetRelativePath(String relative, String path)
        {
            return FileSystem.Path.GetRelativePath(relative, path);
        }

        public override Boolean IsPathRooted([NotNullWhen(true)] String? path)
        {
            return FileSystem.Path.IsPathRooted(path);
        }

        public override Boolean IsPathRooted(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.IsPathRooted(path);
        }

        public override Boolean IsPathRooted(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.IsPathRooted(path);
        }

        public override String? GetPathRoot(String? path)
        {
            return FileSystem.Path.GetPathRoot(path);
        }

        public override ReadOnlySpan<Char> GetPathRoot(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.GetPathRoot(path);
        }

        public override ReadOnlyMemory<Char> GetPathRoot(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.GetPathRoot(path);
        }

        public override Int32 GetUncRootLength(String path)
        {
            return FileSystem.Path.GetUncRootLength(path);
        }

        public override Int32 GetUncRootLength(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.GetUncRootLength(path);
        }

        public override Int32 GetUncRootLength(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.GetUncRootLength(path);
        }

        public override Boolean EndsInDirectorySeparator(String path)
        {
            return FileSystem.Path.EndsInDirectorySeparator(path);
        }

        public override Boolean EndsInDirectorySeparator(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.EndsInDirectorySeparator(path);
        }

        public override Boolean EndsInDirectorySeparator(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.EndsInDirectorySeparator(path);
        }

        public override String TrimEndingDirectorySeparator(String path)
        {
            return FileSystem.Path.TrimEndingDirectorySeparator(path);
        }

        public override ReadOnlySpan<Char> TrimEndingDirectorySeparator(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.TrimEndingDirectorySeparator(path);
        }

        public override ReadOnlyMemory<Char> TrimEndingDirectorySeparator(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.TrimEndingDirectorySeparator(path);
        }

        public override Boolean HasExtension([NotNullWhen(true)] String? path)
        {
            return FileSystem.Path.HasExtension(path);
        }

        public override Boolean HasExtension(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.HasExtension(path);
        }

        public override Boolean HasExtension(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.HasExtension(path);
        }

        [return: NotNullIfNotNull("path")]
        public override String? GetExtension(String? path)
        {
            return FileSystem.Path.GetExtension(path);
        }

        public override ReadOnlySpan<Char> GetExtension(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.GetExtension(path);
        }

        public override ReadOnlyMemory<Char> GetExtension(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.GetExtension(path);
        }

        [return: NotNullIfNotNull("path")]
        public override String? ChangeExtension(String? path, String? extension)
        {
            return FileSystem.Path.ChangeExtension(path, extension);
        }

        [return: NotNullIfNotNull("path")]
        public override String? GetFileName(String? path)
        {
            return FileSystem.Path.GetFileName(path);
        }

        public override ReadOnlySpan<Char> GetFileName(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.GetFileName(path);
        }

        public override ReadOnlyMemory<Char> GetFileName(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.GetFileName(path);
        }

        public override String? GetDirectoryName(String? path)
        {
            return FileSystem.Path.GetDirectoryName(path);
        }

        public override ReadOnlySpan<Char> GetDirectoryName(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.GetDirectoryName(path);
        }

        public override ReadOnlyMemory<Char> GetDirectoryName(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.GetDirectoryName(path);
        }

        public override String GetTempPath()
        {
            return FileSystem.Path.GetTempPath();
        }

        public override String GetTempFileName()
        {
            return FileSystem.Path.GetTempFileName();
        }

        public override String GetRandomFileName()
        {
            return FileSystem.Path.GetRandomFileName();
        }

        [return: NotNullIfNotNull("path")]
        public override String? GetFileNameWithoutExtension(String? path)
        {
            return FileSystem.Path.GetFileNameWithoutExtension(path);
        }

        public override ReadOnlySpan<Char> GetFileNameWithoutExtension(ReadOnlySpan<Char> path)
        {
            return FileSystem.Path.GetFileNameWithoutExtension(path);
        }

        public override ReadOnlyMemory<Char> GetFileNameWithoutExtension(ReadOnlyMemory<Char> path)
        {
            return FileSystem.Path.GetFileNameWithoutExtension(path);
        }

        public override String Combine(String path1, String path2)
        {
            return FileSystem.Path.Combine(path1, path2);
        }

        public override String Combine(String path1, String path2, String path3)
        {
            return FileSystem.Path.Combine(path1, path2, path3);
        }

        public override String Combine(String path1, String path2, String path3, String path4)
        {
            return FileSystem.Path.Combine(path1, path2, path3, path4);
        }

        public override String Combine(params String[] paths)
        {
            return FileSystem.Path.Combine(paths);
        }

        public override String Join(String? path1, String? path2)
        {
            return FileSystem.Path.Join(path1, path2);
        }

        public override String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2)
        {
            return FileSystem.Path.Join(path1, path2);
        }

        public override String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2)
        {
            return FileSystem.Path.Join(path1, path2);
        }

        public override String Join(String? path1, String? path2, String? path3)
        {
            return FileSystem.Path.Join(path1, path2, path3);
        }

        public override String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3)
        {
            return FileSystem.Path.Join(path1, path2, path3);
        }

        public override String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3)
        {
            return FileSystem.Path.Join(path1, path2, path3);
        }

        public override String Join(String? path1, String? path2, String? path3, String? path4)
        {
            return FileSystem.Path.Join(path1, path2, path3, path4);
        }

        public override String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, ReadOnlySpan<Char> path4)
        {
            return FileSystem.Path.Join(path1, path2, path3, path4);
        }

        public override String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, ReadOnlyMemory<Char> path4)
        {
            return FileSystem.Path.Join(path1, path2, path3, path4);
        }

        public override String Join(params String?[] paths)
        {
            return FileSystem.Path.Join(paths);
        }

        public override Int32? TryJoin(String path1, String path2, Span<Char> destination)
        {
            return FileSystem.Path.TryJoin(path1, path2, destination);
        }

        public override Boolean TryJoin(String path1, String path2, Span<Char> destination, out Int32 written)
        {
            return FileSystem.Path.TryJoin(path1, path2, destination, out written);
        }

        public override Int32? TryJoin(String path1, String path2, Memory<Char> destination)
        {
            return FileSystem.Path.TryJoin(path1, path2, destination);
        }

        public override Boolean TryJoin(String path1, String path2, Memory<Char> destination, out Int32 written)
        {
            return FileSystem.Path.TryJoin(path1, path2, destination, out written);
        }

        public override Int32? TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination)
        {
            return FileSystem.Path.TryJoin(path1, path2, destination);
        }

        public override Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination, out Int32 written)
        {
            return FileSystem.Path.TryJoin(path1, path2, destination, out written);
        }

        public override Int32? TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, Memory<Char> destination)
        {
            return FileSystem.Path.TryJoin(path1, path2, destination);
        }

        public override Boolean TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, Memory<Char> destination, out Int32 written)
        {
            return FileSystem.Path.TryJoin(path1, path2, destination, out written);
        }

        public override Int32? TryJoin(String path1, String path2, String path3, Span<Char> destination)
        {
            return FileSystem.Path.TryJoin(path1, path2, path3, destination);
        }

        public override Boolean TryJoin(String path1, String path2, String path3, Span<Char> destination, out Int32 written)
        {
            return FileSystem.Path.TryJoin(path1, path2, path3, destination, out written);
        }

        public override Int32? TryJoin(String path1, String path2, String path3, Memory<Char> destination)
        {
            return FileSystem.Path.TryJoin(path1, path2, path3, destination);
        }

        public override Boolean TryJoin(String path1, String path2, String path3, Memory<Char> destination, out Int32 written)
        {
            return FileSystem.Path.TryJoin(path1, path2, path3, destination, out written);
        }

        public override Int32? TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination)
        {
            return FileSystem.Path.TryJoin(path1, path2, path3, destination);
        }

        public override Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination, out Int32 written)
        {
            return FileSystem.Path.TryJoin(path1, path2, path3, destination, out written);
        }

        public override Int32? TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, Memory<Char> destination)
        {
            return FileSystem.Path.TryJoin(path1, path2, path3, destination);
        }

        public override Boolean TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, Memory<Char> destination, out Int32 written)
        {
            return FileSystem.Path.TryJoin(path1, path2, path3, destination, out written);
        }
    }
}