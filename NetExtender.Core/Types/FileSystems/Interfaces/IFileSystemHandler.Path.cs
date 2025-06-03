// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using NetExtender.Interfaces.Notify;

namespace NetExtender.FileSystems.Interfaces
{
    public interface IPathHandler : INotifyProperty, IDisposable
    {
        public Object SyncRoot { get; }
        public Boolean IsSynchronized { get; }
        public Guid Id { get; }
        public String? Name { get; }
        public DateTime CreationTime { get; }
        public DateTime CreationTimeUtc { get; }
        public Boolean IsReal { get; }
        public StringComparer Comparer { get; }
        public Boolean IsCaseSensitive { get; }

        /// <inheritdoc cref="System.IO.Path.PathSeparator" />
        public Char PathSeparator { get; }

        /// <inheritdoc cref="System.IO.Path.VolumeSeparatorChar" />
        public Char VolumeSeparatorChar { get; }

        /// <inheritdoc cref="System.IO.Path.DirectorySeparatorChar" />
        public Char DirectorySeparatorChar { get; }

        /// <inheritdoc cref="System.IO.Path.AltDirectorySeparatorChar" />
        public Char AltDirectorySeparatorChar { get; }

        /// <inheritdoc cref="System.IO.Path.InvalidPathChars" />
        [Obsolete($"{nameof(InvalidPathChars)} has been deprecated. Use {nameof(GetInvalidPathChars)} instead.")]
        public Char[] InvalidPathChars { get; }

        /// <inheritdoc cref="System.IO.Path.GetInvalidPathChars()" />
        public ImmutableArray<Char> InvalidPathCharacters { get; }

        [Obsolete($"{nameof(InvalidFileNameChars)} has been deprecated. Use {nameof(GetInvalidFileNameChars)} instead.")]
        public Char[] InvalidFileNameChars { get; }
        
        /// <inheritdoc cref="System.IO.Path.GetInvalidFileNameChars()" />
        public ImmutableArray<Char> InvalidFileNameCharacters { get; }

        /// <inheritdoc cref="System.IO.Path.GetInvalidPathChars()" />
        public Char[] GetInvalidPathChars();

        /// <inheritdoc cref="System.IO.Path.GetInvalidFileNameChars()" />
        public Char[] GetInvalidFileNameChars();

