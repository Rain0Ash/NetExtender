// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using NetExtender.Types.Random.Interfaces;
using NetExtender.Types.Spans;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class SpanUtilities
    {
        public static IImmutableSet<Type> MemorySpanType { get; } = new HashSet<Type>
        {
            typeof(Memory<>), typeof(ReadOnlyMemory<>), typeof(Span<>), typeof(ReadOnlySpan<>)
        }.ToImmutableHashSet();

        public static Boolean IsMemorySpan(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type generic = type.TryGetGenericTypeDefinition();
            return MemorySpanType.Contains(generic);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> AsReadOnly<T>(this Memory<T> span)
        {
            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnly<T>(this Span<T> span)
        {
            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetReference<T>(this Memory<T> source)
        {
            return ref GetReference(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetReference<T>(this Span<T> source)
        {
            return ref MemoryMarshal.GetReference(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetReference<T>(this ReadOnlyMemory<T> source)
        {
            return ref GetReference(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetReference<T>(this ReadOnlySpan<T> source)
        {
            return ref MemoryMarshal.GetReference(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Read<T>(this Memory<Byte> source) where T : struct
        {
            return Read<T>(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Read<T>(this Span<Byte> source) where T : struct
        {
            return MemoryMarshal.Read<T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Read<T>(this ReadOnlyMemory<Byte> source) where T : struct
        {
            return Read<T>(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Read<T>(this ReadOnlySpan<Byte> source) where T : struct
        {
            return MemoryMarshal.Read<T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<TTo> As<TFrom, TTo>(this Memory<TFrom> source) where TFrom : struct where TTo : struct
        {
            return As<TFrom, TTo>(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<TTo> As<TFrom, TTo>(this Span<TFrom> source) where TFrom : struct where TTo : struct
        {
            return MemoryMarshal.Cast<TFrom, TTo>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<TTo> As<TFrom, TTo>(this ReadOnlyMemory<TFrom> source) where TFrom : struct where TTo : struct
        {
            return As<TFrom, TTo>(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<TTo> As<TFrom, TTo>(this ReadOnlySpan<TFrom> source) where TFrom : struct where TTo : struct
        {
            return MemoryMarshal.Cast<TFrom, TTo>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Byte> AsBytes<T>(this Memory<T> source) where T : struct
        {
            return AsBytes(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Byte> AsBytes<T>(this Span<T> source) where T : struct
        {
            return MemoryMarshal.AsBytes(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<Byte> AsBytes<T>(this ReadOnlyMemory<T> source) where T : struct
        {
            return AsBytes(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<Byte> AsBytes<T>(this ReadOnlySpan<T> source) where T : struct
        {
            return MemoryMarshal.AsBytes(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> Compress<T>(this Memory<T> source)
        {
            return Compress(source, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> Compress<T>(this Memory<T> source, out Int32 count)
        {
            Compress(source.Span, out count);
            return source.Slice(0, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Compress<T>(this Span<T> source)
        {
            return Compress(source, out _);
        }

        public static Span<T> Compress<T>(this Span<T> source, out Int32 count)
        {
            count = 0;
            for (Int32 i = 0; i < source.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(source[i], default))
                {
                    continue;
                }

                (source[count], source[i]) = (source[i], source[count]);
                count++;
            }

            return source.Slice(0, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> Change<T>(this Memory<T> source, Func<T, T> selector)
        {
            Change(source.Span, selector);
            return source;
        }

        public static Span<T> Change<T>(this Span<T> source, Func<T, T> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            for (Int32 i = 0; i < source.Length; i++)
            {
                source[i] = selector(source[i]);
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this Memory<T> source, Int32 first, Int32 second)
        {
            Swap(source.Span, first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this Span<T> source, Int32 first, Int32 second)
        {
            (source[first], source[second]) = (source[second], source[first]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Memory<T> source)
        {
            source.Span.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Memory<T> source, Int32 start)
        {
            Clear(source.Span, start);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Memory<T> source, Int32 start, Int32 length)
        {
            Clear(source.Span, start, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Span<T> source, Int32 start)
        {
            Clear(source, start, source.Length - start);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Span<T> source, Int32 start, Int32 length)
        {
            source.Slice(start, length).Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Memory<T> source, Boolean crypto) where T : struct
        {
            Clear(source.Span, crypto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Memory<T> source, Int32 start, Boolean crypto) where T : struct
        {
            Clear(source.Span, start, crypto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Memory<T> source, Int32 start, Int32 length, Boolean crypto) where T : struct
        {
            Clear(source.Span, start, length, crypto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Span<T> source, Boolean crypto) where T : struct
        {
            if (crypto)
            {
                CryptographicOperations.ZeroMemory(source.As<T, Byte>());
                return;
            }
            
            source.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Span<T> source, Int32 start, Boolean crypto) where T : struct
        {
            Clear(source, start, source.Length - start, crypto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Span<T> source, Int32 start, Int32 length, Boolean crypto) where T : struct
        {
            Clear(source.Slice(start, length), crypto);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this Memory<T> source, ReadOnlySpan<T> other)
        {
            return SequenceEqual(source, other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this Memory<T> source, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer)
        {
            return SequenceEqual(source.Span, other, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this Memory<T> source, ReadOnlyMemory<T> other)
        {
            return SequenceEqual(source, other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this Memory<T> source, ReadOnlyMemory<T> other, IEqualityComparer<T>? comparer)
        {
            return SequenceEqual(source.Span, other.Span, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this Span<T> source, ReadOnlySpan<T> other)
        {
            return SequenceEqual(source, other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this Span<T> source, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer)
        {
            return SequenceEqual((ReadOnlySpan<T>) source, other, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this ReadOnlyMemory<T> source, ReadOnlySpan<T> other)
        {
            return SequenceEqual(source, other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this ReadOnlyMemory<T> source, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer)
        {
            return SequenceEqual(source.Span, other, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this ReadOnlyMemory<T> source, ReadOnlyMemory<T> other)
        {
            return SequenceEqual(source, other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this ReadOnlyMemory<T> source, ReadOnlyMemory<T> other, IEqualityComparer<T>? comparer)
        {
            return SequenceEqual(source.Span, other.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this ReadOnlySpan<T> source, ReadOnlySpan<T> other)
        {
            return SequenceEqual(source, other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<T>(this ReadOnlySpan<T> source, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer)
        {
            if (source.Length != other.Length)
            {
                return false;
            }
            
            comparer ??= EqualityComparer<T>.Default;
            for (Int32 i = 0; i < source.Length; i++)
            {
                if (!comparer.Equals(source[i], other[i]))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this Memory<T> source, IEnumerable<T>? other)
        {
            return SequenceEqual(source, other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this Memory<T> source, IEnumerable<T>? other, IEqualityComparer<T>? comparer)
        {
            return SequenceEqual(source.Span, other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this Span<T> source, IEnumerable<T>? other)
        {
            return SequenceEqual(source, other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this Span<T> source, IEnumerable<T>? other, IEqualityComparer<T>? comparer)
        {
            return SequenceEqual((ReadOnlySpan<T>) source, other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this ReadOnlyMemory<T> source, IEnumerable<T>? other)
        {
            return SequenceEqual(source, other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this ReadOnlyMemory<T> source, IEnumerable<T>? other, IEqualityComparer<T>? comparer)
        {
            return SequenceEqual(source.Span, other, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<T>(this ReadOnlySpan<T> source, IEnumerable<T>? other)
        {
            return SequenceEqual(source, other, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<T>(this ReadOnlySpan<T> source, IEnumerable<T>? other, IEqualityComparer<T>? comparer)
        {
            if (other is null)
            {
                return source.IsEmpty;
            }

            if (other.CountIfMaterialized(out Int32 count) && count != source.Length)
            {
                return false;
            }

            comparer ??= EqualityComparer<T>.Default;
            
            using IEnumerator<T> enumerator = other.GetEnumerator();
            foreach (T item in source)
            {
                if (!enumerator.MoveNext() || !comparer.Equals(item, enumerator.Current))
                {
                    return false;
                }
            }

            return !enumerator.MoveNext();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Memory<T> source)
        {
            return !source.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Span<T> source)
        {
            return !source.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlyMemory<T> source)
        {
            return !source.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlySpan<T> source)
        {
            return !source.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return Any(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return Any((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return Any(source.Span, predicate);
        }

        public static Boolean Any<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Memory<T> source, Func<T, Int32, Boolean> predicate)
        {
            return Any(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Span<T> source, Func<T, Int32, Boolean> predicate)
        {
            return Any((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlyMemory<T> source, Func<T, Int32, Boolean> predicate)
        {
            return Any(source.Span, predicate);
        }

        public static Boolean Any<T>(this ReadOnlySpan<T> source, Func<T, Int32, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 i = 0;
            foreach (T item in source)
            {
                if (predicate(item, i++))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return All(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return !Any(source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return All(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            return !Any(source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this Memory<T> source, Func<T, Int32, Boolean> predicate)
        {
            return All(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this Span<T> source, Func<T, Int32, Boolean> predicate)
        {
            return !Any(source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this ReadOnlyMemory<T> source, Func<T, Int32, Boolean> predicate)
        {
            return All(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this ReadOnlySpan<T> source, Func<T, Int32, Boolean> predicate)
        {
            return !Any(source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Memory<T> source)
        {
            return source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Span<T> source)
        {
            return source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlyMemory<T> source)
        {
            return source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlySpan<T> source)
        {
            return source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Memory<T> source, T value)
        {
            return Count(source, value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Memory<T> source, T value, IEqualityComparer<T>? comparer)
        {
            return Count(source.Span, value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Span<T> source, T value)
        {
            return Count(source, value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Span<T> source, T value, IEqualityComparer<T>? comparer)
        {
            return Count((ReadOnlySpan<T>) source, value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlyMemory<T> source, T value)
        {
            return Count(source, value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlyMemory<T> source, T value, IEqualityComparer<T>? comparer)
        {
            return Count(source.Span, value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlySpan<T> source, T value)
        {
            return Count(source, value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Int32 Count<T>(this ReadOnlySpan<T> source, T value, IEqualityComparer<T>? comparer)
        {
            Int32 count = 0;
            comparer ??= EqualityComparer<T>.Default;

            foreach (T item in source)
            {
                if (comparer.Equals(item, value))
                {
                    count++;
                }
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return Count(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return Count((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return Count(source.Span, predicate);
        }

        public static Int32 Count<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 count = 0;

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    count++;
                }
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CountWhile<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return CountWhile(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CountWhile<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return CountWhile((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CountWhile<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return CountWhile(source.Span, predicate);
        }

        public static Int32 CountWhile<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 count = 0;
            foreach (T item in source)
            {
                if (!predicate(item))
                {
                    return count;
                }

                count++;
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CountWhile<T>(this Memory<T> source, Func<T, Int32, Boolean> predicate)
        {
            return CountWhile(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CountWhile<T>(this Span<T> source, Func<T, Int32, Boolean> predicate)
        {
            return CountWhile((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CountWhile<T>(this ReadOnlyMemory<T> source, Func<T, Int32, Boolean> predicate)
        {
            return CountWhile(source.Span, predicate);
        }

        public static Int32 CountWhile<T>(this ReadOnlySpan<T> source, Func<T, Int32, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 i = 0;
            Int32 count = 0;
            foreach (T item in source)
            {
                if (!predicate(item, i++))
                {
                    return count;
                }

                count++;
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ReverseCountWhile<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return ReverseCountWhile(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ReverseCountWhile<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return ReverseCountWhile((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ReverseCountWhile<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return ReverseCountWhile(source.Span, predicate);
        }

        public static Int32 ReverseCountWhile<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 count = 0;
            for (Int32 i = source.Length - 1; i >= 0; i--)
            {
                if (!predicate(source[i]))
                {
                    return count;
                }

                count++;
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ReverseCountWhile<T>(this Memory<T> source, Func<T, Int32, Boolean> predicate)
        {
            return ReverseCountWhile(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ReverseCountWhile<T>(this Span<T> source, Func<T, Int32, Boolean> predicate)
        {
            return ReverseCountWhile((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ReverseCountWhile<T>(this ReadOnlyMemory<T> source, Func<T, Int32, Boolean> predicate)
        {
            return ReverseCountWhile(source.Span, predicate);
        }

        public static Int32 ReverseCountWhile<T>(this ReadOnlySpan<T> source, Func<T, Int32, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 count = 0;
            for (Int32 i = source.Length - 1; i >= 0; i--)
            {
                if (!predicate(source[i], source.Length - i - 1))
                {
                    return count;
                }

                count++;
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this Memory<T> source, T value)
        {
            return IndexOf(source.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this Span<T> source, T value)
        {
            return IndexOf((ReadOnlySpan<T>) source, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this ReadOnlyMemory<T> source, T value)
        {
            return IndexOf(source.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this ReadOnlySpan<T> source, T value)
        {
            return IndexOf(source, value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this Memory<T> source, T value, IEqualityComparer<T>? comparer)
        {
            return IndexOf(source.Span, value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this Span<T> source, T value, IEqualityComparer<T>? comparer)
        {
            return IndexOf((ReadOnlySpan<T>) source, value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this ReadOnlyMemory<T> source, T value, IEqualityComparer<T>? comparer)
        {
            return IndexOf(source.Span, value, comparer);
        }

        public static Int32 IndexOf<T>(this ReadOnlySpan<T> source, T value, IEqualityComparer<T>? comparer)
        {
            comparer ??= EqualityComparer<T>.Default;

            Int32 index = 0;
            foreach (T item in source)
            {
                if (comparer.Equals(item, value))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IndexOf<T>(this Memory<T> source, T value, out Int32 index)
        {
            return IndexOf(source.Span, value, out index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IndexOf<T>(this Span<T> source, T value, out Int32 index)
        {
            return IndexOf((ReadOnlySpan<T>) source, value, out index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IndexOf<T>(this ReadOnlyMemory<T> source, T value, out Int32 index)
        {
            return IndexOf(source.Span, value, out index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IndexOf<T>(this ReadOnlySpan<T> source, T value, out Int32 index)
        {
            index = IndexOf(source, value);
            return index >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IndexOf<T>(this Memory<T> source, T value, IEqualityComparer<T>? comparer, out Int32 index)
        {
            return IndexOf(source.Span, value, comparer, out index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IndexOf<T>(this Span<T> source, T value, IEqualityComparer<T>? comparer, out Int32 index)
        {
            return IndexOf((ReadOnlySpan<T>) source, value, comparer, out index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IndexOf<T>(this ReadOnlyMemory<T> source, T value, IEqualityComparer<T>? comparer, out Int32 index)
        {
            return IndexOf(source.Span, value, comparer, out index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IndexOf<T>(this ReadOnlySpan<T> source, T value, IEqualityComparer<T>? comparer, out Int32 index)
        {
            index = IndexOf(source, value, comparer);
            return index >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindIndex<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return FindIndex(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindIndex<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return FindIndex((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindIndex<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return FindIndex(source.Span, predicate);
        }

        public static Int32 FindIndex<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return index;
                }

                ++index;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean FindIndex<T>(this Memory<T> source, Func<T, Boolean> predicate, out Int32 index)
        {
            return FindIndex(source.Span, predicate, out index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean FindIndex<T>(this Span<T> source, Func<T, Boolean> predicate, out Int32 index)
        {
            return FindIndex((ReadOnlySpan<T>) source, predicate, out index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean FindIndex<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate, out Int32 index)
        {
            return FindIndex(source.Span, predicate, out index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean FindIndex<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate, out Int32 index)
        {
            index = FindIndex(source, predicate);
            return index >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Single<T>(this Memory<T> source)
        {
            return Single(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Single<T>(this Span<T> source)
        {
            return Single((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Single<T>(this ReadOnlyMemory<T> source)
        {
            return Single(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Single<T>(this ReadOnlySpan<T> source)
        {
            return source.Length == 1 ? source[0] : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Single<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return Single(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Single<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return Single((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Single<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return Single(source.Span, predicate);
        }

        public static T Single<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            do
            {
                T item = enumerator.Current;

                if (!predicate(item))
                {
                    continue;
                }

                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        throw new InvalidOperationException();
                    }
                }

                return item;

            } while (enumerator.MoveNext());

            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? SingleOrDefault<T>(this Memory<T> source)
        {
            return SingleOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? SingleOrDefault<T>(this Span<T> source)
        {
            return SingleOrDefault((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? SingleOrDefault<T>(this ReadOnlyMemory<T> source)
        {
            return SingleOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? SingleOrDefault<T>(this ReadOnlySpan<T> source)
        {
            return source.Length == 1 ? source[0] : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? SingleOrDefault<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return SingleOrDefault(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? SingleOrDefault<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return SingleOrDefault((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? SingleOrDefault<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return SingleOrDefault(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? SingleOrDefault<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            return SingleOrDefault(source!, predicate!, default(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this Memory<T> source, T alternate)
        {
            return SingleOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this Span<T> source, T alternate)
        {
            return SingleOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this ReadOnlyMemory<T> source, T alternate)
        {
            return SingleOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this ReadOnlySpan<T> source, T alternate)
        {
            return source.Length == 1 ? source[0] : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this Memory<T> source, Func<T, Boolean> predicate, T alternate)
        {
            return SingleOrDefault(source.Span, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this Span<T> source, Func<T, Boolean> predicate, T alternate)
        {
            return SingleOrDefault((ReadOnlySpan<T>) source, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate, T alternate)
        {
            return SingleOrDefault(source.Span, predicate, alternate);
        }

        public static T SingleOrDefault<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return alternate;
            }

            do
            {
                T item = enumerator.Current;

                if (!predicate(item))
                {
                    continue;
                }

                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        return alternate;
                    }
                }

                return item;

            } while (enumerator.MoveNext());

            return alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this Memory<T> source, Func<T> alternate)
        {
            return SingleOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this Span<T> source, Func<T> alternate)
        {
            return SingleOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this ReadOnlyMemory<T> source, Func<T> alternate)
        {
            return SingleOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this ReadOnlySpan<T> source, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.Length == 1 ? source[0] : alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this Memory<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            return SingleOrDefault(source.Span, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this Span<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            return SingleOrDefault((ReadOnlySpan<T>) source, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SingleOrDefault<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            return SingleOrDefault(source.Span, predicate, alternate);
        }

        // ReSharper disable once CognitiveComplexity
        public static T SingleOrDefault<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return alternate();
            }

            do
            {
                T item = enumerator.Current;

                if (!predicate(item))
                {
                    continue;
                }

                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        return alternate();
                    }
                }

                return item;

            } while (enumerator.MoveNext());

            return alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetSingle<T>(this Memory<T> source, [MaybeNullWhen(false)] out T result)
        {
            return TryGetSingle(source.Span, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetSingle<T>(this Span<T> source, [MaybeNullWhen(false)] out T result)
        {
            return TryGetSingle((ReadOnlySpan<T>) source, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetSingle<T>(this ReadOnlyMemory<T> source, [MaybeNullWhen(false)] out T result)
        {
            return TryGetSingle(source.Span, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetSingle<T>(this ReadOnlySpan<T> source, [MaybeNullWhen(false)] out T result)
        {
            if (source.Length == 1)
            {
                result = source[0];
                return true;
            }

            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetSingle<T>(this Memory<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            return TryGetSingle(source.Span, predicate, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetSingle<T>(this Span<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            return TryGetSingle((ReadOnlySpan<T>) source, predicate, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetSingle<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            return TryGetSingle(source.Span, predicate, out result);
        }

        public static Boolean TryGetSingle<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                result = default;
                return false;
            }

            do
            {
                T item = enumerator.Current;

                if (!predicate(item))
                {
                    continue;
                }

                while (enumerator.MoveNext())
                {
                    if (!predicate(enumerator.Current))
                    {
                        continue;
                    }

                    result = default;
                    return false;
                }

                result = item;
                return true;

            } while (enumerator.MoveNext());

            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T First<T>(this Memory<T> source)
        {
            return First(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T First<T>(this Span<T> source)
        {
            return First((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T First<T>(this ReadOnlyMemory<T> source)
        {
            return First(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T First<T>(this ReadOnlySpan<T> source)
        {
            return source.Length >= 1 ? source[0] : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T First<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return First(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T First<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return First((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T First<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return First(source.Span, predicate);
        }

        public static T First<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FirstOrDefault<T>(this Memory<T> source)
        {
            return FirstOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FirstOrDefault<T>(this Span<T> source)
        {
            return FirstOrDefault((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FirstOrDefault<T>(this ReadOnlyMemory<T> source)
        {
            return FirstOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FirstOrDefault<T>(this ReadOnlySpan<T> source)
        {
            return source.Length >= 1 ? source[0] : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FirstOrDefault<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return FirstOrDefault(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FirstOrDefault<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return FirstOrDefault((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FirstOrDefault<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return FirstOrDefault(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FirstOrDefault<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            return FirstOrDefault(source!, predicate!, default(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this Memory<T> source, T alternate)
        {
            return FirstOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this Span<T> source, T alternate)
        {
            return FirstOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this ReadOnlyMemory<T> source, T alternate)
        {
            return FirstOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this ReadOnlySpan<T> source, T alternate)
        {
            return source.Length >= 1 ? source[0] : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this Memory<T> source, Func<T, Boolean> predicate, T alternate)
        {
            return FirstOrDefault(source.Span, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this Span<T> source, Func<T, Boolean> predicate, T alternate)
        {
            return FirstOrDefault((ReadOnlySpan<T>) source, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate, T alternate)
        {
            return FirstOrDefault(source.Span, predicate, alternate);
        }

        public static T FirstOrDefault<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this Memory<T> source, Func<T> alternate)
        {
            return FirstOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this Span<T> source, Func<T> alternate)
        {
            return FirstOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this ReadOnlyMemory<T> source, Func<T> alternate)
        {
            return FirstOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this ReadOnlySpan<T> source, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.Length >= 1 ? source[0] : alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this Memory<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            return FirstOrDefault(source.Span, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this Span<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            return FirstOrDefault((ReadOnlySpan<T>) source, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            return FirstOrDefault(source.Span, predicate, alternate);
        }

        // ReSharper disable once CognitiveComplexity
        public static T FirstOrDefault<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetFirst<T>(this Memory<T> source, [MaybeNullWhen(false)] out T result)
        {
            return TryGetFirst(source.Span, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetFirst<T>(this Span<T> source, [MaybeNullWhen(false)] out T result)
        {
            return TryGetFirst((ReadOnlySpan<T>) source, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetFirst<T>(this ReadOnlyMemory<T> source, [MaybeNullWhen(false)] out T result)
        {
            return TryGetFirst(source.Span, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetFirst<T>(this ReadOnlySpan<T> source, [MaybeNullWhen(false)] out T result)
        {
            if (source.Length <= 0)
            {
                result = default;
                return false;
            }

            result = source[0];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetFirst<T>(this Memory<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            return TryGetFirst(source.Span, predicate, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetFirst<T>(this Span<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            return TryGetFirst((ReadOnlySpan<T>) source, predicate, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetFirst<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            return TryGetFirst(source.Span, predicate, out result);
        }

        public static Boolean TryGetFirst<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (!predicate(item))
                {
                    continue;
                }

                result = item;
                return true;
            }

            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Last<T>(this Memory<T> source)
        {
            return Last(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Last<T>(this Span<T> source)
        {
            return Last((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Last<T>(this ReadOnlyMemory<T> source)
        {
            return Last(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Last<T>(this ReadOnlySpan<T> source)
        {
            return source.Length >= 1 ? source[^1] : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Last<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return Last(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Last<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return Last((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Last<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return Last(source.Span, predicate);
        }

        public static T Last<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (Int32 index = source.Length - 1; index >= 0; --index)
            {
                T item = source[index];
                if (predicate(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? LastOrDefault<T>(this Memory<T> source)
        {
            return LastOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? LastOrDefault<T>(this Span<T> source)
        {
            return LastOrDefault((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? LastOrDefault<T>(this ReadOnlyMemory<T> source)
        {
            return LastOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? LastOrDefault<T>(this ReadOnlySpan<T> source)
        {
            return source.Length >= 1 ? source[^1] : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? LastOrDefault<T>(this Memory<T> source, Func<T, Boolean> predicate)
        {
            return LastOrDefault(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? LastOrDefault<T>(this Span<T> source, Func<T, Boolean> predicate)
        {
            return LastOrDefault((ReadOnlySpan<T>) source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? LastOrDefault<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate)
        {
            return LastOrDefault(source.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? LastOrDefault<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate)
        {
            return LastOrDefault(source!, predicate!, default(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this Memory<T> source, T alternate)
        {
            return LastOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this Span<T> source, T alternate)
        {
            return LastOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this ReadOnlyMemory<T> source, T alternate)
        {
            return LastOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this ReadOnlySpan<T> source, T alternate)
        {
            return source.Length >= 1 ? source[^1] : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this Memory<T> source, Func<T, Boolean> predicate, T alternate)
        {
            return LastOrDefault(source.Span, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this Span<T> source, Func<T, Boolean> predicate, T alternate)
        {
            return LastOrDefault((ReadOnlySpan<T>) source, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate, T alternate)
        {
            return LastOrDefault(source.Span, predicate, alternate);
        }

        public static T LastOrDefault<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (Int32 index = source.Length - 1; index >= 0; --index)
            {
                T item = source[index];
                if (predicate(item))
                {
                    return item;
                }
            }

            return alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this Memory<T> source, Func<T> alternate)
        {
            return LastOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this Span<T> source, Func<T> alternate)
        {
            return LastOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this ReadOnlyMemory<T> source, Func<T> alternate)
        {
            return LastOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this ReadOnlySpan<T> source, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.Length >= 1 ? source[^1] : alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this Memory<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            return LastOrDefault(source.Span, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this Span<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            return LastOrDefault((ReadOnlySpan<T>) source, predicate, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LastOrDefault<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            return LastOrDefault(source.Span, predicate, alternate);
        }

        public static T LastOrDefault<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            for (Int32 index = source.Length - 1; index >= 0; --index)
            {
                T item = source[index];
                if (predicate(item))
                {
                    return item;
                }
            }

            return alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetLast<T>(this Memory<T> source, [MaybeNullWhen(false)] out T result)
        {
            return TryGetLast(source.Span, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetLast<T>(this Span<T> source, [MaybeNullWhen(false)] out T result)
        {
            return TryGetLast((ReadOnlySpan<T>) source, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetLast<T>(this ReadOnlyMemory<T> source, [MaybeNullWhen(false)] out T result)
        {
            return TryGetLast(source.Span, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetLast<T>(this ReadOnlySpan<T> source, [MaybeNullWhen(false)] out T result)
        {
            if (source.Length <= 0)
            {
                result = default;
                return false;
            }

            result = source[^1];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetLast<T>(this Memory<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            return TryGetLast(source.Span, predicate, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetLast<T>(this Span<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            return TryGetLast((ReadOnlySpan<T>) source, predicate, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetLast<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            return TryGetLast(source.Span, predicate, out result);
        }

        public static Boolean TryGetLast<T>(this ReadOnlySpan<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (Int32 index = source.Length - 1; index >= 0; --index)
            {
                T item = source[index];
                if (!predicate(item))
                {
                    continue;
                }

                result = item;
                return true;
            }

            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEach<T>(this Memory<T> source, Action<T> action)
        {
            ForEach(source.Span, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEach<T>(this Span<T> source, Action<T> action)
        {
            ForEach((ReadOnlySpan<T>) source, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEach<T>(this ReadOnlyMemory<T> source, Action<T> action)
        {
            return ForEach(source.Span, action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEach<T>(this ReadOnlySpan<T> source, Action<T> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                action(item);
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachWhere<T>(this Memory<T> source, Func<T, Boolean> where, Action<T> action)
        {
            ForEachWhere(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachWhere<T>(this Span<T> source, Func<T, Boolean> where, Action<T> action)
        {
            ForEachWhere((ReadOnlySpan<T>) source, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachWhere<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> where, Action<T> action)
        {
            ForEachWhere(source.Span, where, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEachWhere<T>(this ReadOnlySpan<T> source, Func<T, Boolean> where, Action<T> action)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                if (where(item))
                {
                    action(item);
                }
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachWhere<T>(this Memory<T> source, Func<T, Int32, Boolean> where, Action<T> action)
        {
            ForEachWhere(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachWhere<T>(this Span<T> source, Func<T, Int32, Boolean> where, Action<T> action)
        {
            ForEachWhere((ReadOnlySpan<T>) source, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachWhere<T>(this ReadOnlyMemory<T> source, Func<T, Int32, Boolean> where, Action<T> action)
        {
            ForEachWhere(source.Span, where, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEachWhere<T>(this ReadOnlySpan<T> source, Func<T, Int32, Boolean> where, Action<T> action)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (where(item, index))
                {
                    action(item);
                }

                ++index;
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachWhereNot<T>(this Memory<T> source, Func<T, Boolean> where, Action<T> action)
        {
            ForEachWhereNot(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachWhereNot<T>(this Span<T> source, Func<T, Boolean> where, Action<T> action)
        {
            ForEachWhereNot((ReadOnlySpan<T>) source, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachWhereNot<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> where, Action<T> action)
        {
            ForEachWhereNot(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEachWhereNot<T>(this ReadOnlySpan<T> source, Func<T, Boolean> where, Action<T> action)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachWhere(source, item => !where(item), action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachWhereNot<T>(this Memory<T> source, Func<T, Int32, Boolean> where, Action<T> action)
        {
            ForEachWhereNot(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachWhereNot<T>(this Span<T> source, Func<T, Int32, Boolean> where, Action<T> action)
        {
            ForEachWhereNot((ReadOnlySpan<T>) source, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachWhereNot<T>(this ReadOnlyMemory<T> source, Func<T, Int32, Boolean> where, Action<T> action)
        {
            ForEachWhereNot(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEachWhereNot<T>(this ReadOnlySpan<T> source, Func<T, Int32, Boolean> where, Action<T> action)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachWhere(source, (item, index) => !where(item, index), action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEach<T>(this Memory<T> source, Action<T, Int32> action)
        {
            ForEach(source.Span, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEach<T>(this Span<T> source, Action<T, Int32> action)
        {
            ForEach((ReadOnlySpan<T>) source, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEach<T>(this ReadOnlyMemory<T> source, Action<T, Int32> action)
        {
            ForEach(source.Span, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEach<T>(this ReadOnlySpan<T> source, Action<T, Int32> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                action(item, index);
                ++index;
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachWhere<T>(this Memory<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhere(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachWhere<T>(this Span<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhere((ReadOnlySpan<T>) source, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachWhere<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhere(source.Span, where, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEachWhere<T>(this ReadOnlySpan<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (where(item))
                {
                    action(item, index);
                }

                ++index;
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachWhere<T>(this Memory<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhere(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachWhere<T>(this Span<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhere((ReadOnlySpan<T>) source, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachWhere<T>(this ReadOnlyMemory<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhere(source.Span, where, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEachWhere<T>(this ReadOnlySpan<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (where(item, index))
                {
                    action(item, index);
                }

                ++index;
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachWhereNot<T>(this Memory<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhereNot(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachWhereNot<T>(this Span<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhereNot((ReadOnlySpan<T>) source, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachWhereNot<T>(this ReadOnlyMemory<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhereNot(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEachWhereNot<T>(this ReadOnlySpan<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachWhere(source, item => !where(item), action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachWhereNot<T>(this Memory<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhereNot(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachWhereNot<T>(this Span<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhereNot((ReadOnlySpan<T>) source, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachWhereNot<T>(this ReadOnlyMemory<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
        {
            ForEachWhereNot(source.Span, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEachWhereNot<T>(this ReadOnlySpan<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachWhere(source, (item, index) => !where(item, index), action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Action<TKey> action)
        {
            ForEachBy(source.Span, selector, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachBy<T, TKey>(this Span<T> source, Func<T, TKey> selector, Action<TKey> action)
        {
            ForEachBy((ReadOnlySpan<T>) source, selector, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Action<TKey> action)
        {
            ForEachBy(source.Span, selector, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEachBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Action<TKey> action)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                action(selector(item));
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachByWhere<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
        {
            ForEachByWhere(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachByWhere<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
        {
            ForEachByWhere((ReadOnlySpan<T>) source, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachByWhere<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
        {
            ForEachByWhere(source.Span, selector, where, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEachByWhere<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                TKey select = selector(item);
                if (where(select))
                {
                    action(select);
                }
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachByWhere<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            ForEachByWhere(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachByWhere<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            ForEachByWhere((ReadOnlySpan<T>) source, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachByWhere<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            ForEachByWhere(source.Span, selector, where, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEachByWhere<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                TKey select = selector(item);
                if (where(select, index))
                {
                    action(select);
                }

                ++index;
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachByWhereNot<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
        {
            ForEachByWhereNot(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachByWhereNot<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
        {
            ForEachByWhereNot((ReadOnlySpan<T>) source, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachByWhereNot<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
        {
            ForEachByWhereNot(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEachByWhereNot<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachByWhere(source, selector, item => !where(item), action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachByWhereNot<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            ForEachByWhereNot(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachByWhereNot<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            ForEachByWhereNot((ReadOnlySpan<T>) source, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachByWhereNot<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            ForEachByWhereNot(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEachByWhereNot<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachByWhere(source, selector, (item, index) => !where(item, index), action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Action<TKey, Int32> action)
        {
            ForEachBy(source.Span, selector, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachBy<T, TKey>(this Span<T> source, Func<T, TKey> selector, Action<TKey, Int32> action)
        {
            ForEachBy((ReadOnlySpan<T>) source, selector, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Action<TKey, Int32> action)
        {
            ForEachBy(source.Span, selector, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEachBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Action<TKey, Int32> action)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                action(selector(item), index);
                ++index;
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachByWhere<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhere(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachByWhere<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhere((ReadOnlySpan<T>) source, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachByWhere<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhere(source.Span, selector, where, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEachByWhere<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                TKey select = selector(item);
                if (where(select))
                {
                    action(select, index);
                }

                ++index;
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachByWhere<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhere(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachByWhere<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhere((ReadOnlySpan<T>) source, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachByWhere<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhere(source.Span, selector, where, action);
            return source;
        }

        public static ReadOnlySpan<T> ForEachByWhere<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                TKey select = selector(item);
                if (where(select, index))
                {
                    action(select, index);
                }

                ++index;
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachByWhereNot<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhereNot(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachByWhereNot<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhereNot((ReadOnlySpan<T>) source, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachByWhereNot<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhereNot(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEachByWhereNot<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachByWhere(source, selector, item => !where(item), action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachByWhereNot<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhereNot(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachByWhereNot<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhereNot((ReadOnlySpan<T>) source, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachByWhereNot<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            ForEachByWhereNot(source.Span, selector, where, action);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEachByWhereNot<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachByWhere(source, selector, (item, index) => !where(item, index), action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachEvery<T>(this Memory<T> source, Action<T> action, Int32 every)
        {
            ForEachEvery(source.Span, action, every);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachEvery<T>(this Span<T> source, Action<T> action, Int32 every)
        {
            ForEachEvery((ReadOnlySpan<T>) source, action, every);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachEvery<T>(this ReadOnlyMemory<T> source, Action<T> action, Int32 every)
        {
            ForEachEvery(source.Span, action, every);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEachEvery<T>(this ReadOnlySpan<T> source, Action<T> action, Int32 every)
        {
            return ForEachEvery(source, action, every, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> ForEachEvery<T>(this Memory<T> source, Action<T> action, Int32 every, Boolean first)
        {
            ForEachEvery(source.Span, action, every, first);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEachEvery<T>(this Span<T> source, Action<T> action, Int32 every, Boolean first)
        {
            ForEachEvery((ReadOnlySpan<T>) source, action, every, first);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> ForEachEvery<T>(this ReadOnlyMemory<T> source, Action<T> action, Int32 every, Boolean first)
        {
            ForEachEvery(source.Span, action, every, first);
            return source;
        }

        public static ReadOnlySpan<T> ForEachEvery<T>(this ReadOnlySpan<T> source, Action<T> action, Int32 every, Boolean first)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (every <= 1)
            {
                return ForEach(source, action);
            }

            Int32 counter = first ? 0 : 1;
            return source.ForEachWhere(_ => counter++ % every == 0, action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? TryGetValue<T>(this Memory<T> source, Int32 index)
        {
            return TryGetValue(source.Span, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? TryGetValue<T>(this Span<T> source, Int32 index)
        {
            return TryGetValue((ReadOnlySpan<T>) source, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? TryGetValue<T>(this ReadOnlyMemory<T> source, Int32 index)
        {
            return TryGetValue(source.Span, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? TryGetValue<T>(this ReadOnlySpan<T> source, Int32 index)
        {
            return TryGetValue(source, index, out T? value) ? value : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TryGetValue<T>(this Memory<T> source, Int32 index, T alternate)
        {
            return TryGetValue(source.Span, index, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TryGetValue<T>(this Span<T> source, Int32 index, T alternate)
        {
            return TryGetValue((ReadOnlySpan<T>) source, index, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TryGetValue<T>(this ReadOnlyMemory<T> source, Int32 index, T alternate)
        {
            return TryGetValue(source.Span, index, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TryGetValue<T>(this ReadOnlySpan<T> source, Int32 index, T alternate)
        {
            return TryGetValue(source, index, out T? value) ? value : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TryGetValue<T>(this Memory<T> source, Int32 index, Func<T> alternate)
        {
            return TryGetValue(source.Span, index, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TryGetValue<T>(this Span<T> source, Int32 index, Func<T> alternate)
        {
            return TryGetValue((ReadOnlySpan<T>) source, index, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TryGetValue<T>(this ReadOnlyMemory<T> source, Int32 index, Func<T> alternate)
        {
            return TryGetValue(source.Span, index, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T TryGetValue<T>(this ReadOnlySpan<T> source, Int32 index, Func<T> alternate)
        {
            TryGetValue(source, index, alternate, out T value);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this Memory<T> source, Int32 index, [MaybeNullWhen(false)] out T value)
        {
            return TryGetValue(source.Span, index, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this Span<T> source, Int32 index, [MaybeNullWhen(false)] out T value)
        {
            return TryGetValue((ReadOnlySpan<T>) source, index, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this ReadOnlyMemory<T> source, Int32 index, [MaybeNullWhen(false)] out T value)
        {
            return TryGetValue(source.Span, index, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this ReadOnlySpan<T> source, Int32 index, [MaybeNullWhen(false)] out T value)
        {
            return TryGetValue(source!, index, default(T), out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this Memory<T> source, Int32 index, T alternate, out T value)
        {
            return TryGetValue(source.Span, index, alternate, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this Span<T> source, Int32 index, T alternate, out T value)
        {
            return TryGetValue((ReadOnlySpan<T>) source, index, alternate, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this ReadOnlyMemory<T> source, Int32 index, T alternate, out T value)
        {
            return TryGetValue(source.Span, index, alternate, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this ReadOnlySpan<T> source, Int32 index, T alternate, out T value)
        {
            if (index < 0 || index >= source.Length)
            {
                value = alternate;
                return false;
            }

            value = source[index];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this Memory<T> source, Int32 index, Func<T> alternate, out T value)
        {
            return TryGetValue(source.Span, index, alternate, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this Span<T> source, Int32 index, Func<T> alternate, out T value)
        {
            return TryGetValue((ReadOnlySpan<T>) source, index, alternate, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this ReadOnlyMemory<T> source, Int32 index, Func<T> alternate, out T value)
        {
            return TryGetValue(source.Span, index, alternate, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<T>(this ReadOnlySpan<T> source, Int32 index, Func<T> alternate, out T value)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            if (index < 0 || index >= source.Length)
            {
                value = alternate();
                return false;
            }

            value = source[index];
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandom<T>(this Memory<T> source)
        {
            return GetRandom(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandom<T>(this Span<T> source)
        {
            return GetRandom((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandom<T>(this ReadOnlyMemory<T> source)
        {
            return GetRandom(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandom<T>(this ReadOnlySpan<T> source)
        {
            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomOrDefault<T>(this Memory<T> source, T alternate)
        {
            return GetRandomOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomOrDefault<T>(this Span<T> source, T alternate)
        {
            return GetRandomOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomOrDefault<T>(this ReadOnlyMemory<T> source, T alternate)
        {
            return GetRandomOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomOrDefault<T>(this ReadOnlySpan<T> source, T alternate)
        {
            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomOrDefault<T>(this Memory<T> source, Func<T> alternate)
        {
            return GetRandomOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomOrDefault<T>(this Span<T> source, Func<T> alternate)
        {
            return GetRandomOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomOrDefault<T>(this ReadOnlyMemory<T> source, Func<T> alternate)
        {
            return GetRandomOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetRandomOrDefault<T>(this ReadOnlySpan<T> source, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : alternate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetRandomOrDefault<T>(this Memory<T> source)
        {
            return GetRandomOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetRandomOrDefault<T>(this Span<T> source)
        {
            return GetRandomOrDefault((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetRandomOrDefault<T>(this ReadOnlyMemory<T> source)
        {
            return GetRandomOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetRandomOrDefault<T>(this ReadOnlySpan<T> source)
        {
            return source.Length > 0 ? source[RandomUtilities.NextNonNegative(source.Length - 1)] : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Aggregate<T>(this Memory<T> source, Func<T, T, T> selector)
        {
            return Aggregate(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Aggregate<T>(this Span<T> source, Func<T, T, T> selector)
        {
            return Aggregate((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Aggregate<T>(this ReadOnlyMemory<T> source, Func<T, T, T> selector)
        {
            return Aggregate(source.Span, selector);
        }

        public static T Aggregate<T>(this ReadOnlySpan<T> source, Func<T, T, T> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (source.Length <= 0)
            {
                throw new InvalidOperationException();
            }

            T result = source[0];
            for (Int32 i = 1; i < source.Length; i++)
            {
                result = selector(result, source[i]);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Aggregate<T>(this Memory<T> source, T seed, Func<T, T, T> selector)
        {
            return Aggregate<T>(source.Span, seed, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Aggregate<T>(this Span<T> source, T seed, Func<T, T, T> selector)
        {
            return Aggregate<T>((ReadOnlySpan<T>) source, seed, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Aggregate<T>(this ReadOnlyMemory<T> source, T seed, Func<T, T, T> selector)
        {
            return Aggregate<T>(source.Span, seed, selector);
        }

        public static T Aggregate<T>(this ReadOnlySpan<T> source, T seed, Func<T, T, T> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T result = seed;
            foreach (T item in source)
            {
                result = selector(result, item);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TAccumulate Aggregate<T, TAccumulate>(this Memory<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> selector)
        {
            return Aggregate(source.Span, seed, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TAccumulate Aggregate<T, TAccumulate>(this Span<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> selector)
        {
            return Aggregate((ReadOnlySpan<T>) source, seed, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TAccumulate Aggregate<T, TAccumulate>(this ReadOnlyMemory<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> selector)
        {
            return Aggregate(source.Span, seed, selector);
        }

        public static TAccumulate Aggregate<T, TAccumulate>(this ReadOnlySpan<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            TAccumulate result = seed;
            foreach (T element in source)
            {
                result = selector(result, element);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Aggregate<T, TAccumulate, TResult>(this Memory<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> selector, Func<TAccumulate, TResult> result)
        {
            return Aggregate(source.Span, seed, selector, result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Aggregate<T, TAccumulate, TResult>(this Span<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> selector, Func<TAccumulate, TResult> result)
        {
            return Aggregate((ReadOnlySpan<T>) source, seed, selector, result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Aggregate<T, TAccumulate, TResult>(this ReadOnlyMemory<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> selector, Func<TAccumulate, TResult> result)
        {
            return Aggregate(source.Span, seed, selector, result);
        }

        public static TResult Aggregate<T, TAccumulate, TResult>(this ReadOnlySpan<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> selector, Func<TAccumulate, TResult> result)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            TAccumulate value = seed;
            foreach (T element in source)
            {
                value = selector(value, element);
            }

            return result(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> Shuffle<T>(this Memory<T> source)
        {
            return Shuffle(source, RandomUtilities.Generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> Shuffle<T>(this Memory<T> source, Random random)
        {
            Shuffle(source.Span, random);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> Shuffle<T>(this Memory<T> source, IRandom random)
        {
            Shuffle(source.Span, random);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Shuffle<T>(this Span<T> source)
        {
            return Shuffle(source, RandomUtilities.Generator);
        }

        public static Span<T> Shuffle<T>(this Span<T> source, Random random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < source.Length; i++)
            {
                Int32 j = random.Next(i, source.Length);
                (source[i], source[j]) = (source[j], source[i]);
            }

            return source;
        }

        public static Span<T> Shuffle<T>(this Span<T> source, IRandom random)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            for (Int32 i = 0; i < source.Length; i++)
            {
                Int32 j = random.Next(i, source.Length);
                (source[i], source[j]) = (source[j], source[i]);
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> Rotate<T>(this Memory<T> source)
        {
            return Rotate(source, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> Rotate<T>(this Memory<T> source, Int32 offset)
        {
            Rotate(source.Span, offset);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Rotate<T>(this Span<T> source)
        {
            return Rotate(source, 1);
        }

        public static Span<T> Rotate<T>(this Span<T> source, Int32 offset)
        {
            if (source.Length <= 1)
            {
                return source;
            }

            offset %= source.Length;

            switch (offset)
            {
                case 0:
                    return source;
                case < 0:
                    offset += source.Length;
                    break;
            }

            Span<T> buffer = new T[offset];
            source.Slice(source.Length - offset).CopyTo(buffer);
            source.Slice(0, source.Length - offset).CopyTo(source.Slice(offset));
            buffer.CopyTo(source.Slice(0, offset));

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Multiply(this Memory<BigInteger> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Multiply(this Span<BigInteger> source)
        {
            return Multiply((ReadOnlySpan<BigInteger>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Multiply(this ReadOnlyMemory<BigInteger> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Multiply(this ReadOnlySpan<BigInteger> source)
        {
            // ReSharper disable once RedundantOverflowCheckingContext
            checked
            {
                ReadOnlySpan<BigInteger>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == BigInteger.Zero)
                {
                    return BigInteger.Zero;
                }

                BigInteger result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current == BigInteger.Zero)
                    {
                        return BigInteger.Zero;
                    }

                    if (enumerator.Current == BigInteger.One)
                    {
                        continue;
                    }

                    result *= enumerator.Current;
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Multiply(this Memory<Complex> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Multiply(this Span<Complex> source)
        {
            return Multiply((ReadOnlySpan<Complex>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Multiply(this ReadOnlyMemory<Complex> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Multiply(this ReadOnlySpan<Complex> source)
        {
            // ReSharper disable once RedundantOverflowCheckingContext
            checked
            {
                ReadOnlySpan<Complex>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == Complex.Zero)
                {
                    return Complex.Zero;
                }

                Complex result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current == Complex.Zero)
                    {
                        return Complex.Zero;
                    }

                    if (enumerator.Current == Complex.One)
                    {
                        continue;
                    }

                    result *= enumerator.Current;
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Multiply(this Memory<Complex> source, Complex overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Multiply(this Span<Complex> source, Complex overflow)
        {
            return Multiply((ReadOnlySpan<Complex>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Multiply(this ReadOnlyMemory<Complex> source, Complex overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Multiply(this ReadOnlySpan<Complex> source, Complex overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this Memory<T> source)
        {
            return Max(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this Memory<T> source, IComparer<T>? comparer)
        {
            return Max(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this Memory<T> source)
        {
            return Min(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this Memory<T> source, IComparer<T>? comparer)
        {
            return Min(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this Span<T> source)
        {
            return Max((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this Span<T> source, IComparer<T>? comparer)
        {
            return Max((ReadOnlySpan<T>) source, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this Span<T> source)
        {
            return Min((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this Span<T> source, IComparer<T>? comparer)
        {
            return Min((ReadOnlySpan<T>) source, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this ReadOnlyMemory<T> source)
        {
            return Max(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this ReadOnlyMemory<T> source, IComparer<T>? comparer)
        {
            return Max(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this ReadOnlyMemory<T> source)
        {
            return Min(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this ReadOnlyMemory<T> source, IComparer<T>? comparer)
        {
            return Min(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this ReadOnlySpan<T> source)
        {
            return Max(source, null);
        }

        public static T Max<T>(this ReadOnlySpan<T> source, IComparer<T>? comparer)
        {
            switch (source.Length)
            {
                case <= 0:
                    throw new InvalidOperationException();
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<T>.Default;

            T max = source[0];

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                if (comparer.Compare(max, item) >= 0)
                {
                    continue;
                }

                max = item;
            }

            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this ReadOnlySpan<T> source)
        {
            return Min(source, null);
        }

        public static T Min<T>(this ReadOnlySpan<T> source, IComparer<T>? comparer)
        {
            switch (source.Length)
            {
                case <= 0:
                    throw new InvalidOperationException();
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<T>.Default;

            T min = source[0];

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                if (comparer.Compare(min, item) <= 0)
                {
                    continue;
                }

                min = item;
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxOrDefault<T>(this Memory<T> source)
        {
            return MaxOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxOrDefault<T>(this Memory<T> source, IComparer<T>? comparer)
        {
            return MaxOrDefault(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinOrDefault<T>(this Memory<T> source)
        {
            return MinOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinOrDefault<T>(this Memory<T> source, IComparer<T>? comparer)
        {
            return MinOrDefault(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxOrDefault<T>(this Span<T> source)
        {
            return MaxOrDefault((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxOrDefault<T>(this Span<T> source, IComparer<T>? comparer)
        {
            return MaxOrDefault((ReadOnlySpan<T>) source, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinOrDefault<T>(this Span<T> source)
        {
            return MinOrDefault((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinOrDefault<T>(this Span<T> source, IComparer<T>? comparer)
        {
            return MinOrDefault((ReadOnlySpan<T>) source, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxOrDefault<T>(this ReadOnlyMemory<T> source)
        {
            return MaxOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxOrDefault<T>(this ReadOnlyMemory<T> source, IComparer<T>? comparer)
        {
            return MaxOrDefault(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinOrDefault<T>(this ReadOnlyMemory<T> source)
        {
            return MinOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinOrDefault<T>(this ReadOnlyMemory<T> source, IComparer<T>? comparer)
        {
            return MinOrDefault(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxOrDefault<T>(this ReadOnlySpan<T> source)
        {
            return MaxOrDefault(source, (IComparer<T>?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxOrDefault<T>(this ReadOnlySpan<T> source, IComparer<T>? comparer)
        {
            return MaxOrDefault(source!, default(T), comparer!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinOrDefault<T>(this ReadOnlySpan<T> source)
        {
            return MinOrDefault(source, (IComparer<T>?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinOrDefault<T>(this ReadOnlySpan<T> source, IComparer<T>? comparer)
        {
            return MinOrDefault(source!, default(T), comparer!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this Memory<T> source, T alternate)
        {
            return MaxOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this Memory<T> source, T alternate, IComparer<T>? comparer)
        {
            return MaxOrDefault(source.Span, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this Memory<T> source, T alternate)
        {
            return MinOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this Memory<T> source, T alternate, IComparer<T>? comparer)
        {
            return MinOrDefault(source.Span, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this Span<T> source, T alternate)
        {
            return MaxOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this Span<T> source, T alternate, IComparer<T>? comparer)
        {
            return MaxOrDefault((ReadOnlySpan<T>) source, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this Span<T> source, T alternate)
        {
            return MinOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this Span<T> source, T alternate, IComparer<T>? comparer)
        {
            return MinOrDefault((ReadOnlySpan<T>) source, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this ReadOnlyMemory<T> source, T alternate)
        {
            return MaxOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this ReadOnlyMemory<T> source, T alternate, IComparer<T>? comparer)
        {
            return MaxOrDefault(source.Span, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this ReadOnlyMemory<T> source, T alternate)
        {
            return MinOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this ReadOnlyMemory<T> source, T alternate, IComparer<T>? comparer)
        {
            return MinOrDefault(source.Span, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this ReadOnlySpan<T> source, T alternate)
        {
            return MaxOrDefault(source, alternate, null);
        }

        public static T MaxOrDefault<T>(this ReadOnlySpan<T> source, T alternate, IComparer<T>? comparer)
        {
            switch (source.Length)
            {
                case <= 0:
                    return alternate;
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<T>.Default;

            T max = source[0];

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                if (comparer.Compare(max, item) >= 0)
                {
                    continue;
                }

                max = item;
            }

            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this ReadOnlySpan<T> source, T alternate)
        {
            return MinOrDefault(source, alternate, null);
        }

        public static T MinOrDefault<T>(this ReadOnlySpan<T> source, T alternate, IComparer<T>? comparer)
        {
            switch (source.Length)
            {
                case <= 0:
                    return alternate;
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<T>.Default;

            T min = source[0];

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                if (comparer.Compare(min, item) <= 0)
                {
                    continue;
                }

                min = item;
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this Memory<T> source, Func<T> alternate)
        {
            return MaxOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this Memory<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            return MaxOrDefault(source.Span, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this Memory<T> source, Func<T> alternate)
        {
            return MinOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this Memory<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            return MinOrDefault(source.Span, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this Span<T> source, Func<T> alternate)
        {
            return MaxOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this Span<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            return MaxOrDefault((ReadOnlySpan<T>) source, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this Span<T> source, Func<T> alternate)
        {
            return MinOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this Span<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            return MinOrDefault((ReadOnlySpan<T>) source, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this ReadOnlyMemory<T> source, Func<T> alternate)
        {
            return MaxOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this ReadOnlyMemory<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            return MaxOrDefault(source.Span, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this ReadOnlyMemory<T> source, Func<T> alternate)
        {
            return MinOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this ReadOnlyMemory<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            return MinOrDefault(source.Span, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this ReadOnlySpan<T> source, Func<T> alternate)
        {
            return MaxOrDefault(source, alternate, null);
        }

        public static T MaxOrDefault<T>(this ReadOnlySpan<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            switch (source.Length)
            {
                case <= 0:
                    return alternate();
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<T>.Default;

            T max = source[0];

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                if (comparer.Compare(max, item) >= 0)
                {
                    continue;
                }

                max = item;
            }

            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this ReadOnlySpan<T> source, Func<T> alternate)
        {
            return MinOrDefault(source, alternate, null);
        }

        public static T MinOrDefault<T>(this ReadOnlySpan<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            switch (source.Length)
            {
                case <= 0:
                    return alternate();
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<T>.Default;

            T min = source[0];

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                if (comparer.Compare(min, item) <= 0)
                {
                    continue;
                }

                min = item;
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxBy(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MaxBy(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinBy(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinBy(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxBy<T, TKey>(this Span<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxBy((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxBy<T, TKey>(this Span<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MaxBy((ReadOnlySpan<T>) source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinBy<T, TKey>(this Span<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinBy((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinBy<T, TKey>(this Span<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinBy((ReadOnlySpan<T>) source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxBy(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MaxBy(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinBy(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinBy(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxBy(source, selector, null);
        }

        public static T MaxBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            switch (source.Length)
            {
                case <= 0:
                    throw new InvalidOperationException();
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<TKey>.Default;

            T max = source[0];
            TKey maxby = selector(max);

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                TKey itemby = selector(item);
                if (comparer.Compare(maxby, itemby) >= 0)
                {
                    continue;
                }

                max = item;
                maxby = itemby;
            }

            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinBy(source, selector, null);
        }

        public static T MinBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            switch (source.Length)
            {
                case <= 0:
                    throw new InvalidOperationException();
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<TKey>.Default;

            T min = source[0];
            TKey minby = selector(min);

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                TKey itemby = selector(item);
                if (comparer.Compare(minby, itemby) <= 0)
                {
                    continue;
                }

                min = item;
                minby = itemby;
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MaxByOrDefault(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinByOrDefault(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinByOrDefault(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MaxByOrDefault((ReadOnlySpan<T>) source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinByOrDefault((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinByOrDefault((ReadOnlySpan<T>) source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MaxByOrDefault(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinByOrDefault(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinByOrDefault(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault(source, selector, (IComparer<TKey>?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MaxByOrDefault(source!, selector!, default(T), comparer!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector) where TKey : IComparable<TKey>
        {
            return MinByOrDefault(source, selector, (IComparer<TKey>?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinByOrDefault(source!, selector!, default(T), comparer!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, T alternate) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            return MaxByOrDefault(source.Span, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, T alternate) where TKey : IComparable<TKey>
        {
            return MinByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            return MinByOrDefault(source.Span, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, T alternate) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault((ReadOnlySpan<T>) source, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            return MaxByOrDefault((ReadOnlySpan<T>) source, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, T alternate) where TKey : IComparable<TKey>
        {
            return MinByOrDefault((ReadOnlySpan<T>) source, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            return MinByOrDefault((ReadOnlySpan<T>) source, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, T alternate) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            return MaxByOrDefault(source.Span, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, T alternate) where TKey : IComparable<TKey>
        {
            return MinByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            return MinByOrDefault(source.Span, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, T alternate) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault(source, selector, alternate, null);
        }

        public static T MaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            switch (source.Length)
            {
                case <= 0:
                    return alternate;
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<TKey>.Default;

            T max = source[0];
            TKey maxby = selector(max);

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                TKey itemby = selector(item);
                if (comparer.Compare(maxby, itemby) >= 0)
                {
                    continue;
                }

                max = item;
                maxby = itemby;
            }

            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, T alternate) where TKey : IComparable<TKey>
        {
            return MinByOrDefault(source, selector, alternate, null);
        }

        public static T MinByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            switch (source.Length)
            {
                case <= 0:
                    return alternate;
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<TKey>.Default;

            T min = source[0];
            TKey minby = selector(min);

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                TKey itemby = selector(item);
                if (comparer.Compare(minby, itemby) <= 0)
                {
                    continue;
                }

                min = item;
                minby = itemby;
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<T> alternate) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            return MaxByOrDefault(source.Span, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<T> alternate) where TKey : IComparable<TKey>
        {
            return MinByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            return MinByOrDefault(source.Span, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<T>alternate) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault((ReadOnlySpan<T>) source, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            return MaxByOrDefault((ReadOnlySpan<T>) source, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<T> alternate) where TKey : IComparable<TKey>
        {
            return MinByOrDefault((ReadOnlySpan<T>) source, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            return MinByOrDefault((ReadOnlySpan<T>) source, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<T> alternate) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            return MaxByOrDefault(source.Span, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<T> alternate) where TKey : IComparable<TKey>
        {
            return MinByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            return MinByOrDefault(source.Span, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<T> alternate) where TKey : IComparable<TKey>
        {
            return MaxByOrDefault(source, selector, alternate, null);
        }

        public static T MaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            switch (source.Length)
            {
                case <= 0:
                    return alternate();
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<TKey>.Default;

            T max = source[0];
            TKey maxby = selector(max);

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                TKey itemby = selector(item);
                if (comparer.Compare(maxby, itemby) >= 0)
                {
                    continue;
                }

                max = item;
                maxby = itemby;
            }

            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<T> alternate) where TKey : IComparable<TKey>
        {
            return MinByOrDefault(source, selector, alternate, null);
        }

        public static T MinByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            switch (source.Length)
            {
                case <= 0:
                    return alternate();
                case 1:
                    return source[0];
            }

            comparer ??= Comparer<TKey>.Default;

            T min = source[0];
            TKey minby = selector(min);

            for (Int32 i = 1; i < source.Length; i++)
            {
                T item = source[i];
                TKey itemby = selector(item);
                if (comparer.Compare(minby, itemby) <= 0)
                {
                    continue;
                }

                min = item;
                minby = itemby;
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMax<T>(this Memory<T> source)
        {
            return MinMax(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMax<T>(this Span<T> source)
        {
            return MinMax((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMax<T>(this ReadOnlyMemory<T> source)
        {
            return MinMax(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMax<T>(this ReadOnlySpan<T> source)
        {
            return MinMax(source, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMax<T>(this Memory<T> source, IComparer<T>? comparer)
        {
            return MinMax(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMax<T>(this Span<T> source, IComparer<T>? comparer)
        {
            return MinMax((ReadOnlySpan<T>) source, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMax<T>(this ReadOnlyMemory<T> source, IComparer<T>? comparer)
        {
            return MinMax(source.Span, comparer);
        }

        public static (T Min, T Max) MinMax<T>(this ReadOnlySpan<T> source, IComparer<T>? comparer)
        {
            ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            comparer ??= Comparer<T>.Default;

            T current = enumerator.Current;

            T min = current;
            T max = current;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (comparer.Compare(current, min) < 0)
                {
                    min = current;
                }

                if (comparer.Compare(current, max) > 0)
                {
                    max = current;
                }
            }

            return (min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Memory<T> source)
        {
            return MinMaxOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Span<T> source)
        {
            return MinMaxOrDefault((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this ReadOnlyMemory<T> source)
        {
            return MinMaxOrDefault(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this ReadOnlySpan<T> source)
        {
            return MinMaxOrDefault(source, (IComparer<T>?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Memory<T> source, IComparer<T>? comparer)
        {
            return MinMaxOrDefault(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Span<T> source, IComparer<T>? comparer)
        {
            return MinMaxOrDefault((ReadOnlySpan<T>) source, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this ReadOnlyMemory<T> source, IComparer<T>? comparer)
        {
            return MinMaxOrDefault(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this ReadOnlySpan<T> source, IComparer<T>? comparer)
        {
            return MinMaxOrDefault(source!, default(T), comparer!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Memory<T> source, T alternate)
        {
            return MinMaxOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Span<T> source, T alternate)
        {
            return MinMaxOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this ReadOnlyMemory<T> source, T alternate)
        {
            return MinMaxOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxOrDefault<T>(this ReadOnlySpan<T> source, T alternate)
        {
            return MinMaxOrDefault(source, alternate, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Memory<T> source, T alternate, IComparer<T>? comparer)
        {
            return MinMaxOrDefault(source.Span, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Span<T> source, T alternate, IComparer<T>? comparer)
        {
            return MinMaxOrDefault((ReadOnlySpan<T>) source, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this ReadOnlyMemory<T> source, T alternate, IComparer<T>? comparer)
        {
            return MinMaxOrDefault(source.Span, alternate, comparer);
        }

        public static (T Min, T Max) MinMaxOrDefault<T>(this ReadOnlySpan<T> source, T alternate, IComparer<T>? comparer)
        {
            ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return (alternate, alternate);
            }

            comparer ??= Comparer<T>.Default;

            T current = enumerator.Current;

            T min = current;
            T max = current;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (comparer.Compare(current, min) < 0)
                {
                    min = current;
                }

                if (comparer.Compare(current, max) > 0)
                {
                    max = current;
                }
            }

            return (min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Memory<T> source, Func<T> alternate)
        {
            return MinMaxOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Span<T> source, Func<T> alternate)
        {
            return MinMaxOrDefault((ReadOnlySpan<T>) source, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this ReadOnlyMemory<T> source, Func<T> alternate)
        {
            return MinMaxOrDefault(source.Span, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxOrDefault<T>(this ReadOnlySpan<T> source, Func<T> alternate)
        {
            return MinMaxOrDefault(source, alternate, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Memory<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            return MinMaxOrDefault(source.Span, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this Span<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            return MinMaxOrDefault((ReadOnlySpan<T>) source, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxOrDefault<T>(this ReadOnlyMemory<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            return MinMaxOrDefault(source.Span, alternate, comparer);
        }

        public static (T Min, T Max) MinMaxOrDefault<T>(this ReadOnlySpan<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

            T current;

            if (!enumerator.MoveNext())
            {
                current = alternate();
                return (current, current);
            }

            comparer ??= Comparer<T>.Default;

            current = enumerator.Current;

            T min = current;
            T max = current;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (comparer.Compare(current, min) < 0)
                {
                    min = current;
                }

                if (comparer.Compare(current, max) > 0)
                {
                    max = current;
                }
            }

            return (min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector)
        {
            return MinMaxBy(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxBy<T, TKey>(this Span<T> source, Func<T, TKey> selector)
        {
            return MinMaxBy((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector)
        {
            return MinMaxBy(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector)
        {
            return MinMaxBy(source, selector, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxBy<T, TKey>(this Memory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinMaxBy(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxBy<T, TKey>(this Span<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinMaxBy((ReadOnlySpan<T>) source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxBy<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinMaxBy(source.Span, selector, comparer);
        }

        public static (T Min, T Max) MinMaxBy<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            comparer ??= Comparer<TKey>.Default;

            T current = enumerator.Current;

            T min = current;
            T max = current;
            TKey key = selector(current);
            TKey minkey = key;
            TKey maxkey = key;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
                key = selector(current);

                if (comparer.Compare(key, minkey) < 0)
                {
                    min = current;
                    minkey = selector(min);
                }

                if (comparer.Compare(key, maxkey) > 0)
                {
                    max = current;
                    maxkey = selector(max);
                }
            }

            return (min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector)
        {
            return MinMaxByOrDefault(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector)
        {
            return MinMaxByOrDefault((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector)
        {
            return MinMaxByOrDefault(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector)
        {
            return MinMaxByOrDefault(source, selector, (IComparer<TKey>?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault((ReadOnlySpan<T>) source, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault(source.Span, selector, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T? Min, T? Max) MinMaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault(source!, selector!, default(T), comparer!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, T alternate)
        {
            return MinMaxByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, T alternate)
        {
            return MinMaxByOrDefault((ReadOnlySpan<T>) source, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, T alternate)
        {
            return MinMaxByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, T alternate)
        {
            return MinMaxByOrDefault(source, selector, alternate, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault(source.Span, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault((ReadOnlySpan<T>) source, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault(source.Span, selector, alternate, comparer);
        }

        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return (alternate, alternate);
            }

            comparer ??= Comparer<TKey>.Default;

            T current = enumerator.Current;

            T min = current;
            T max = current;
            TKey key = selector(current);
            TKey minkey = key;
            TKey maxkey = key;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
                key = selector(current);

                if (comparer.Compare(key, minkey) < 0)
                {
                    min = current;
                    minkey = selector(min);
                }

                if (comparer.Compare(key, maxkey) > 0)
                {
                    max = current;
                    maxkey = selector(max);
                }
            }

            return (min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<T> alternate)
        {
            return MinMaxByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<T> alternate)
        {
            return MinMaxByOrDefault((ReadOnlySpan<T>) source, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<T> alternate)
        {
            return MinMaxByOrDefault(source.Span, selector, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<T> alternate)
        {
            return MinMaxByOrDefault(source, selector, alternate, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this Memory<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault(source.Span, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this Span<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault((ReadOnlySpan<T>) source, selector, alternate, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this ReadOnlyMemory<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault(source.Span, selector, alternate, comparer);
        }

        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this ReadOnlySpan<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            ReadOnlySpan<T>.Enumerator enumerator = source.GetEnumerator();

            T current;

            if (!enumerator.MoveNext())
            {
                current = alternate();
                return (current, current);
            }

            comparer ??= Comparer<TKey>.Default;

            current = enumerator.Current;

            T min = current;
            T max = current;
            TKey key = selector(current);
            TKey minkey = key;
            TKey maxkey = key;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
                key = selector(current);

                if (comparer.Compare(key, minkey) < 0)
                {
                    min = current;
                    minkey = selector(min);
                }

                if (comparer.Compare(key, maxkey) > 0)
                {
                    max = current;
                    maxkey = selector(max);
                }
            }

            return (min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this Memory<T> source)
        {
            return AllSame(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this Span<T> source)
        {
            return AllSame(source, EqualityComparer<T>.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this Memory<T> source, IEqualityComparer<T>? comparer)
        {
            return AllSame(source.Span, comparer);
        }

        public static Boolean AllSame<T>(this Span<T> source, IEqualityComparer<T>? comparer)
        {
            return AllSame((ReadOnlySpan<T>) source, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this ReadOnlyMemory<T> source)
        {
            return AllSame(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this ReadOnlySpan<T> source)
        {
            return AllSame(source, EqualityComparer<T>.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this ReadOnlyMemory<T> source, IEqualityComparer<T>? comparer)
        {
            return AllSame(source.Span, comparer);
        }

        public static Boolean AllSame<T>(this ReadOnlySpan<T> source, IEqualityComparer<T>? comparer)
        {
            if (source.Length <= 0)
            {
                return true;
            }

            comparer ??= EqualityComparer<T>.Default;

            T first = source[0];
            return source.All(item => comparer.Equals(first, item));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDistinct<T>(this Memory<T> source)
        {
            return IsDistinct(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDistinct<T>(this Span<T> source)
        {
            return IsDistinct((ReadOnlySpan<T>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDistinct<T>(this ReadOnlyMemory<T> source)
        {
            return IsDistinct(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDistinct<T>(this ReadOnlySpan<T> source)
        {
            return IsDistinct(source, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDistinct<T>(this Memory<T> source, IEqualityComparer<T>? comparer)
        {
            return IsDistinct(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDistinct<T>(this Span<T> source, IEqualityComparer<T>? comparer)
        {
            return IsDistinct((ReadOnlySpan<T>) source, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDistinct<T>(this ReadOnlyMemory<T> source, IEqualityComparer<T>? comparer)
        {
            return IsDistinct(source.Span, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDistinct<T>(this ReadOnlySpan<T> source, IEqualityComparer<T>? comparer)
        {
            HashSet<T> set = new HashSet<T>(comparer);
            return source.All(item => set.Add(item));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> Skip<T>(this Memory<T> source, Int32 count)
        {
            return count < source.Length ? count > 0 ? source.Slice(count) : source : Memory<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Skip<T>(this Span<T> source, Int32 count)
        {
            return count < source.Length ? count > 0 ? source.Slice(count) : source : Span<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> Skip<T>(this ReadOnlyMemory<T> source, Int32 count)
        {
            return count < source.Length ? count > 0 ? source.Slice(count) : source : ReadOnlyMemory<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> Skip<T>(this ReadOnlySpan<T> source, Int32 count)
        {
            return count < source.Length ? count > 0 ? source.Slice(count) : source : ReadOnlySpan<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<T> Take<T>(this Memory<T> source, Int32 count)
        {
            return count > 0 ? source.Slice(0, Math.Min(count, source.Length)) : Memory<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Take<T>(this Span<T> source, Int32 count)
        {
            return count > 0 ? source.Slice(0, Math.Min(count, source.Length)) : Span<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> Take<T>(this ReadOnlyMemory<T> source, Int32 count)
        {
            return count > 0 ? source.Slice(0, Math.Min(count, source.Length)) : ReadOnlyMemory<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> Take<T>(this ReadOnlySpan<T> source, Int32 count)
        {
            return count > 0 ? source.Slice(0, Math.Min(count, source.Length)) : ReadOnlySpan<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InBounds<T>(this Memory<T> source, Int32 index)
        {
            return index >= 0 && index < source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InBounds<T>(this Span<T> source, Int32 index)
        {
            return index >= 0 && index < source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InBounds<T>(this ReadOnlyMemory<T> source, Int32 index)
        {
            return index >= 0 && index < source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InBounds<T>(this ReadOnlySpan<T> source, Int32 index)
        {
            return index >= 0 && index < source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> AsEnumerable<T>(this Memory<T> source)
        {
            for (Int32 i = 0; i < source.Length; i++)
            {
                yield return source.Span[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> AsEnumerable<T>(this ReadOnlyMemory<T> source)
        {
            for (Int32 i = 0; i < source.Length; i++)
            {
                yield return source.Span[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T>.Enumerator GetEnumerator<T>(this Memory<T> source)
        {
            return source.Span.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T>.Enumerator GetEnumerator<T>(this ReadOnlyMemory<T> source)
        {
            return source.Span.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Mutate<T>(this ReadOnlyMemory<T> source) where T : unmanaged
        {
            return source.Span.Mutate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe Span<T> Mutate<T>(this ReadOnlySpan<T> source) where T : unmanaged
        {
            fixed (T* pointer = source)
            {
                return new Span<T>(pointer, source.Length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanMutationEnumerator GetMutationEnumerator(this Memory<Byte> source)
        {
            return new SpanMutationEnumerator(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanMutationEnumerator GetMutationEnumerator(this Span<Byte> source)
        {
            return new SpanMutationEnumerator(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanMutationEnumerator<T> GetMutationEnumerator<T>(this Memory<T> source)
        {
            return new SpanMutationEnumerator<T>(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanMutationEnumerator<T> GetMutationEnumerator<T>(this Span<T> source)
        {
            return new SpanMutationEnumerator<T>(source);
        }
    }
}