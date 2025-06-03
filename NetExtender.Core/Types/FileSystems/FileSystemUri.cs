using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems
{
    public readonly struct FileSystemUri : IEquatableStruct<FileSystemUri>, IReadOnlyList<Char>, IEnumerable<FileSystemUri.Segment>, IEquatable<FileSystemUri.Enumerator>
    {
        public static implicit operator FileSystemUri(String? value)
        {
            return !String.IsNullOrEmpty(value) ? new FileSystemUri(value) : default;
        }
        
        public static implicit operator FileSystemUri(Uri? value)
        {
            return value is not null ? new FileSystemUri(value) : default;
        }
        
        public static implicit operator FileSystemUri(FileSystemInfo? value)
        {
            return value is not null ? new FileSystemUri(value) : default;
        }

        public static implicit operator ReadOnlySpan<Char>(FileSystemUri value)
        {
            return value.Uri;
        }

        public static implicit operator ReadOnlyMemory<Char>(FileSystemUri value)
        {
            return value.Uri.AsMemory();
        }

        public static Boolean operator ==(FileSystemUri first, FileSystemUri second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(FileSystemUri first, FileSystemUri second)
        {
            return !(first == second);
        }
        
        private IFileSystem? FileSystem { get; }
        public String Uri { get; }

        public Int32 Count
        {
            get
            {
                return Uri.Length;
            }
        }

        /// <inheritdoc cref="IPathHandler.PathSeparator" />
        public Char PathSeparator
        {
            get
            {
                return FileSystem?.Path.PathSeparator ?? Path.PathSeparator;
            }
        }

        /// <inheritdoc cref="IPathHandler.VolumeSeparatorChar" />
        public Char VolumeSeparatorChar
        {
            get
            {
                return FileSystem?.Path.VolumeSeparatorChar ?? Path.VolumeSeparatorChar;
            }
        }

        /// <inheritdoc cref="IPathHandler.DirectorySeparatorChar" />
        public Char DirectorySeparatorChar
        {
            get
            {
                return FileSystem?.Path.DirectorySeparatorChar ?? Path.DirectorySeparatorChar;
            }
        }

        /// <inheritdoc cref="IPathHandler.AltDirectorySeparatorChar" />
        public Char AltDirectorySeparatorChar
        {
            get
            {
                return FileSystem?.Path.AltDirectorySeparatorChar ?? Path.AltDirectorySeparatorChar;
            }
        }

        /// <inheritdoc cref="IPathHandler.InvalidPathCharacters" />
        public ImmutableArray<Char> InvalidPathCharacters
        {
            get
            {
                return FileSystem?.Path.InvalidPathCharacters ?? Path.GetInvalidPathChars().ToImmutableArray();
            }
        }

        /// <inheritdoc cref="IPathHandler.InvalidFileNameCharacters" />
        public ImmutableArray<Char> InvalidFileNameCharacters
        {
            get
            {
                return FileSystem?.Path.InvalidFileNameCharacters ?? Path.GetInvalidFileNameChars().ToImmutableArray();
            }
        }

        private readonly Boolean? _sensitive = true;
        public Boolean IsCaseSensitive
        {
            get
            {
                return FileSystem?.IsCaseSensitive ?? _sensitive ?? true;
            }
            init
            {
                _sensitive = value;
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return String.IsNullOrEmpty(Uri);
            }
        }

        public FileSystemUri(String uri)
            : this(null, uri)
        {
        }

        public FileSystemUri(IFileSystem? system, String uri)
        {
            Uri = !String.IsNullOrEmpty(uri) ? uri : throw new ArgumentNullOrEmptyStringException(uri, nameof(uri));
            FileSystem = system;
        }

        public FileSystemUri(Uri uri)
            : this(null, uri)
        {
        }

        public FileSystemUri(IFileSystem? system, Uri uri)
        {
            Uri = uri switch
            {
                null => throw new ArgumentNullException(nameof(uri)),
                { IsFile: false } => throw new ArgumentException("Argument must be a file URI.", nameof(uri)),
                { IsAbsoluteUri: false } => throw new ArgumentException("Argument must be a absolute URI.", nameof(uri)),
                _ => uri.AbsolutePath
            };
            
            FileSystem = system;
        }

        public FileSystemUri(FileSystemInfo info)
            : this(null, info)
        {
        }

        public FileSystemUri(IFileSystem? system, FileSystemInfo info)
        {
            Uri = info is not null ? info.FullName : throw new ArgumentNullException(nameof(info));
            FileSystem = system;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static SegmentState ToState(SegmentType type)
        {
            return (SegmentState) (type & ~(SegmentType.Start | SegmentType.Final));
        }

        public override Int32 GetHashCode()
        {
            HashCode code = default;
            code.Add(FileSystem);
            code.Add(Uri, IsCaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
            return code.ToHashCode();
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<Segment> IEnumerable<Segment>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<Char> IEnumerable<Char>.GetEnumerator()
        {
            return Uri.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                FileSystemUri value => Equals(value),
                Enumerator value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(FileSystemUri other)
        {
            return Equals(FileSystem, other.FileSystem) && String.Equals(Uri, other.Uri, IsCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
        }

        public Boolean Equals(Enumerator other)
        {
            return Equals(other.Uri);
        }

        public override String ToString()
        {
            return Uri;
        }

        public Char this[Int32 index]
        {
            get
            {
                return Uri[index];
            }
        }

        public ReadOnlyMemory<Char> this[Range range]
        {
            get
            {
                return Uri.AsMemory(range);
            }
        }
        
        [Flags]
        public enum SegmentType : UInt16
        {
            None = 0,
            Start = 1,
            Final = 2,
            Normalized = 4,
            UNC = 8 | Start,
            Drive = 16 | Start,
            Directory = 32,
            Root = 64 | Start | Directory,
            Home = 128 | Start | Directory,
            File = 256,
            Entity = Directory | File,
            AlternateDataStream = 512 | File,
            Unknown = UInt16.MaxValue
        }

        public enum SegmentState : Byte
        {
            None = (Byte) SegmentType.None,
            Start = (Byte) SegmentType.Start,
            Final = (Byte) SegmentType.Final
        }

        public readonly struct Segment : IEquatableStruct<Segment>
        {
            public static implicit operator ReadOnlySpan<Char>(Segment value)
            {
                return value.Value.Span;
            }

            public static implicit operator ReadOnlyMemory<Char>(Segment value)
            {
                return value.Value;
            }

            public static implicit operator String(Segment value)
            {
                return value.ToString();
            }

            public static Boolean operator ==(Segment first, Segment second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Segment first, Segment second)
            {
                return !(first == second);
            }

            private readonly Enumerator Enumerator;
            
            public Byte Index { get; }
            public SegmentType Type { get; }

            public SegmentState State
            {
                get
                {
                    return ToState(Type);
                }
            }

            public Segment? Start
            {
                get
                {
                    return Enumerator.Start;
                }
            }

            private readonly ReadOnlyMemory<Char> _value;
            public ReadOnlyMemory<Char> Value
            {
                get
                {
                    return _value;
                }
            }

            public Boolean IsStart
            {
                get
                {
                    return Type.HasFlag(SegmentType.Start);
                }
            }

            public Boolean IsFinal
            {
                get
                {
                    return IsEmpty || Type.HasFlag(SegmentType.Final);
                }
            }

            public Boolean IsSingle
            {
                get
                {
                    return IsStart && IsFinal;
                }
            }

            public Boolean IsCaseSensitive
            {
                get
                {
                    return Enumerator.IsCaseSensitive;
                }
            }

            public Boolean IsEmpty
            {
                get
                {
                    return _value.Length <= 0;
                }
            }

            internal Segment(Byte index, SegmentType type, ReadOnlyMemory<Char> value)
                : this(default, index, type, value)
            {
            }

            internal Segment(Byte index, SegmentType type, String value)
                : this(default, index, type, value)
            {
            }

            internal Segment(Enumerator enumerator, Byte index, SegmentType type, ReadOnlyMemory<Char> value)
            {
                Enumerator = enumerator;
                Index = index;
                Type = type;
                _value = value;
            }

            [SuppressMessage("ReSharper", "MergeConditionalExpression")]
            internal Segment(Enumerator enumerator, Byte index, SegmentType type, String value)
                : this(enumerator, index, type, value is not null ? value.AsMemory() : throw new ArgumentNullException(nameof(value)))
            {
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Type, Value);
            }

            public override Boolean Equals(Object? other)
            {
                return other is Segment value && Equals(value);
            }

            public Boolean Equals(Segment other)
            {
                return Type == other.Type && _value.SequenceEqual(other._value, IsCaseSensitive ? CharEqualityComparer.Ordinal : CharEqualityComparer.OrdinalIgnoreCase);
            }

            public override String ToString()
            {
                return Value.ToString();
            }
        }
        
        public struct Enumerator : IEquatableStruct<Enumerator>, IEnumerator<Segment>, IEquatable<FileSystemUri>
        {
            public static Boolean operator ==(Enumerator first, Enumerator second)
            {
                return first.Same(second);
            }

            public static Boolean operator !=(Enumerator first, Enumerator second)
            {
                return !(first == second);
            }
            
            private readonly FileSystemUri _uri;
            public readonly FileSystemUri Uri
            {
                get
                {
                    return _uri;
                }
            }

            /// <inheritdoc cref="IPathHandler.PathSeparator" />
            public readonly Char PathSeparator
            {
                get
                {
                    return _uri.PathSeparator;
                }
            }

            /// <inheritdoc cref="IPathHandler.VolumeSeparatorChar" />
            public readonly Char VolumeSeparatorChar
            {
                get
                {
                    return _uri.VolumeSeparatorChar;
                }
            }

            /// <inheritdoc cref="IPathHandler.DirectorySeparatorChar" />
            public readonly Char DirectorySeparatorChar
            {
                get
                {
                    return _uri.DirectorySeparatorChar;
                }
            }

            /// <inheritdoc cref="IPathHandler.AltDirectorySeparatorChar" />
            public readonly Char AltDirectorySeparatorChar
            {
                get
                {
                    return _uri.AltDirectorySeparatorChar;
                }
            }

            /// <inheritdoc cref="IPathHandler.InvalidPathCharacters" />
            public readonly ImmutableArray<Char> InvalidPathCharacters
            {
                get
                {
                    return _uri.InvalidPathCharacters;
                }
            }

            /// <inheritdoc cref="IPathHandler.InvalidFileNameCharacters" />
            public readonly ImmutableArray<Char> InvalidFileNameCharacters
            {
                get
                {
                    return _uri.InvalidFileNameCharacters;
                }
            }

            private SegmentType _type = SegmentType.None;
            public readonly SegmentType Type
            {
                get
                {
                    return _type;
                }
            }

            public readonly SegmentState State
            {
                get
                {
                    return ToState(Type);
                }
            }

            public readonly Segment? Start
            {
                get
                {
                    Enumerator enumerator = this;
                    enumerator.Reset();
                    return enumerator.MoveNext() ? enumerator.Current : null;
                }
            }

            public readonly Boolean IsStart
            {
                get
                {
                    return Type.HasFlag(SegmentType.Start);
                }
            }

            public readonly Boolean IsFinal
            {
                get
                {
                    return IsEmpty || Type.HasFlag(SegmentType.Final);
                }
            }

            public readonly Boolean IsSingle
            {
                get
                {
                    return IsStart && IsFinal;
                }
            }

            public readonly Boolean IsCaseSensitive
            {
                get
                {
                    return _uri.IsCaseSensitive;
                }
            }

            private Byte _index = 0;
            public readonly Byte Index
            {
                get
                {
                    return _index;
                }
            }
            
            private Range _range = default;

            public readonly Segment Current
            {
                get
                {
                    return Type is not SegmentType.None ? new Segment(this, _index, Type, _uri[_range]) : throw new InvalidOperationException();
                }
            }

            readonly Object? IEnumerator.Current
            {
                get
                {
                    return IsEmpty ? Current : null;
                }
            }

            public readonly Boolean IsEmpty
            {
                get
                {
                    return Uri.IsEmpty;
                }
            }

            public Enumerator(FileSystemUri uri)
            {
                _uri = uri;
            }
            
            private readonly Boolean IsSeparator(Char character)
            {
                return character == DirectorySeparatorChar || character == AltDirectorySeparatorChar;
            }

            public Boolean MoveNext()
            {
                if (IsEmpty)
                {
                    return false;
                }

                ReadOnlySpan<Char> uri = Uri;
                return Type is SegmentType.None ? ProcessRootSegment(uri) : ProcessNextSegment(uri);
            }

            private Boolean ProcessRootSegment(ReadOnlySpan<Char> uri)
            {
                _index = 0;
                return TryProcessUNC(uri) || TryProcessHome(uri) || TryProcessDrive(uri) || TryProcessRoot(uri) || TryProcessLocalUri(uri);
            }

            private Boolean TryProcessUNC(ReadOnlySpan<Char> uri)
            {
                if (uri.Length < 2 || !IsSeparator(uri[0]) || !IsSeparator(uri[1]))
                {
                    return false;
                }

                SegmentType type = SegmentType.UNC;
                Int32 position = 2;

                if (uri.Length >= 4 && (uri[2] == '?' || uri[2] == '.') && IsSeparator(uri[3]))
                {
                    type |= SegmentType.Normalized;
                    position = 4;
                }

                while (position < uri.Length && IsSeparator(uri[position]))
                {
                    position++;
                    type |= SegmentType.Root;
                }

                Int32 end = position;
                while (end < uri.Length && !IsSeparator(uri[end]))
                {
                    end++;
                }
                
                if (end == uri.Length)
                {
                    type |= SegmentType.Final;
                }

                _type = type;
                _range = ..end;
                return true;
            }

            private Boolean TryProcessHome(ReadOnlySpan<Char> uri)
            {
                if (uri.Length < 2 || uri[0] != '~' || !IsSeparator(uri[1]))
                {
                    return false;
                }
                
                Int32 end = 2;
                SegmentType type = SegmentType.Home;
                if (end < uri.Length && IsSeparator(uri[end]))
                {
                    end = SkipSeparators(uri, end);
                }
                
                if (uri.Length == end)
                {
                    type |= SegmentType.Final;
                }

                _type = type;
                _range = ..SkipSeparators(uri, 1);
                return true;
            }

            private Boolean TryProcessDrive(ReadOnlySpan<Char> uri)
            {
                if (uri.Length < 2 || !Char.IsLetter(uri[0]) || uri[1] != VolumeSeparatorChar)
                {
                    return false;
                }

                Int32 end = 2;
                SegmentType type = SegmentType.Drive;
                if (end < uri.Length && IsSeparator(uri[end]))
                {
                    type |= SegmentType.Root;
                    end = SkipSeparators(uri, end);
                }

                if (end == uri.Length)
                {
                    type |= SegmentType.Final;
                }

                _type = type;
                _range = ..end;
                return true;
            }

            private Boolean TryProcessRoot(ReadOnlySpan<Char> uri)
            {
                if (uri.Length < 1 || !IsSeparator(uri[0]))
                {
                    return false;
                }
                
                SegmentType type = SegmentType.Root;

                if (uri.Length == 1)
                {
                    type |= SegmentType.Final;
                }

                _type = type;
                _range = ..1;
                return true;
            }

            private Boolean TryProcessLocalUri(ReadOnlySpan<Char> uri)
            {
                Int32 end = FindSegment(uri, 0);
                SegmentType type = SegmentType.Start;
                type |= end == uri.Length ? ProcessAlternateStream(uri[..end]) | SegmentType.Final : SegmentType.Directory;

                _type = type;
                _range = ..end;
                return true;
            }

            private readonly SegmentType ProcessAlternateStream(ReadOnlySpan<Char> uri)
            {
                Int32 index = uri.LastIndexOf(':');
                if (index <= 0 || index >= uri.Length - 1)
                {
                    return SegmentType.File;
                }

                ReadOnlySpan<Char> stream = uri[(index + 1)..];
                return stream.IndexOfAny(InvalidFileNameCharacters.AsSpan()) < 0 ? SegmentType.AlternateDataStream : SegmentType.File;
            }

            private Boolean ProcessNextSegment(ReadOnlySpan<Char> uri)
            {
                Int32 start = SkipSeparators(uri, _range.End.Value);
                if (start >= uri.Length)
                {
                    return false;
                }

                Int32 end = FindSegment(uri, start);
                SetSegmentType(uri, start, end);
                
                _index++;
                _range = start..end;
                return true;
            }

            private void SetSegmentType(ReadOnlySpan<Char> uri, Int32 start, Int32 end)
            {
                Int32 next = SkipSeparators(uri, end);
                _type = next == uri.Length ? ProcessAlternateStream(uri[start..end]) | SegmentType.Final : SegmentType.Directory;
            }

            private readonly Int32 SkipSeparators(ReadOnlySpan<Char> uri, Int32 start)
            {
                while (start < uri.Length && IsSeparator(uri[start]))
                {
                    start++;
                }

                return start;
            }

            private readonly Int32 FindSegment(ReadOnlySpan<Char> uri, Int32 start)
            {
                Int32 end = start;
                while (end < uri.Length && !IsSeparator(uri[end]))
                {
                    end++;
                }

                return end;
            }

            public void Reset()
            {
                _type = SegmentType.None;
                _index = 0;
                _range = default;
            }

            public override readonly Int32 GetHashCode()
            {
                return _uri.GetHashCode();
            }

            public override readonly Boolean Equals(Object? other)
            {
                return other switch
                {
                    FileSystemUri value => Equals(value),
                    Enumerator value => Equals(value),
                    _ => false
                };
            }

            public readonly Boolean Equals(FileSystemUri other)
            {
                return _uri.Equals(other);
            }

            public readonly Boolean Equals(Enumerator other)
            {
                return Equals(other._uri) && _type == other._type && _index == other._index && _range.Equals(other._range);
            }

            public readonly Boolean Same(Enumerator other)
            {
                return Equals(other._uri);
            }

            public override readonly String ToString()
            {
                return $"{{ {nameof(Enumerator)}: {_uri}, {nameof(Current)}: {(Type is not SegmentType.None ? Current : Type)} }}";
            }

            public void Dispose()
            {
                Reset();
            }
        }
    }
}