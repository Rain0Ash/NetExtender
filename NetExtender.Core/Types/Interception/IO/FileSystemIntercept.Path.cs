using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NetExtender.Types.Memory;

namespace NetExtender.Types.Interception
{
    public partial class FileSystemIntercept<TInfo>
    {
        public override Char PathSeparator
        {
            get
            {
                return Interceptor.InterceptGet<Char>(this);
            }
        }

        public override Char VolumeSeparatorChar
        {
            get
            {
                return Interceptor.InterceptGet<Char>(this);
            }
        }

        public override Char DirectorySeparatorChar
        {
            get
            {
                return Interceptor.InterceptGet<Char>(this);
            }
        }

        public override Char AltDirectorySeparatorChar
        {
            get
            {
                return Interceptor.InterceptGet<Char>(this);
            }
        }

#pragma warning disable CA1041
        [Obsolete]
        public override Char[] InvalidPathChars
        {
            get
            {
                return Interceptor.InterceptGet<Char[]>(this);
            }
        }
#pragma warning restore CA1041
        
        public override Char[] GetInvalidPathChars()
        {
            return Interceptor.Intercept(this, default, Path.GetInvalidPathChars);
        }

        public override Char[] GetInvalidFileNameChars()
        {
            return Interceptor.Intercept(this, default, Path.GetInvalidFileNameChars);
        }

        public override String GetVolumeName(String path)
        {
            return Interceptor.Intercept(this, default, base.GetVolumeName, path);
        }

        public override ReadOnlySpan<Char> GetVolumeName(ReadOnlySpan<Char> path)
        {
            return GetVolumeName(new String(path));
        }

        public override ReadOnlyMemory<Char> GetVolumeName(ReadOnlyMemory<Char> path)
        {
            return GetVolumeName(new String(path.Span)).AsMemory();
        }

        public override Boolean IsPathFullyQualified(String path)
        {
            return Interceptor.Intercept(this, default, Path.IsPathFullyQualified, path);
        }

        public override Boolean IsPathFullyQualified(ReadOnlySpan<Char> path)
        {
            return IsPathFullyQualified(new String(path));
        }

        public override Boolean IsPathFullyQualified(ReadOnlyMemory<Char> path)
        {
            return IsPathFullyQualified(new String(path.Span));
        }

        public override String GetFullPath(String path)
        {
            return Interceptor.Intercept(this, default, Path.GetFullPath, path);
        }

        public override String GetFullPath(String path, String @base)
        {
            return Interceptor.Intercept(this, default, Path.GetFullPath, path, @base);
        }

        public override String GetRelativePath(String relative, String path)
        {
            return Interceptor.Intercept(this, default, Path.GetRelativePath, relative, path);
        }

        public override Boolean IsPathRooted([NotNullWhen(true)] String? path)
        {
            return Interceptor.Intercept(this, default, Path.IsPathRooted, path);
        }

        public override Boolean IsPathRooted(ReadOnlySpan<Char> path)
        {
            return IsPathRooted(new String(path));
        }

        public override Boolean IsPathRooted(ReadOnlyMemory<Char> path)
        {
            return IsPathRooted(new String(path.Span));
        }

        public override String? GetPathRoot(String? path)
        {
            return Interceptor.Intercept(this, default, Path.GetPathRoot, path);
        }

        public override ReadOnlySpan<Char> GetPathRoot(ReadOnlySpan<Char> path)
        {
            return GetPathRoot(new String(path));
        }

        public override ReadOnlyMemory<Char> GetPathRoot(ReadOnlyMemory<Char> path)
        {
            return GetPathRoot(new String(path.Span)).AsMemory();
        }

        public override Int32 GetUncRootLength(String path)
        {
            return Interceptor.Intercept(this, default, base.GetUncRootLength, path);
        }

        public override Int32 GetUncRootLength(ReadOnlySpan<Char> path)
        {
            return GetUncRootLength(new String(path));
        }

        public override Int32 GetUncRootLength(ReadOnlyMemory<Char> path)
        {
            return GetUncRootLength(new String(path.Span));
        }

        public override Boolean EndsInDirectorySeparator(String path)
        {
            return Interceptor.Intercept(this, default, Path.EndsInDirectorySeparator, path);
        }

        public override Boolean EndsInDirectorySeparator(ReadOnlySpan<Char> path)
        {
            return EndsInDirectorySeparator(new String(path));
        }

        public override Boolean EndsInDirectorySeparator(ReadOnlyMemory<Char> path)
        {
            return EndsInDirectorySeparator(new String(path.Span));
        }

        public override String TrimEndingDirectorySeparator(String path)
        {
            return Interceptor.Intercept(this, default, Path.TrimEndingDirectorySeparator, path);
        }

        public override ReadOnlySpan<Char> TrimEndingDirectorySeparator(ReadOnlySpan<Char> path)
        {
            return TrimEndingDirectorySeparator(new String(path));
        }

