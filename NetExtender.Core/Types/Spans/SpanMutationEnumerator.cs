// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetExtender.Types.Spans
{
    public readonly ref struct SpanMutationReference<T>
    {
        public static implicit operator T(SpanMutationReference<T> value)
        {
            return value.Value;
        }

        private Span<T> Internal { get; }
        private Int32 Index { get; }
        
        public ref T Value
        {
            get
            {
                return ref Internal[Index];
            }
        }

        internal SpanMutationReference(Span<T> span, Int32 index)
        {
            Internal = span;
            Index = index;
        }
    }

    public ref struct SpanMutationEnumerator<T>
    {
        private Span<T> Internal { get; }
        private Int32 Index { get; set; }
        
        public readonly SpanMutationReference<T> Current
        {
            get
            {
                return new SpanMutationReference<T>(Internal, Index);
            }
        }

        internal SpanMutationEnumerator(Span<T> span)
        {
            Internal = span;
            Index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MoveNext()
        {
            return MoveNext(1);
        }

        public Boolean MoveNext(Int32 count)
        {
            Int64 index = Index + count;
            if (index < 0 || index >= Internal.Length)
            {
                return false;
            }
            
            Index += count;
            return true;
        }
        
        internal Boolean MoveNext(Int32 count, out Span<T> result)
        {
            if (count < 0 || count >= Internal.Length - Index)
            {
                result = default;
                return false;
            }
            
            result = Internal.Slice(Index, count);
            return MoveNext(count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MovePrevious()
        {
            return MovePrevious(1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MovePrevious(Int32 count)
        {
            return count > Int32.MinValue && MoveNext(-count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            Index = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly SpanMutationEnumerator<T> GetEnumerator()
        {
            return this;
        }
    }
    
    public readonly ref struct SpanMutationEnumerator
    {
        private SpanMutationEnumerator<Byte> Internal { get; }

        public SpanMutationReference<Byte> Current
        {
            get
            {
                return Internal.Current;
            }
        }

        internal SpanMutationEnumerator(Span<Byte> span)
        {
            Internal = new SpanMutationEnumerator<Byte>(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MoveNext()
        {
            return Internal.MoveNext();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MoveNext(Int32 count)
        {
            return Internal.MoveNext(count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MoveNext(Int32 count, out Span<Byte> result)
        {
            return Internal.MoveNext(count, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MoveNext<T>(out T value) where T : struct
        {
            if (!Internal.MoveNext(Unsafe.SizeOf<T>(), out Span<Byte> span))
            {
                value = default;
                return false;
            }
            
            value = MemoryMarshal.AsRef<T>(span);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MovePrevious()
        {
            return Internal.MovePrevious();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MovePrevious(Int32 count)
        {
            return Internal.MovePrevious(count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean MovePrevious<T>(out T value) where T : struct
        {
            return MoveNext(out value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            Internal.Reset();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanMutationEnumerator GetEnumerator()
        {
            return this;
        }
    }
}