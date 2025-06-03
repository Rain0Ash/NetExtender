// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.FileSystems
{
    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive>
    {
        public abstract override Char PathSeparator { get; }
        public abstract override Char VolumeSeparatorChar { get; }
        public abstract override Char DirectorySeparatorChar { get; }
        public abstract override Char AltDirectorySeparatorChar { get; }

        [Obsolete($"{nameof(InvalidPathChars)} has been deprecated. Use {nameof(GetInvalidPathChars)} instead.")]
        public override Char[] InvalidPathChars
        {
            get
            {
                return GetInvalidPathChars();
            }
        }

        [Obsolete($"{nameof(InvalidFileNameChars)} has been deprecated. Use {nameof(GetInvalidFileNameChars)} instead.")]
        public override Char[] InvalidFileNameChars
        {
            get
            {
                return GetInvalidFileNameChars();
            }
        }

        public abstract override Char[] GetInvalidPathChars();
        public abstract override Char[] GetInvalidFileNameChars();
        public abstract override String GetVolumeName(String path);
        public abstract override ReadOnlySpan<Char> GetVolumeName(ReadOnlySpan<Char> path);
        public abstract override Boolean IsPathFullyQualified(String path);
        public abstract override Boolean IsPathFullyQualified(ReadOnlySpan<Char> path);
        public abstract override String GetFullPath(String path);
        public abstract override String GetFullPath(String path, String @base);
        public abstract override String GetRelativePath(String relative, String path);
        public abstract override Boolean IsPathRooted([NotNullWhen(true)] String? path);
        public abstract override Boolean IsPathRooted(ReadOnlySpan<Char> path);
        public abstract override String? GetPathRoot(String? path);
        public abstract override ReadOnlySpan<Char> GetPathRoot(ReadOnlySpan<Char> path);
        public abstract override Int32 GetUncRootLength(String path);
        public abstract override Int32 GetUncRootLength(ReadOnlySpan<Char> path);
        public abstract override Boolean EndsInDirectorySeparator(String path);
        public abstract override Boolean EndsInDirectorySeparator(ReadOnlySpan<Char> path);
        public abstract override String TrimEndingDirectorySeparator(String path);
        public abstract override ReadOnlySpan<Char> TrimEndingDirectorySeparator(ReadOnlySpan<Char> path);
        public abstract override Boolean HasExtension([NotNullWhen(true)] String? path);
        public abstract override Boolean HasExtension(ReadOnlySpan<Char> path);

        [return: NotNullIfNotNull("path")]
        public abstract override String? GetExtension(String? path);
        public abstract override ReadOnlySpan<Char> GetExtension(ReadOnlySpan<Char> path);

        [return: NotNullIfNotNull("path")]
        public abstract override String? ChangeExtension(String? path, String? extension);

        [return: NotNullIfNotNull("path")]
        public abstract override String? GetFileName(String? path);
        public abstract override ReadOnlySpan<Char> GetFileName(ReadOnlySpan<Char> path);
        public abstract override String? GetDirectoryName(String? path);
        public abstract override ReadOnlySpan<Char> GetDirectoryName(ReadOnlySpan<Char> path);
        public abstract override String GetTempPath();
        public abstract override String GetTempFileName();
        public abstract override String GetRandomFileName();

        [return: NotNullIfNotNull("path")]
        public abstract override String? GetFileNameWithoutExtension(String? path);
        public abstract override ReadOnlySpan<Char> GetFileNameWithoutExtension(ReadOnlySpan<Char> path);
        public abstract override String Combine(String path1, String path2);
        public abstract override String Combine(String path1, String path2, String path3);
        public abstract override String Combine(String path1, String path2, String path3, String path4);
        public abstract override String Combine(params String[] paths);
        public abstract override String Join(String? path1, String? path2);
        public abstract override String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2);
        public abstract override String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2);
        public abstract override String Join(String? path1, String? path2, String? path3);
        public abstract override String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3);
        public abstract override String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3);
        public abstract override String Join(String? path1, String? path2, String? path3, String? path4);
        public abstract override String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, ReadOnlySpan<Char> path4);
        public abstract override String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, ReadOnlyMemory<Char> path4);
        public abstract override String Join(params String?[] paths);
        public abstract override Boolean TryJoin(String path1, String path2, Span<Char> destination, out Int32 written);
        public abstract override Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination, out Int32 written);
        public abstract override Boolean TryJoin(String path1, String path2, String path3, Span<Char> destination, out Int32 written);
        public abstract override Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination, out Int32 written);
    }
}