        public override ReadOnlyMemory<Char> TrimEndingDirectorySeparator(ReadOnlyMemory<Char> path)
        {
            return TrimEndingDirectorySeparator(new String(path.Span)).AsMemory();
        }

        public override Boolean HasExtension([NotNullWhen(true)] String? path)
        {
            return Interceptor.Intercept(this, default, Path.HasExtension, path);
        }

        public override Boolean HasExtension(ReadOnlySpan<Char> path)
        {
            return HasExtension(new String(path));
        }

        public override Boolean HasExtension(ReadOnlyMemory<Char> path)
        {
            return HasExtension(new String(path.Span));
        }

        [return: NotNullIfNotNull("path")]
        public override String? GetExtension(String? path)
        {
            return Interceptor.Intercept(this, default, Path.GetExtension, path);
        }

        public override ReadOnlySpan<Char> GetExtension(ReadOnlySpan<Char> path)
        {
            return GetExtension(new String(path));
        }

        public override ReadOnlyMemory<Char> GetExtension(ReadOnlyMemory<Char> path)
        {
            return GetExtension(new String(path.Span)).AsMemory();
        }

        [return: NotNullIfNotNull("path")]
        public override String? ChangeExtension(String? path, String? extension)
        {
            return Interceptor.Intercept(this, default, Path.ChangeExtension, path, extension);
        }

        [return: NotNullIfNotNull("path")]
        public override String? GetFileName(String? path)
        {
            return Interceptor.Intercept(this, default, Path.GetFileName, path);
        }

        public override ReadOnlySpan<Char> GetFileName(ReadOnlySpan<Char> path)
        {
            return GetFileName(new String(path));
        }

        public override ReadOnlyMemory<Char> GetFileName(ReadOnlyMemory<Char> path)
        {
            return GetFileName(new String(path.Span)).AsMemory();
        }

        public override String? GetDirectoryName(String? path)
        {
            return Interceptor.Intercept(this, default, Path.GetDirectoryName, path);
        }

        public override ReadOnlySpan<Char> GetDirectoryName(ReadOnlySpan<Char> path)
        {
            return GetDirectoryName(new String(path));
        }

        public override ReadOnlyMemory<Char> GetDirectoryName(ReadOnlyMemory<Char> path)
        {
            return GetDirectoryName(new String(path.Span)).AsMemory();
        }

        public override String GetTempPath()
        {
            return Interceptor.Intercept(this, default, Path.GetTempPath);
        }

        public override String GetTempFileName()
        {
            return Interceptor.Intercept(this, default, Path.GetTempFileName);
        }

        public override String GetRandomFileName()
        {
            return Interceptor.Intercept(this, default, Path.GetRandomFileName);
        }

        [return: NotNullIfNotNull("path")]
        public override String? GetFileNameWithoutExtension(String? path)
        {
            return Interceptor.Intercept(this, default, Path.GetFileNameWithoutExtension, path);
        }

        public override ReadOnlySpan<Char> GetFileNameWithoutExtension(ReadOnlySpan<Char> path)
        {
            return GetFileNameWithoutExtension(new String(path));
        }

        public override ReadOnlyMemory<Char> GetFileNameWithoutExtension(ReadOnlyMemory<Char> path)
        {
            return GetFileNameWithoutExtension(new String(path.Span)).AsMemory();
        }

        public override String Combine(String path1, String path2)
        {
            return Interceptor.Intercept(this, default, Path.Combine, path1, path2);
        }

        public override String Combine(String path1, String path2, String path3)
        {
            return Interceptor.Intercept(this, default, Path.Combine, path1, path2, path3);
        }

        public override String Combine(String path1, String path2, String path3, String path4)
        {
            return Interceptor.Intercept(this, default, Path.Combine, path1, path2, path3, path4);
        }

        public override String Combine(params String[] paths)
        {
            return Interceptor.Intercept(this, default, Path.Combine, paths);
        }

        public override String Join(String? path1, String? path2)
        {
            return Interceptor.Intercept(this, default, Path.Join, path1, path2);
        }

        public override String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2)
        {
            return Join(new String(path1), new String(path2));
        }

