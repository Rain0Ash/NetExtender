using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using NetExtender.Types.Strings.Interfaces;

#if NET6_0_OR_GREATER
using System.Runtime.InteropServices;
#endif

namespace NetExtender.Types.Spans
{
    public readonly ref struct SpanOwner<T>
    {
        public enum Allocation
        {
            Default,
            Clear
        }

        public static SpanOwner<T> Empty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new SpanOwner<T>(ArrayPool<T>.Shared, 0, Allocation.Default);
            }
        }

        private readonly ArrayPool<T> _pool;
        private readonly T[] _array;

        public Int32 Length { get; }

        public Span<T> Span
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Span<T>(_array, 0, Length);
            }
        }

        public ArraySegment<T> Array
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new ArraySegment<T>(_array, 0, Length);
            }
        }

        public ref T Reference
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref MemoryMarshal.GetArrayDataReference(_array);
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return _pool is null || Length <= 0;
            }
        }

        private SpanOwner(ArrayPool<T> pool, Int32 length, Allocation mode)
        {
            _pool = pool;
            Length = length;
            _array = pool.Rent(length);

            if (mode is Allocation.Clear)
            {
                _array.AsSpan(0, length).Clear();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanOwner<T> Allocate(Int32 size)
        {
            return new SpanOwner<T>(ArrayPool<T>.Shared, size, Allocation.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanOwner<T> Allocate(ArrayPool<T> pool, Int32 size)
        {
            return new SpanOwner<T>(pool, size, Allocation.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanOwner<T> Allocate(Int32 size, Allocation mode)
        {
            return new SpanOwner<T>(ArrayPool<T>.Shared, size, mode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpanOwner<T> Allocate(ArrayPool<T> pool, Int32 size, Allocation mode)
        {
            return new SpanOwner<T>(pool, size, mode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            _pool.Return(_array, true);
        }

        public override String ToString()
        {
            if (typeof(T) == typeof(Char) && _array is Char[] array)
            {
                return new String(array, 0, Length);
            }

            return IStringProvider.Default.GetString(Array)!;
        }
    }
}