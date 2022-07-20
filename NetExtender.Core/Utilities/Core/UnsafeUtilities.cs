// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    public static class UnsafeUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref IntPtr RefTypeHandle(this Object instance)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return ref **(IntPtr**)Unsafe.AsPointer(ref instance);
        }
        
        public static T UncheckedCast<T>(this Object instance)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            Object casting = instance.MemberwiseClone();
            casting.RefTypeHandle() = typeof(T).TypeHandle.Value;
            return Unsafe.As<Object, T>(ref casting);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T Read<T>(void* source)
        {
            return Unsafe.Read<T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T ReadUnaligned<T>(void* source)
        {
            return Unsafe.ReadUnaligned<T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadUnaligned<T>(ref Byte source)
        {
            return Unsafe.ReadUnaligned<T>(ref source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill(void* destination, Int32 length)
        {
            Fill(destination, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill(void* destination, UInt32 length)
        {
            Fill(destination, Byte.MinValue, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill(void* destination, Boolean value, Int32 length)
        {
            Fill(destination, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill(void* destination, Boolean value, UInt32 length)
        {
            Unsafe.InitBlock(destination, value ? Byte.MaxValue : Byte.MinValue, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill(void* destination, Byte value, Int32 length)
        {
            Fill(destination, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill(void* destination, Byte value, UInt32 length)
        {
            Unsafe.InitBlock(destination, value, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill(ref Byte start, Int32 length)
        {
            Fill(ref start, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill(ref Byte start, UInt32 length)
        {
            Fill(ref start, Byte.MinValue, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill(ref Byte start, Boolean value, Int32 length)
        {
            Fill(ref start, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill(ref Byte start, Boolean value, UInt32 length)
        {
            Unsafe.InitBlock(ref start, value ? Byte.MaxValue : Byte.MinValue, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill(ref Byte start, Byte value, Int32 length)
        {
            Fill(ref start, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill(ref Byte start, Byte value, UInt32 length)
        {
            Unsafe.InitBlock(ref start, value, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void FillUnaligned(void* destination, Int32 length)
        {
            FillUnaligned(destination, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void FillUnaligned(void* destination, UInt32 length)
        {
            FillUnaligned(destination, Byte.MinValue, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void FillUnaligned(void* destination, Boolean value, Int32 length)
        {
            FillUnaligned(destination, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void FillUnaligned(void* destination, Boolean value, UInt32 length)
        {
            Unsafe.InitBlockUnaligned(destination, value ? Byte.MaxValue : Byte.MinValue, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void FillUnaligned(void* destination, Byte value, Int32 length)
        {
            FillUnaligned(destination, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void FillUnaligned(void* destination, Byte value, UInt32 length)
        {
            Unsafe.InitBlockUnaligned(destination, value, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillUnaligned(ref Byte start, Int32 length)
        {
            FillUnaligned(ref start, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillUnaligned(ref Byte start, UInt32 length)
        {
            FillUnaligned(ref start, Byte.MinValue, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillUnaligned(ref Byte start, Boolean value, Int32 length)
        {
            FillUnaligned(ref start, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillUnaligned(ref Byte start, Boolean value, UInt32 length)
        {
            Unsafe.InitBlock(ref start, value ? Byte.MaxValue : Byte.MinValue, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillUnaligned(ref Byte start, Byte value, Int32 length)
        {
            FillUnaligned(ref start, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillUnaligned(ref Byte start, Byte value, UInt32 length)
        {
            Unsafe.InitBlock(ref start, value, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill<T>(void* destination, Int32 count)
        {
            if (count >= 0)
            {
                Fill<T>(destination, (UInt32) count);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill<T>(void* destination, UInt32 count)
        {
            Fill<T>(destination, default, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill<T>(void* destination, T? value, Int32 count)
        {
            if (count >= 0)
            {
                Fill(destination, value, (UInt32) count);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Fill<T>(void* destination, T? value, UInt32 count)
        {
            for (Int32 i = 0; i < count; i++)
            {
                Write(destination, value);
                destination = Add<T>(destination);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Write<T>(void* destination, T value)
        {
            Unsafe.Write(destination, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteUnaligned<T>(void* destination, T value)
        {
            Unsafe.WriteUnaligned(destination, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUnaligned<T>(ref Byte destination, T value)
        {
            Unsafe.WriteUnaligned(ref destination, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Copy<T>(void* destination, ref T source)
        {
            Unsafe.Copy(destination, ref source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Copy<T>(ref T destination, void* source)
        {
            Unsafe.Copy(ref destination, source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* AsPointer<T>(ref T value)
        {
            return Unsafe.AsPointer(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SizeOf<T>()
        {
            return Unsafe.SizeOf<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void CopyBlock(void* destination, void* source, Int32 length)
        {
            CopyBlock(destination, source, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void CopyBlock(void* destination, void* source, UInt32 length)
        {
            Unsafe.CopyBlock(destination, source, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyBlock(ref Byte destination, ref Byte source, Int32 length)
        {
            CopyBlock(ref destination, ref source, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyBlock(ref Byte destination, ref Byte source, UInt32 length)
        {
            Unsafe.CopyBlock(ref destination, ref source, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void CopyBlockUnaligned(void* destination, void* source, Int32 length)
        {
            CopyBlockUnaligned(destination, source, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void CopyBlockUnaligned(void* destination, void* source, UInt32 length)
        {
            Unsafe.CopyBlockUnaligned(destination, source, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyBlockUnaligned(ref Byte destination, ref Byte source, Int32 length)
        {
            CopyBlockUnaligned(ref destination, ref source, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyBlockUnaligned(ref Byte destination, ref Byte source, UInt32 length)
        {
            Unsafe.CopyBlockUnaligned(ref destination, ref source, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void InitBlock(void* destination, Byte value, Int32 length)
        {
            InitBlock(destination, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void InitBlock(void* destination, Byte value, UInt32 length)
        {
            Unsafe.InitBlock(destination, value, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlock(ref Byte destination, Byte value, Int32 length)
        {
            InitBlock(ref destination, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlock(ref Byte destination, Byte value, UInt32 length)
        {
            Unsafe.InitBlock(ref destination, value, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void InitBlockUnaligned(void* destination, Byte value, Int32 length)
        {
            InitBlockUnaligned(destination, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void InitBlockUnaligned(void* destination, Byte value, UInt32 length)
        {
            Unsafe.InitBlockUnaligned(destination, value, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlockUnaligned(ref Byte destination, Byte value, Int32 length)
        {
            InitBlockUnaligned(ref destination, value, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlockUnaligned(ref Byte destination, Byte value, UInt32 length)
        {
            Unsafe.InitBlockUnaligned(ref destination, value, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SwapBlock(void* destination, void* source, Int32 length)
        {
            SwapBlock(destination, source, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SwapBlock(void* destination, void* source, UInt32 length)
        {
            SwapBlock(destination, source, length, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SwapBlock(void* destination, void* source, Int32 length, Int32 stackbuffer)
        {
            SwapBlock(destination, source, length >= 0 ? (UInt32) length : 0, stackbuffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void SwapBlock(void* destination, void* source, UInt32 length, Int32 stackbuffer)
        {
            if (length <= 0)
            {
                return;
            }

            Span<Byte> buffer = stackbuffer switch
            {
                0 => stackalloc Byte[(Int32) Math.Min(length, UInt16.MaxValue + 1)],
                > 0 => stackalloc Byte[(Int32) Math.Min(length, (UInt32) stackbuffer)],
                _ => new Byte[Math.Min(length, (UInt32) (-stackbuffer))]
            };
            
            SwapBlock(destination, source, length, buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SwapBlock(void* destination, void* source, Int32 length, Span<Byte> buffer)
        {
            SwapBlock(destination, source, length >= 0 ? (UInt32) length : 0, buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void SwapBlock(void* destination, void* source, UInt32 length, Span<Byte> buffer)
        {
            if (length <= 0)
            {
                return;
            }

            unchecked
            {
                fixed (void* pointer = buffer)
                {
                    UInt32 count = Math.Min(length, (UInt32) buffer.Length);
                    
                    UInt64 position = 0;

                    while (position < length - count)
                    {
                        Unsafe.CopyBlock(pointer, destination, count);
                        Unsafe.CopyBlock(destination, source, count);
                        Unsafe.CopyBlock(source, pointer, count);

                        Increment(ref source, count);
                        Increment(ref destination, count);

                        position += count;
                    }

                    if (position >= length)
                    {
                        return;
                    }

                    count = length - (UInt32) position;

                    Unsafe.CopyBlock(pointer, destination, count);
                    Unsafe.CopyBlock(destination, source, count);
                    Unsafe.CopyBlock(source, pointer, count);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SwapBlockUnaligned(void* destination, void* source, Int32 length)
        {
            SwapBlockUnaligned(destination, source, length >= 0 ? (UInt32) length : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SwapBlockUnaligned(void* destination, void* source, UInt32 length)
        {
            SwapBlockUnaligned(destination, source, length, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SwapBlockUnaligned(void* destination, void* source, Int32 length, Int32 stackbuffer)
        {
            SwapBlockUnaligned(destination, source, length >= 0 ? (UInt32) length : 0, stackbuffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void SwapBlockUnaligned(void* destination, void* source, UInt32 length, Int32 stackbuffer)
        {
            if (length <= 0)
            {
                return;
            }

            Span<Byte> buffer = stackbuffer switch
            {
                0 => stackalloc Byte[(Int32) Math.Min(length, UInt16.MaxValue + 1)],
                > 0 => stackalloc Byte[(Int32) Math.Min(length, (UInt32) stackbuffer)],
                _ => new Byte[Math.Min(length, (UInt32) (-stackbuffer))]
            };
            
            SwapBlockUnaligned(destination, source, length, buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SwapBlockUnaligned(void* destination, void* source, Int32 length, Span<Byte> buffer)
        {
            SwapBlockUnaligned(destination, source, length >= 0 ? (UInt32) length : 0, buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static unsafe void SwapBlockUnaligned(void* destination, void* source, UInt32 length, Span<Byte> buffer)
        {
            if (length <= 0)
            {
                return;
            }

            unchecked
            {
                fixed (void* pointer = buffer)
                {
                    UInt32 count = Math.Min(length, (UInt32) buffer.Length);
                    
                    UInt64 position = 0;

                    while (position < length - count)
                    {
                        Unsafe.CopyBlockUnaligned(pointer, destination, count);
                        Unsafe.CopyBlockUnaligned(destination, source, count);
                        Unsafe.CopyBlockUnaligned(source, pointer, count);

                        Increment(ref source, count);
                        Increment(ref destination, count);

                        position += count;
                    }

                    if (position >= length)
                    {
                        return;
                    }

                    count = length - (UInt32) position;

                    Unsafe.CopyBlockUnaligned(pointer, destination, count);
                    Unsafe.CopyBlockUnaligned(destination, source, count);
                    Unsafe.CopyBlockUnaligned(source, pointer, count);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T As<T>(Object value) where T : class
        {
            return Unsafe.As<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref T AsRef<T>(void* source)
        {
            return ref Unsafe.AsRef<T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AsRef<T>(in T source)
        {
            return ref Unsafe.AsRef(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo As<TFrom, TTo>(ref TFrom source)
        {
            return ref Unsafe.As<TFrom, TTo>(ref source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Unbox<T>(Object box) where T : struct
        {
            return ref Unsafe.Unbox<T>(box);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(ref T source, Int32 elementOffset)
        {
            return ref Unsafe.Add(ref source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Add(void* source)
        {
            return Add(source, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Add(ref void* source)
        {
            source = Add(source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Increment(ref void* source)
        {
            Add(ref source);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Add(void* source, Int32 byteOffset)
        {
            return (void*) ((IntPtr) source + byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Add(void* source, UInt32 byteOffset)
        {
            return byteOffset <= Int32.MaxValue ? Add(source, (Int32) byteOffset) : Add(Add(source, Int32.MaxValue), (Int32) (byteOffset - Int32.MaxValue));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Add(ref void* source, Int32 byteOffset)
        {
            source = Add(source, byteOffset);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Add(ref void* source, UInt32 byteOffset)
        {
            source = Add(source, byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Increment(ref void* source, Int32 byteOffset)
        {
            Add(ref source, byteOffset);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Increment(ref void* source, UInt32 byteOffset)
        {
            Add(ref source, byteOffset);
            return source;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Add<T>(void* source)
        {
            return Add<T>(source, 1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Add<T>(ref void* source)
        {
            source = Add<T>(source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Increment<T>(ref void* source)
        {
            Add<T>(ref source);
            return source;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Add<T>(void* source, Int32 elementOffset)
        {
            return Unsafe.Add<T>(source, elementOffset);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Add<T>(ref void* source, Int32 elementOffset)
        {
            source = Add<T>(source, elementOffset);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Increment<T>(ref void* source, Int32 elementOffset)
        {
            Add<T>(ref source, elementOffset);
            return source;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(ref T source)
        {
            return ref Unsafe.Add(ref source, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(ref T source, IntPtr elementOffset)
        {
            return ref Unsafe.Add(ref source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
        {
            return ref Unsafe.AddByteOffset(ref source, byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Subtract<T>(ref T source, Int32 elementOffset)
        {
            return ref Unsafe.Subtract(ref source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Subtract(void* source)
        {
            return Subtract(source, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Subtract(ref void* source)
        {
            source = Subtract(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Decrement(ref void* source)
        {
            Subtract(ref source);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Subtract(void* source, Int32 byteOffset)
        {
            return (void*) ((IntPtr) source - byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Subtract(void* source, UInt32 byteOffset)
        {
            return byteOffset <= Int32.MaxValue ? Subtract(source, (Int32) byteOffset) : Subtract(Subtract(source, Int32.MaxValue), (Int32) (byteOffset - Int32.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Subtract(ref void* source, Int32 byteOffset)
        {
            source = Subtract(source, byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Subtract(ref void* source, UInt32 byteOffset)
        {
            source = Subtract(source, byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Decrement(ref void* source, Int32 byteOffset)
        {
            Subtract(ref source, byteOffset);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Decrement(ref void* source, UInt32 byteOffset)
        {
            Subtract(ref source, byteOffset);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Subtract<T>(void* source)
        {
            return Subtract<T>(source, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Subtract<T>(ref void* source)
        {
            source = Subtract<T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Decrement<T>(ref void* source)
        {
            Subtract<T>(ref source);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Subtract<T>(void* source, Int32 elementOffset)
        {
            return Unsafe.Subtract<T>(source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void Subtract<T>(ref void* source, Int32 elementOffset)
        {
            source = Subtract<T>(source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void* Decrement<T>(ref void* source, Int32 elementOffset)
        {
            Subtract<T>(ref source, elementOffset);
            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Subtract<T>(ref T source)
        {
            return ref Unsafe.Subtract(ref source, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Subtract<T>(ref T source, IntPtr elementOffset)
        {
            return ref Unsafe.Subtract(ref source, elementOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T SubtractByteOffset<T>(ref T source, IntPtr byteOffset)
        {
            return ref Unsafe.SubtractByteOffset(ref source, byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr ByteOffset<T>(ref T origin, ref T target)
        {
            return Unsafe.ByteOffset(ref origin, ref target);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AreSame<T>(ref T left, ref T right)
        {
            return Unsafe.AreSame(ref left, ref right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAddressGreaterThan<T>(ref T left, ref T right)
        {
            return Unsafe.IsAddressGreaterThan(ref left, ref right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAddressLessThan<T>(ref T left, ref T right)
        {
            return Unsafe.IsAddressLessThan(ref left, ref right);
        }

        public static class Segfault
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            private delegate void UnmanagedCallback();

            private static UnmanagedCallback? SegfaultDelegate { get; } = (UnmanagedCallback) Marshal.GetDelegateForFunctionPointer((IntPtr) 1, typeof(UnmanagedCallback));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Crash()
            {
                SegfaultDelegate?.Invoke();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Task CrashAsync(Int32 milliseconds)
            {
                return CrashAsync(milliseconds, CancellationToken.None);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Task CrashAsync(TimeSpan wait)
            {
                return CrashAsync(wait, CancellationToken.None);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Task CrashAsync(Int32 milliseconds, CancellationToken token)
            {
                return CrashAsync(TimeSpan.FromMilliseconds(milliseconds), token);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task CrashAsync(TimeSpan wait, CancellationToken token)
            {
                try
                {
                    await Task.Delay(wait, token).ContinueWith(Crash, token).ConfigureAwait(false);
                }
                catch (TaskCanceledException)
                {
                    //ignored
                }
            }
        }
    }
}