        public override String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2)
        {
            return Join(new String(path1.Span), new String(path2.Span));
        }

        public override String Join(String? path1, String? path2, String? path3)
        {
            return Interceptor.Intercept(this, default, Path.Join, path1, path2, path3);
        }

        public override String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3)
        {
            return Join(new String(path1), new String(path2), new String(path3));
        }

        public override String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3)
        {
            return Join(new String(path1.Span), new String(path2.Span), new String(path3.Span));
        }

        public override String Join(String? path1, String? path2, String? path3, String? path4)
        {
            return Interceptor.Intercept(this, default, Path.Join, path1, path2, path3, path4);
        }

        public override String Join(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, ReadOnlySpan<Char> path4)
        {
            return Join(new String(path1), new String(path2), new String(path3), new String(path4));
        }

        public override String Join(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, ReadOnlyMemory<Char> path4)
        {
            return Join(new String(path1.Span), new String(path2.Span), new String(path3.Span), new String(path4.Span));
        }

        public override String Join(params String?[] paths)
        {
            return Interceptor.Intercept(this, default, Path.Join, paths);
        }

        public override unsafe Int32? TryJoin(String path1, String path2, Span<Char> destination)
        {
            fixed (Char* _ = destination)
            {
                using UnsafeMemory<Char> result = new UnsafeMemory<Char>(destination);
                return TryJoin(path1, path2, result);
            }
        }

        public override Boolean TryJoin(String path1, String path2, Span<Char> destination, out Int32 written)
        {
            if (TryJoin(path1, path2, destination) is not { } result)
            {
                written = -1;
                return false;
            }

            written = result;
            return true;
        }

        public override Int32? TryJoin(String path1, String path2, Memory<Char> destination)
        {
            return Interceptor.Intercept(this, default, base.TryJoin, path1, path2, destination);
        }

        public override Boolean TryJoin(String path1, String path2, Memory<Char> destination, out Int32 written)
        {
            if (TryJoin(path1, path2, destination) is not { } result)
            {
                written = -1;
                return false;
            }

            written = result;
            return true;
        }

        [SuppressMessage("ReSharper", "UnusedVariable")]
        public override unsafe Int32? TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination)
        {
            fixed (Char* _1 = path1, _2 = path2, _ = destination)
            {
                using UnsafeMemory<Char> first = new UnsafeMemory<Char>(path1);
                using UnsafeMemory<Char> second = new UnsafeMemory<Char>(path2);
                using UnsafeMemory<Char> result = new UnsafeMemory<Char>(destination);
                return TryJoin(first, second, result);
            }
        }

        public override Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, Span<Char> destination, out Int32 written)
        {
            if (TryJoin(path1, path2, destination) is not { } result)
            {
                written = -1;
                return false;
            }

            written = result;
            return true;
        }

        public override Int32? TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, Memory<Char> destination)
        {
            return Interceptor.Intercept(this, default, base.TryJoin, path1, path2, destination);
        }

        public override Boolean TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, Memory<Char> destination, out Int32 written)
        {
            if (TryJoin(path1, path2, destination) is not { } result)
            {
                written = -1;
                return false;
            }

            written = result;
            return true;
        }

        public override unsafe Int32? TryJoin(String path1, String path2, String path3, Span<Char> destination)
        {
            fixed (Char* _ = destination)
            {
                using UnsafeMemory<Char> result = new UnsafeMemory<Char>(destination);
                return TryJoin(path1, path2, path3, result);
            }
        }

        public override Boolean TryJoin(String path1, String path2, String path3, Span<Char> destination, out Int32 written)
        {
            if (TryJoin(path1, path2, path3, destination) is not { } result)
            {
                written = -1;
                return false;
            }

            written = result;
            return true;
        }

        public override Int32? TryJoin(String path1, String path2, String path3, Memory<Char> destination)
        {
            return Interceptor.Intercept(this, default, base.TryJoin, path1, path2, path3, destination);
        }

        public override Boolean TryJoin(String path1, String path2, String path3, Memory<Char> destination, out Int32 written)
        {
            if (TryJoin(path1, path2, path3, destination) is not { } result)
            {
                written = -1;
                return false;
            }

            written = result;
            return true;
        }

        [SuppressMessage("ReSharper", "UnusedVariable")]
        public override unsafe Int32? TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination)
        {
            fixed (Char* _1 = path1, _2 = path2, _3 = path3, _ = destination)
            {
                using UnsafeMemory<Char> first = new UnsafeMemory<Char>(path1);
                using UnsafeMemory<Char> second = new UnsafeMemory<Char>(path2);
                using UnsafeMemory<Char> third = new UnsafeMemory<Char>(path3);
                using UnsafeMemory<Char> result = new UnsafeMemory<Char>(destination);
                return TryJoin(first, second, third, result);
            }
        }

        public override Boolean TryJoin(ReadOnlySpan<Char> path1, ReadOnlySpan<Char> path2, ReadOnlySpan<Char> path3, Span<Char> destination, out Int32 written)
        {
            if (TryJoin(path1, path2, path3, destination) is not { } result)
            {
                written = -1;
                return false;
            }

            written = result;
            return true;
        }

        public override Int32? TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, Memory<Char> destination)
        {
            return Interceptor.Intercept(this, default, base.TryJoin, path1, path2, path3, destination);
        }

        public override Boolean TryJoin(ReadOnlyMemory<Char> path1, ReadOnlyMemory<Char> path2, ReadOnlyMemory<Char> path3, Memory<Char> destination, out Int32 written)
        {
            if (TryJoin(path1, path2, path3, destination) is not { } result)
            {
                written = -1;
                return false;
            }

            written = result;
            return true;
        }
    }
}