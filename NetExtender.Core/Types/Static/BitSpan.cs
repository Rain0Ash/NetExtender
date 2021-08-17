// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NetExtender.Core.Types.Static
{
    public readonly unsafe ref struct BitSpan
    {
        public static implicit operator Span<Byte>(BitSpan span)
        {
            return span.Buffer;
        }

        /*public static implicit operator ReadOnlyBitSpan(BitSpan span)
        {
            return span.Buffer;
        }*/

        public static Boolean operator ==(BitSpan first, BitSpan second)
        {
            return first.Buffer == second.Buffer;
        }

        public static Boolean operator ==(BitSpan span, Span<Byte> buffer)
        {
            return span.Buffer == buffer;
        }

        public static Boolean operator ==(Span<Byte> buffer, BitSpan span)
        {
            return buffer == span.Buffer;
        }

        public static Boolean operator !=(BitSpan first, BitSpan second)
        {
            return first.Buffer != second.Buffer;
        }

        public static Boolean operator !=(BitSpan span, Span<Byte> buffer)
        {
            return span.Buffer != buffer;
        }

        public static Boolean operator !=(Span<Byte> buffer, BitSpan span)
        {
            return buffer != span.Buffer;
        }

        public static BitSpan operator !(BitSpan span)
        {
            return span.Clone().Not();
        }

        public static BitSpan operator |(BitSpan first, Boolean second)
        {
            return first.Clone().Or(second);
        }

        public static BitSpan operator |(BitSpan first, BitSpan second)
        {
            return first.Clone().Or(second);
        }

        public static BitSpan operator &(BitSpan first, Boolean second)
        {
            return first.Clone().And(second);
        }

        public static BitSpan operator &(BitSpan first, BitSpan second)
        {
            return first.Clone().And(second);
        }

        public static BitSpan operator ^(BitSpan first, Boolean second)
        {
            return first.Clone().Xor(second);
        }

        public static BitSpan operator ^(BitSpan first, BitSpan second)
        {
            return first.Clone().Xor(second);
        }

        public static BitSpan operator <<(BitSpan span, Int32 count)
        {
            return span.Clone().LeftShift(count);
        }

        public static BitSpan operator >>(BitSpan span, Int32 count)
        {
            return span.Clone().RightShift(count);
        }

        private Span<Byte> Buffer { get; }

        public Int32 Length { get; }

        public BitSpan(void* pointer, Int32 count)
            : this(pointer, count, count * 8)
        {
        }

        public BitSpan(void* pointer, Int32 count, Int32 length)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (length > count * 8)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (count == 0 || length <= 0)
            {
                Buffer = Span<Byte>.Empty;
                Length = 0;
                return;
            }

            Buffer = new Span<Byte>(pointer, count);
            Length = length;
        }

        public BitSpan(void* pointer, Int32 count, Boolean value)
            : this(pointer, count, count * 8, value)
        {
        }

        public BitSpan(void* pointer, Int32 count, Int32 length, Boolean value)
            : this(pointer, count, length)
        {
            SetAll(value);
        }

        public BitSpan(Span<Byte> buffer)
        {
            Buffer = buffer;
            Length = Buffer.Length * 8;
        }
        
        public BitSpan(Span<Byte> buffer, Int32 length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (length > buffer.Length * 8)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (length == 0)
            {
                Buffer = Span<Byte>.Empty;
            }

            Buffer = buffer;
            Length = length;
        }

        public BitSpan(Span<Byte> buffer, Boolean value)
            : this(buffer)
        {
            SetAll(value);
        }
        
        public BitSpan(Span<Byte> buffer, Int32 length, Boolean value)
            : this(buffer, length)
        {
            SetAll(value);
        }

        public BitSpan(Int32 length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            Length = length;
            Buffer = length > 0 ? new Byte[(Int32) Math.Ceiling(length / 8D)] : Span<Byte>.Empty;
        }

        public BitSpan(Int32 length, Boolean value)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            Length = length;
            Buffer = length > 0 ? new Byte[(Int32) Math.Ceiling(length / 8D)] : Span<Byte>.Empty;

            if (value)
            {
                SetAll(true);
            }
        }

        public BitSpan(Byte[] buffer)
            : this((Span<Byte>) buffer)
        {
        }
        
        public BitSpan(Byte[] buffer, Int32 length)
            : this((Span<Byte>) buffer, length)
        {
        }

        public BitSpan(Byte[] buffer, Boolean value)
            : this((Span<Byte>) buffer, value)
        {
        }
        
        public BitSpan(Byte[] buffer, Int32 length, Boolean value)
            : this((Span<Byte>) buffer, length, value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref Byte GetPinnableReference()
        {
            return ref Buffer.GetPinnableReference();
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public BitSpan Not()
        {
            unchecked
            {
                fixed (Byte* pointer = Buffer)
                {
                    for (Int32 i = 0; i < Buffer.Length; i++)
                    {
                        pointer[i] = (Byte) (~pointer[i]);
                    }
                }
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public BitSpan Or(Boolean value)
        {
            if (value)
            {
                Buffer.Fill(Byte.MaxValue);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public BitSpan Or(BitSpan value)
        {
            if (Length != value.Length)
            {
                throw new ArgumentException("Span lengths must be the same.", nameof(value));
            }

            unchecked
            {
                fixed (Byte* pointer = Buffer)
                fixed (Byte* second = value.Buffer)
                {
                    for (Int32 i = 0; i < Buffer.Length; i++)
                    {
                        pointer[i] = (Byte) (pointer[i] | second[i]);
                    }
                }
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public BitSpan And(Boolean value)
        {
            if (!value)
            {
                Buffer.Fill(Byte.MinValue);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public BitSpan And(BitSpan value)
        {
            if (Length != value.Length)
            {
                throw new ArgumentException("Span lengths must be the same.", nameof(value));
            }

            unchecked
            {
                fixed (Byte* pointer = Buffer)
                fixed (Byte* second = value.Buffer)
                {
                    for (Int32 i = 0; i < Buffer.Length; i++)
                    {
                        pointer[i] = (Byte) (pointer[i] & second[i]);
                    }
                }
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public BitSpan Xor(Boolean value)
        {
            Byte xor = value ? Byte.MaxValue : Byte.MinValue;

            unchecked
            {
                fixed (Byte* pointer = Buffer)
                {
                    for (Int32 i = 0; i < Buffer.Length; i++)
                    {
                        pointer[i] = (Byte) (pointer[i] ^ xor);
                    }
                }
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public BitSpan Xor(BitSpan value)
        {
            if (Length != value.Length)
            {
                throw new ArgumentException("Span lengths must be the same.", nameof(value));
            }

            unchecked
            {
                fixed (Byte* pointer = Buffer)
                fixed (Byte* second = value.Buffer)
                {
                    for (Int32 i = 0; i < Buffer.Length; i++)
                    {
                        pointer[i] = (Byte) (pointer[i] ^ second[i]);
                    }
                }
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public BitSpan LeftShift(Int32 count)
        {
            if (count < 0)
            {
                return RightShift(-count);
            }

            if (count == 0)
            {
                return this;
            }

            if (count >= Length)
            {
                return SetAll(false);
            }
            
            unchecked
            {
                fixed (Byte* pointer = Buffer)
                {
                    for (Int32 index = 0; index < Buffer.Length; index++)
                    {
                        Boolean carry = (pointer[index] & 0x80) > 0;

                        if (index <= 0)
                        {
                            continue;
                        }

                        if (carry)
                        {
                            pointer[index - 1] = (Byte) (pointer[index - 1] | 0x01);
                        }

                        pointer[index] = (Byte)(pointer[index] << 1);
                    }
                }
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public BitSpan RightShift(Int32 count)
        {
            if (count < 0)
            {
                return LeftShift(-count);
            }

            if (count == 0)
            {
                return this;
            }

            if (count >= Length)
            {
                return SetAll(false);
            }

            unchecked
            {
                fixed (Byte* pointer = Buffer)
                {
                    //TODO: To BitUtils
                    Int32 right = Buffer.Length - 1;

                    for (Int32 index = right; index >= 0; index--)
                    {
                        Boolean carry = (pointer[index] & 0x01) > 0;

                        if (index >= right)
                        {
                            continue;
                        }

                        if (carry)
                        {
                            pointer[index + 1] = (Byte) (pointer[index + 1] | 0x80);
                        }

                        pointer[index] = (Byte) (pointer[index] >> 1);
                    }
                }
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Get(Int32 index)
        {
            return this[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BitSpan Set(Int32 index, Boolean value)
        {
            this[index] = value;
            return this;
        }

        public BitSpan SetAll(Boolean value)
        {
            Buffer.Fill(value ? Byte.MaxValue : Byte.MinValue);
            return this;
        }

        public BitSpan Clone()
        {
            BitSpan clone = new BitSpan(Length);
            CopyTo(clone);

            return clone;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<Byte> destination)
        {
            Buffer.CopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public void CopyTo(Span<Boolean> destination)
        {
            if (Length > destination.Length)
            {
                throw new ArgumentException("Destination is too short.", nameof(destination));
            }

            if (Buffer.Length <= 0)
            {
                return;
            }

            unchecked
            {
                fixed (Byte* pointer = Buffer)
                {
                    Int32 length = Buffer.Length - 1;
                    for (Int32 i = 0; i < length; i++)
                    {
                        for (Int32 position = 0; position < 8; position++)
                        {
                            destination[i * 8 + position] = (pointer[i] & (1 << position)) != 0;
                        }
                    }

                    for (Int32 position = 0; position < Length % 8; position++)
                    {
                        destination[length + position] = (pointer[length] & (1 << position)) != 0;
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryCopyTo(Span<Byte> destination)
        {
            return Buffer.TryCopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryCopyTo(Span<Boolean> destination)
        {
            if (Length > destination.Length)
            {
                return false;
            }

            if (Buffer.Length <= 0)
            {
                return true;
            }

            unchecked
            {
                fixed (Byte* pointer = Buffer)
                {
                    Int32 length = Buffer.Length - 1;
                    for (Int32 i = 0; i < length; i++)
                    {
                        for (Int32 position = 0; position < 8; position++)
                        {
                            destination[i * 8 + position] = (pointer[i] & (1 << position)) != 0;
                        }
                    }

                    for (Int32 position = 0; position < Length % 8; position++)
                    {
                        destination[length + position] = (pointer[length] & (1 << position)) != 0;
                    }
                }
            }

            return true;
        }

        public Boolean this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                if (index < 0 || index >= Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                fixed (Byte* pointer = Buffer)
                {
                    return (pointer[index / 8] & (1 << (index % 8))) != 0;
                }
            }
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            set
            {
                if (index < 0 || index >= Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                unchecked
                {
                    fixed (Byte* pointer = Buffer)
                    {
                        if (value)
                        {
                            pointer[index / 8] = (Byte) (pointer[index / 8] | (1 << (index % 8)));
                            return;
                        }

                        pointer[index / 8] = (Byte) (pointer[index / 8] & ~(1 << (index % 8)));
                    }
                }
            }
        }

        public Boolean this[Index index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return this[index.GetOffset(Length)];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this[index.GetOffset(Length)] = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("GetHashCode() on Span will always throw an exception.")]
        public override Int32 GetHashCode()
        {
            return Buffer.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Equals() on Span will always throw an exception. Use == instead.")]
        public override Boolean Equals(Object? obj)
        {
            return Buffer.Equals(obj);
        }

        public override String ToString()
        {
            return Buffer.ToString();
        }
    }
}