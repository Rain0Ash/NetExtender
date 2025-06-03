// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads;

namespace NetExtender.Utilities.Types
{
    public static class EncodingUtilities
    {
        private static UTF8Encoding Default { get; } = new UTF8Encoding(false);
        internal static Encoding UTF8NoBOM { get; } = new UTF8Encoding(false, true);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsStack(Int32 count)
        {
            return count >= 0 ? count <= EncodingCount.MaxSize : throw new ArgumentOutOfRangeException(nameof(count), count, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsHeap(Int32 count)
        {
            return count >= 0 ? count > EncodingCount.MaxSize : throw new ArgumentOutOfRangeException(nameof(count), count, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GetByteCount(ReadOnlySpan<Char> source, out EncodingCount result)
        {
            return GetByteCount(Default, source, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GetByteCount(this Encoding encoding, ReadOnlySpan<Char> source, out EncodingCount result)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            try
            {
                result = new EncodingCount(encoding, source);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GetByteCount(String source, out EncodingCount result)
        {
            return GetByteCount(Default, source, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GetByteCount(this Encoding encoding, String source, out EncodingCount result)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                result = new EncodingCount(encoding, source);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GetByteCount(String? first, String? second, Char? separator, out EncodingCount result)
        {
            return GetByteCount(Default, first, second, separator, out result);
        }

        public static Boolean GetByteCount(this Encoding encoding, String? first, String? second, Char? separator, out EncodingCount result)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            Int32 count;
            try
            {
                count = GetByteCount(encoding, first, second, separator);
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
            
            result = new EncodingCount(encoding, (first?.Length ?? 0) + (second?.Length ?? 0) + (separator.HasValue ? 1 : 0), count);
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GetByteCount(String? first, String? second, String? separator, out EncodingCount result)
        {
            return GetByteCount(Default, first, second, separator, out result);
        }

        public static Boolean GetByteCount(this Encoding encoding, String? first, String? second, String? separator, out EncodingCount result)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            Int32 count;
            try
            {
                count = GetByteCount(encoding, first, second, separator);
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
            
            result = new EncodingCount(encoding, (first?.Length ?? 0) + (second?.Length ?? 0) + (separator?.Length ?? 0), count);
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GetByteCount(Char? separator, out EncodingCount result, params String?[]? values)
        {
            return GetByteCount(Default, separator, out result, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean GetByteCount(this Encoding encoding, Char? separator, out EncodingCount result, params String?[]? values)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            try
            {
                Int32 count = GetByteCount(encoding, separator, values);

                Int32 i = 0;
                Int32 length = 0;
                foreach (String? value in values ?? Array.Empty<String?>())
                {
                    if (value is null)
                    {
                        continue;
                    }

                    ++i;
                    length += value.Length;
                }
            
                result = new EncodingCount(encoding, length + (--i > 0 ? i * (separator.HasValue ? 1 : 0) : 0), count);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GetByteCount(String? separator, out EncodingCount result, params String?[]? values)
        {
            return GetByteCount(Default, separator, out result, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean GetByteCount(this Encoding encoding, String? separator, out EncodingCount result, params String?[]? values)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            try
            {
                Int32 count = GetByteCount(encoding, separator, values);

                Int32 i = 0;
                Int32 length = 0;
                foreach (String? value in values ?? Array.Empty<String?>())
                {
                    if (value is null)
                    {
                        continue;
                    }

                    ++i;
                    length += value.Length;
                }
            
                result = new EncodingCount(encoding, length + (--i > 0 ? i * (separator?.Length ?? 0) : 0), count);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetByteCount(String? first, String? second, Char? separator)
        {
            return GetByteCount(Default, first, second, separator);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 GetByteCount(this Encoding encoding, String? first, String? second, Char? separator)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (first is null)
            {
                return second is not null ? encoding.GetByteCount(second) : 0;
            }

            if (second is null)
            {
                return encoding.GetByteCount(first);
            }

            return encoding.GetByteCount(first) + encoding.GetByteCount(second) + (separator is { } character ? encoding.GetByteCount(stackalloc Char[] { character }) : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetByteCount(String? first, String? second, String? separator)
        {
            return GetByteCount(Default, first, second, separator);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 GetByteCount(this Encoding encoding, String? first, String? second, String? separator)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            switch (separator?.Length)
            {
                case null:
                case 0:
                {
                    return GetByteCount((Char?) null, first, second);
                }
                case 1:
                {
                    return GetByteCount(separator[0], first, second);
                }
                default:
                {
                    if (first is null)
                    {
                        return second is not null ? encoding.GetByteCount(second) : 0;
                    }

                    if (second is null)
                    {
                        return encoding.GetByteCount(first);
                    }

                    return encoding.GetByteCount(first) + encoding.GetByteCount(second) + encoding.GetByteCount(separator);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetByteCount(Char? separator, params String?[]? values)
        {
            return GetByteCount(Default, separator, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 GetByteCount(this Encoding encoding, Char? separator, params String?[]? values)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            switch (values?.Length)
            {
                case null:
                case 0:
                {
                    return 0;
                }
                case 1:
                {
                    return values[0] is { } value ? encoding.GetByteCount(value) : 0;
                }
                case 2:
                {
                    return GetByteCount(separator, values[0], values[1]);
                }
                default:
                {
                    Int32 i = 0;
                    Int32 count = 0;
                    
                    foreach (String? value in values)
                    {
                        if (value is null)
                        {
                            continue;
                        }

                        ++i;
                        count += encoding.GetByteCount(value);
                    }
                    
                    return count + (separator is { } character && --i > 0 ? i * encoding.GetByteCount(stackalloc Char[] { character }) : 0);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetByteCount(String? separator, params String?[]? values)
        {
            return GetByteCount(Default, separator, values);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 GetByteCount(this Encoding encoding, String? separator, params String?[]? values)
        {
            switch (values?.Length)
            {
                case null:
                case 0:
                {
                    return 0;
                }
                case 1:
                {
                    return values[0] is { } value ? encoding.GetByteCount(value) : 0;
                }
                case 2:
                {
                    return GetByteCount(separator, values[0], values[1]);
                }
                default:
                {
                    Int32 i = 0;
                    Int32 count = 0;
                    
                    foreach (String? value in values)
                    {
                        if (value is null)
                        {
                            continue;
                        }

                        ++i;
                        count += encoding.GetByteCount(value);
                    }
                    
                    return count + (!String.IsNullOrEmpty(separator) && --i > 0 ? i * encoding.GetByteCount(separator) : 0);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void GetBytes(this EncodingCount encoding, Span<Byte> destination)
        {
            if (encoding.IsEmpty)
            {
                throw new ArgumentNullException(nameof(encoding));
            }
            
            if (!encoding.Source.HasValue || encoding.Source.Value is not { } source)
            {
                throw new InvalidOperationException("Result must have a source value.");
            }

            if (encoding != destination.Length)
            {
                throw new ArgumentException("Destination length does not match.", nameof(destination));
            }
            
            encoding.Encoding.GetBytes(source, destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void GetBytes(this EncodingCount encoding, ReadOnlySpan<Char> source, Span<Byte> destination)
        {
            if (encoding.IsEmpty)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (encoding != destination.Length)
            {
                throw new ArgumentException("Destination length does not match.", nameof(destination));
            }
            
            encoding.Encoding.GetBytes(source, destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Byte> GetBytes(ReadOnlySpan<Char> source, Span<Byte> destination)
        {
            return GetBytes(Default, source, destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Byte> GetBytes(this Encoding encoding, ReadOnlySpan<Char> source, Span<Byte> destination)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            GetBytes(encoding, source, destination, out Int32 written);
            return destination.Slice(written);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Byte> GetBytes(String source, Span<Byte> destination)
        {
            return GetBytes(Default, source, destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Byte> GetBytes(this Encoding encoding, String source, Span<Byte> destination)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            GetBytes(encoding, source, destination, out Int32 written);
            return destination.Slice(written);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetBytes(ReadOnlySpan<Char> source, Span<Byte> destination, out Int32 written)
        {
            GetBytes(Default, source, destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void GetBytes(this Encoding encoding, ReadOnlySpan<Char> source, Span<Byte> destination, out Int32 written)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (TryGetBytes(encoding, source, destination, out written))
            {
                return;
            }

            if (written > destination.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(destination), destination.Length, $"The destination span is too small. Destination require length of at least '{written}'.");
            }

            throw new InvalidOperationException("Unknown exception.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetBytes(String source, Span<Byte> destination, out Int32 written)
        {
            GetBytes(Default, source, destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void GetBytes(this Encoding encoding, String source, Span<Byte> destination, out Int32 written)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (TryGetBytes(encoding, source, destination, out written))
            {
                return;
            }

            if (written > destination.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(destination), destination.Length, $"The destination span is too small. Destination require length of at least '{written}'.");
            }

            throw new InvalidOperationException("Unknown exception.");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] GetBytes(String? first, String? second, Char? separator)
        {
            return GetBytes(Default, first, second, separator);
        }

        public static Byte[] GetBytes(this Encoding encoding, String? first, String? second, Char? separator)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            Int32 count = GetByteCount(encoding, first, second, separator);
            if (count <= 0)
            {
                return Array.Empty<Byte>();
            }

            Byte[] result = new Byte[count];
            
            Int32 offset = 0;
            if (!String.IsNullOrEmpty(first))
            {
                offset += encoding.GetBytes(first, 0, first.Length, result, offset);
            }

            if (first is not null && second is not null && separator.HasValue)
            {
                offset += encoding.GetBytes(stackalloc Char[] { separator.Value }, result.AsSpan(offset));
            }

            if (!String.IsNullOrEmpty(second))
            {
                offset += encoding.GetBytes(second, 0, second.Length, result, offset);
            }

            if (count != offset)
            {
                throw new NeverOperationException("Count must match offset after encoding.");
            }
            
            return result;
        }

        public static Span<Byte> GetBytes(String? first, String? second, Char? separator, Span<Byte> destination)
        {
            return GetBytes(Default, first, second, separator, destination);
        }

        public static Span<Byte> GetBytes(this Encoding encoding, String? first, String? second, Char? separator, Span<Byte> destination)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (TryGetBytes(encoding, first, second, separator, destination, out Int32 written))
            {
                return destination.Slice(written);
            }
            
            if (written > destination.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(destination), destination.Length, $"The destination span is too small. Destination require length of at least '{written}'.");
            }

            throw new InvalidOperationException("Unknown exception.");
        }
        
        public static void GetBytes(String? first, String? second, Char? separator, Span<Byte> destination, out Int32 written)
        {
            GetBytes(Default, first, second, separator, destination, out written);
        }

        public static void GetBytes(this Encoding encoding, String? first, String? second, Char? separator, Span<Byte> destination, out Int32 written)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (TryGetBytes(encoding, first, second, separator, destination, out written))
            {
                return;
            }
            
            if (written > destination.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(destination), destination.Length, $"The destination span is too small. Destination require length of at least '{written}'.");
            }

            throw new InvalidOperationException("Unknown exception.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetBytes(String source, ref Span<Byte> destination)
        {
            return TryGetBytes(Default, source, ref destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetBytes(this Encoding encoding, String source, ref Span<Byte> destination)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!TryGetBytes(encoding, source, destination, out Int32 length))
            {
                return false;
            }

            destination = destination.Slice(length);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetBytes(ReadOnlySpan<Char> source, ref Span<Byte> destination)
        {
            return TryGetBytes(Default, source, ref destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetBytes(this Encoding encoding, ReadOnlySpan<Char> source, ref Span<Byte> destination)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (!TryGetBytes(encoding, source, destination, out Int32 length))
            {
                return false;
            }

            destination = destination.Slice(length);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetBytes(String source, Span<Byte> destination, out Int32 written)
        {
            return TryGetBytes(Default, source, destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryGetBytes(this Encoding encoding, String source, Span<Byte> destination, out Int32 written)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination.Length < (written = encoding.GetByteCount(source)))
            {
                return false;
            }

            try
            {
                written = encoding.GetBytes(source, destination);
                return true;
            }
            catch (Exception)
            {
                written = 0;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetBytes(ReadOnlySpan<Char> source, Span<Byte> destination, out Int32 written)
        {
            return TryGetBytes(Default, source, destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryGetBytes(this Encoding encoding, ReadOnlySpan<Char> source, Span<Byte> destination, out Int32 written)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (destination.Length < (written = encoding.GetByteCount(source)))
            {
                return false;
            }

            try
            {
                written = encoding.GetBytes(source, destination);
                return true;
            }
            catch (Exception)
            {
                written = 0;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetBytes(String? first, String? second, Char? separator, ref Span<Byte> destination)
        {
            return TryGetBytes(Default, first, second, separator, ref destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetBytes(this Encoding encoding, String? first, String? second, Char? separator, ref Span<Byte> destination)
        {
            if (!TryGetBytes(encoding, first, second, separator, destination, out Int32 written))
            {
                return false;
            }

            destination = destination.Slice(written);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetBytes(String? first, String? second, Char? separator, Span<Byte> destination, out Int32 written)
        {
            return TryGetBytes(Default, first, second, separator, destination, out written);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryGetBytes(this Encoding encoding, String? first, String? second, Char? separator, Span<Byte> destination, out Int32 written)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if ((written = encoding.GetByteCount(first, second, separator)) > destination.Length)
            {
                return false;
            }

            written = 0;
            if (!String.IsNullOrEmpty(first))
            {
                Int32 count = encoding.GetBytes(first, destination);
                written += count;
                destination = destination.Slice(count);
            }

            if (first is not null && second is not null && separator.HasValue)
            {
                Int32 count = encoding.GetBytes(stackalloc Char[] { separator.Value }, destination);
                written += count;
                destination = destination.Slice(count);
            }

            if (!String.IsNullOrEmpty(second))
            {
                Int32 count = encoding.GetBytes(second, destination);
                written += count;
            }

            return true;
        }
    }
    
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public readonly struct EncodingCount : IEquatableStruct<EncodingCount>
    {
        public static implicit operator Int32(EncodingCount value)
        {
            return value.Count;
        }

        public static Boolean operator ==(EncodingCount first, EncodingCount second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(EncodingCount first, EncodingCount second)
        {
            return !(first == second);
        }

        public static Int32 operator +(EncodingCount first, Int32 second)
        {
            return first.Count + second;
        }

        public static Int32 operator +(EncodingCount first, EncodingCount second)
        {
            return first.Count + second.Count;
        }

        public const UInt16 MaxSize = UInt16.MaxValue / 4 + 1;

        public Encoding Encoding { get; }

        private readonly Int32 _count;

        private Int32 Count
        {
            get
            {
                return !IsEmpty ? _count : throw new InvalidOperationException("Result is default.");
            }
        }

        internal Maybe<String?> Source { get; } = null;
        internal Int32 Length { get; }

        public Boolean IsStack
        {
            get
            {
                return !IsEmpty ? EncodingUtilities.IsStack(_count) : throw new InvalidOperationException("Result is default.");
            }
        }

        public Boolean IsHeap
        {
            get
            {
                return !IsEmpty ? EncodingUtilities.IsHeap(_count) : throw new InvalidOperationException("Result is default.");
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Encoding is null;
            }
        }

        internal EncodingCount(Encoding encoding, Int32 length, Int32 count)
        {
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            Source = default;
            Length = length >= 0 ? length : throw new ArgumentOutOfRangeException(nameof(length), length, null);
            _count = count >= 0 ? count : throw new ArgumentOutOfRangeException(nameof(count), count, null);
        }

        internal EncodingCount(Encoding encoding, ReadOnlySpan<Char> source)
        {
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            Source = new Maybe<String?>(null);
            Length = source.Length;
            _count = encoding.GetByteCount(source);
        }

        internal EncodingCount(Encoding encoding, String source)
        {
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            Source = source is not null ? new Maybe<String?>(source) : throw new ArgumentNullException(nameof(source));
            Length = source.Length;
            _count = encoding.GetByteCount(source);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(_count, Encoding, Source, Length);
        }

        public override Boolean Equals(Object? other)
        {
            return other is EncodingCount result && Equals(result);
        }

        public Boolean Equals(EncodingCount other)
        {
            return Encoding?.Equals(other.Encoding) is true && _count == other._count && Length == other.Length && Source.Equals(other.Source);
        }

        public override String? ToString()
        {
            return Encoding is not null ? $"{Encoding.EncodingName}: {Count}" : null;
        }
    }
}