        public String GetVolumeName(String path);
        public ReadOnlySpan<Char> GetVolumeName(ReadOnlySpan<Char> path);
        public ReadOnlyMemory<Char> GetVolumeName(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.IsPathFullyQualified(System.String)" />
        public Boolean IsPathFullyQualified(String path);

        /// <inheritdoc cref="System.IO.Path.IsPathFullyQualified(System.ReadOnlySpan{System.Char})" />
        public Boolean IsPathFullyQualified(ReadOnlySpan<Char> path);

        /// <inheritdoc cref="System.IO.Path.IsPathFullyQualified(System.ReadOnlySpan{System.Char})" />
        public Boolean IsPathFullyQualified(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.GetFullPath(System.String)" />
        public String GetFullPath(String path);

        /// <inheritdoc cref="System.IO.Path.GetFullPath(System.String, System.String)" />
        public String GetFullPath(String path, String @base);

        /// <inheritdoc cref="System.IO.Path.GetRelativePath(System.String, System.String)" />
        public String GetRelativePath(String relative, String path);

        /// <inheritdoc cref="System.IO.Path.IsPathRooted(System.String)" />
        public Boolean IsPathRooted([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] String? path);

        /// <inheritdoc cref="System.IO.Path.IsPathRooted(System.ReadOnlySpan{System.Char})" />
        public Boolean IsPathRooted(ReadOnlySpan<Char> path);

        /// <inheritdoc cref="System.IO.Path.IsPathRooted(System.ReadOnlySpan{System.Char})" />
        public Boolean IsPathRooted(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.GetPathRoot(System.String?)" />
        public String? GetPathRoot(String? path);

        /// <inheritdoc cref="System.IO.Path.GetPathRoot(System.ReadOnlySpan{System.Char})" />
        public ReadOnlySpan<Char> GetPathRoot(ReadOnlySpan<Char> path);

        /// <inheritdoc cref="System.IO.Path.GetPathRoot(System.ReadOnlySpan{System.Char})" />
        public ReadOnlyMemory<Char> GetPathRoot(ReadOnlyMemory<Char> path);

        public Int32 GetUncRootLength(String path);
        public Int32 GetUncRootLength(ReadOnlySpan<Char> path);
        public Int32 GetUncRootLength(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.EndsInDirectorySeparator(System.String)" />
        public Boolean EndsInDirectorySeparator(String path);

        /// <inheritdoc cref="System.IO.Path.EndsInDirectorySeparator(System.ReadOnlySpan{System.Char})" />
        public Boolean EndsInDirectorySeparator(ReadOnlySpan<Char> path);

        /// <inheritdoc cref="System.IO.Path.EndsInDirectorySeparator(System.ReadOnlySpan{System.Char})" />
        public Boolean EndsInDirectorySeparator(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.TrimEndingDirectorySeparator(System.String)" />
        public String TrimEndingDirectorySeparator(String path);

        /// <inheritdoc cref="System.IO.Path.TrimEndingDirectorySeparator(System.ReadOnlySpan{System.Char})" />
        public ReadOnlySpan<Char> TrimEndingDirectorySeparator(ReadOnlySpan<Char> path);

        /// <inheritdoc cref="System.IO.Path.TrimEndingDirectorySeparator(System.ReadOnlySpan{System.Char})" />
        public ReadOnlyMemory<Char> TrimEndingDirectorySeparator(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.HasExtension(System.String)" />
        public Boolean HasExtension([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] String? path);

        /// <inheritdoc cref="System.IO.Path.HasExtension(System.ReadOnlySpan{System.Char})" />
        public Boolean HasExtension(ReadOnlySpan<Char> path);

        /// <inheritdoc cref="System.IO.Path.HasExtension(System.ReadOnlySpan{System.Char})" />
        public Boolean HasExtension(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.GetExtension(System.String?)" />
        [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("path")]
        public String? GetExtension(String? path);

        /// <inheritdoc cref="System.IO.Path.GetExtension(System.ReadOnlySpan{System.Char})" />
        public ReadOnlySpan<Char> GetExtension(ReadOnlySpan<Char> path);

        /// <inheritdoc cref="System.IO.Path.GetExtension(System.ReadOnlySpan{System.Char})" />
        public ReadOnlyMemory<Char> GetExtension(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.ChangeExtension(System.String?, System.String?)" />
        [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("path")]
        public String? ChangeExtension(String? path, String? extension);

        /// <inheritdoc cref="System.IO.Path.GetFileName(System.String?)" />
        [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("path")]
        public String? GetFileName(String? path);

        /// <inheritdoc cref="System.IO.Path.GetFileName(System.ReadOnlySpan{System.Char})" />
        public ReadOnlySpan<Char> GetFileName(ReadOnlySpan<Char> path);

        /// <inheritdoc cref="System.IO.Path.GetFileName(System.ReadOnlySpan{System.Char})" />
        public ReadOnlyMemory<Char> GetFileName(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.GetDirectoryName(System.String?)" />
        public String? GetDirectoryName(String? path);

        /// <inheritdoc cref="System.IO.Path.GetDirectoryName(System.ReadOnlySpan{System.Char})" />
        public ReadOnlySpan<Char> GetDirectoryName(ReadOnlySpan<Char> path);

        /// <inheritdoc cref="System.IO.Path.GetDirectoryName(System.ReadOnlySpan{System.Char})" />
        public ReadOnlyMemory<Char> GetDirectoryName(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.GetTempPath()" />
        public String GetTempPath();

        /// <inheritdoc cref="System.IO.Path.GetTempFileName()" />
        public String GetTempFileName();

        /// <inheritdoc cref="System.IO.Path.GetRandomFileName()" />
        public String GetRandomFileName();

        /// <inheritdoc cref="System.IO.Path.GetFileNameWithoutExtension(System.String?)" />
        [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("path")]
        public String? GetFileNameWithoutExtension(String? path);

        /// <inheritdoc cref="System.IO.Path.GetFileNameWithoutExtension(System.ReadOnlySpan{System.Char})" />
        public ReadOnlySpan<Char> GetFileNameWithoutExtension(ReadOnlySpan<Char> path);

        /// <inheritdoc cref="System.IO.Path.GetFileNameWithoutExtension(System.ReadOnlySpan{System.Char})" />
        public ReadOnlyMemory<Char> GetFileNameWithoutExtension(ReadOnlyMemory<Char> path);

        /// <inheritdoc cref="System.IO.Path.Combine(System.String, System.String)" />
        public String Combine(String path1, String path2);

        /// <inheritdoc cref="System.IO.Path.Combine(System.String, System.String, System.String)" />
        public String Combine(String path1, String path2, String path3);

        /// <inheritdoc cref="System.IO.Path.Combine(System.String, System.String, System.String, System.String)" />
        public String Combine(String path1, String path2, String path3, String path4);

        /// <inheritdoc cref="System.IO.Path.Combine(System.String[])" />
        public String Combine(params String[] paths);

        /// <inheritdoc cref="System.IO.Path.Join(System.String?, System.String?)" />
        public String Join(String? path1, String? path2);

        /// <inheritdoc cref="System.IO.Path.Join(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char})" />
        public String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2);

        /// <inheritdoc cref="System.IO.Path.Join(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char})" />
        public String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2);

        /// <inheritdoc cref="System.IO.Path.Join(System.String?, System.String?, System.String?)" />
        public String Join(String? path1, String? path2, String? path3);

        /// <inheritdoc cref="System.IO.Path.Join(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char})" />
        public String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3);

        /// <inheritdoc cref="System.IO.Path.Join(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char})" />
        public String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3);

        /// <inheritdoc cref="System.IO.Path.Join(System.String?, System.String?, System.String?, System.String?)" />
        public String Join(String? path1, String? path2, String? path3, String? path4);

        /// <inheritdoc cref="System.IO.Path.Join(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char})" />
        public String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, ReadOnlySpan<Char> path4);

        /// <inheritdoc cref="System.IO.Path.Join(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char})" />
        public String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, ReadOnlyMemory<Char> path4);

        /// <inheritdoc cref="System.IO.Path.Join(System.String?[])" />
        public String Join(params String?[] paths);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Int32? TryJoin(String path1, String path2, Span<Char> destination);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Boolean TryJoin(String path1, String path2, Span<Char> destination, out Int32 written);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Int32? TryJoin(String path1, String path2, Memory<Char> destination);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Boolean TryJoin(String path1, String path2, Memory<Char> destination, out Int32 written);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Int32? TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination, out Int32 written);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Int32? TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, Memory<Char> destination);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Boolean TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, Memory<Char> destination, out Int32 written);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Int32? TryJoin(String path1, String path2, String path3, Span<Char> destination);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Boolean TryJoin(String path1, String path2, String path3, Span<Char> destination, out Int32 written);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Int32? TryJoin(String path1, String path2, String path3, Memory<Char> destination);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Boolean TryJoin(String path1, String path2, String path3, Memory<Char> destination, out Int32 written);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Int32? TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination, out Int32 written);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Int32? TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, Memory<Char> destination);

        /// <inheritdoc cref="System.IO.Path.TryJoin(System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.ReadOnlySpan{System.Char}, System.Span{System.Char}, out System.Int32)" />
        public Boolean TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, Memory<Char> destination, out Int32 written);
    }
}