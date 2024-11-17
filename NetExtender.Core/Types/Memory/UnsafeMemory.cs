using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetExtender.Types.Memory
{
    public sealed unsafe class UnsafeMemory<T> : MemoryManager<T> where T : unmanaged
    {
        public static implicit operator ReadOnlyMemory<T>(UnsafeMemory<T>? value)
        {
            return value?.Memory ?? default;
        }
        
        public static implicit operator Memory<T>(UnsafeMemory<T>? value)
        {
            return value?.Memory ?? default;
        }
        
        private readonly void* _pointer;
        private readonly Int32 _length;

        private UnsafeMemory(void* pointer, Int32 length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            _pointer = pointer;
            _length = length;
        }

        public UnsafeMemory(IntPtr pointer, Int32 length)
            : this(pointer.ToPointer(), length)
        {
        }

        [CLSCompliant(false)]
        public UnsafeMemory(T* pointer, Int32 length)
            : this((void*) pointer, length)
        {
        }

        public UnsafeMemory(ReadOnlySpan<T> span)
        {
            _pointer = Unsafe.AsPointer(ref MemoryMarshal.GetReference(span));
            _length = span.Length;
        }

        public UnsafeMemory(Span<T> span)
        {
            _pointer = Unsafe.AsPointer(ref MemoryMarshal.GetReference(span));
            _length = span.Length;
        }

        public override Span<T> GetSpan()
        {
            return new Span<T>(_pointer, _length);
        }

        public override MemoryHandle Pin(Int32 index = 0)
        {
            if (index < 0 || index >= _length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return new MemoryHandle(Unsafe.Add<T>(_pointer, index));
        }

        public override void Unpin()
        {
        }

        protected override void Dispose(Boolean disposing)
        {
        }
    }
}