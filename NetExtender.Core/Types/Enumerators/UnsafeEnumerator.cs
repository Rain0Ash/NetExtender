// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Types.Enumerators.Interfaces;

namespace NetExtender.Types.Enumerators
{
    [SuppressMessage("ReSharper", "StructCanBeMadeReadOnly")]
    [SuppressMessage("ReSharper", "StructMemberCanBeMadeReadOnly")]
    public unsafe struct UnsafeEnumerator<T, TSize> : IUnsafeEnumerator<T>, IStruct<UnsafeEnumerator<T, TSize>> where T : struct where TSize : struct, IUnsafeSize<TSize>
    {
        private TSize Internal;

        public Type Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            get
            {
                fixed (void* pointer = Internal)
                {
                    return _type(pointer);
                }
            }
        }

        public readonly Int32 Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Length;
            }
        }

        public Int32 Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
                return Internal.Count;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private init
            {
                Internal.Count = value;
            }
        }
        
        public readonly Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsEmpty;
            }
        }

        public readonly T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            get
            {
                fixed (void* pointer = Internal)
                {
                    return _current(pointer);
                }
            }
        }

        readonly Object IEnumerator.Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Current;
            }
        }
        
        private readonly delegate*<void*, Type> _type;
        private readonly delegate*<void*, T> _current;
        private readonly delegate*<void*, Boolean> _next;
        private readonly delegate*<void*, void> _reset;
        private readonly delegate*<void*, void> _dispose;

        public UnsafeEnumerator(Span<Byte> value, delegate*<void*, Type> type, delegate*<void*, T> current, delegate*<void*, Boolean> next, delegate*<void*, void> reset, delegate*<void*, void> dispose)
        {
            Internal = default;
            if (value.Length > Internal.Length || !value.TryCopyTo(Internal.AsSpan()))
            {
                throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Internal.Length}");
            }
            
            Internal.Count = value.Length;
            _type = type;
            _current = current;
            _next = next;
            _reset = reset;
            _dispose = dispose;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Boolean MoveNext()
        {
            fixed (void* pointer = Internal.AsSpan())
            {
                return _next(pointer);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void Reset()
        {
            fixed (void* pointer = Internal.AsSpan())
            {
                _reset(pointer);
            }
        }

        public readonly UnsafeEnumerator<T, TSize> GetEnumerator()
        {
            UnsafeEnumerator<T, TSize> enumerator = this;
            enumerator.Reset();
            return enumerator;
        }

        [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
        readonly IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (T item in this)
            {
                yield return item;
            }
        }

        readonly IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ref Byte GetPinnableReference()
        {
            fixed (Byte* pointer = this)
            {
                return ref *pointer;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void Dispose()
        {
            fixed (void* pointer = Internal.AsSpan())
            {
                _dispose(pointer);
            }
        }
    }
}