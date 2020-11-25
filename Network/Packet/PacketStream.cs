﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

 using System;
 using System.Collections;
 using System.Collections.Generic;
 using System.Text;
 using NetExtender.Converters.BitEndian;
 using NetExtender.Utils.Types;
 using SBuffer = System.Buffer;

 namespace NetExtender.Network.Packet
{
    [Serializable]
    public sealed class MarshalException : Exception
    {
    }

    /// <summary>
    /// Class to manage, merge, read and write packets. 
    /// Methods have equal names as BinaryReader and BinaryWriter.
    /// → Class has dependency from stream endianess!
    /// </summary>
    public class PacketStream : ICloneable, IComparable
    {
        private const Int32 DefaultSize = 128;
        public Byte[] Buffer { get; private set; }

        public Int32 Count { get; private set; }

        public Int32 Capacity
        {
            get
            {
                return Buffer.Length;
            }
        }

        public Int32 Pos { get; set; }

        public Boolean IsLittleEndian { get; set; }

        public Boolean HasBytes
        {
            get
            {
                return Pos < Count;
            }
        }

        public Int32 LeftBytes
        {
            get
            {
                return Count - Pos;
            }
        }

        public EndianBitConverter Converter
        {
            get
            {
                return IsLittleEndian ? EndianBitConverter.Little : (EndianBitConverter) EndianBitConverter.Big;
            }
        }

        public Byte this[Int32 index]
        {
            set
            {
                Buffer[index] = value;
            }
            get
            {
                return Buffer[index];
            }
        }

        public static explicit operator PacketStream(Byte[] o)
        {
            return new PacketStream(o);
        }

        public static implicit operator Byte[](PacketStream o)
        {
            return o.GetBytes();
        }


        public PacketStream()
            : this(DefaultSize)
        {
        }

        public PacketStream(Int32 count)
        {
            IsLittleEndian = true;
            Reserve(count);
        }

        public PacketStream(PacketStream sourcePacketStream)
        {
            IsLittleEndian = sourcePacketStream.IsLittleEndian;
            Replace(sourcePacketStream);
        }

        public PacketStream(Byte[] sourcebytes)
        {
            IsLittleEndian = true;
            Replace(sourcebytes);
        }

        public PacketStream(Byte[] sourcebytes, Int32 offset, Int32 count)
        {
            IsLittleEndian = true;
            Replace(sourcebytes, offset, count);
        }

        public PacketStream(PacketStream sourcePacketStream, Int32 offset, Int32 count)
        {
            IsLittleEndian = sourcePacketStream.IsLittleEndian;
            Replace(sourcePacketStream, offset, count);
        }


        private static Byte[] Roundup(Int32 length)
        {
            Int32 i = 16;
            while (length > i)
            {
                i <<= 1;
            }

            return new Byte[i];
        }

        /// <summary>
        /// Initializes buffer for this stream with provided minimum size.
        /// </summary>
        /// <param name="count">Minimum buffer size.</param>
        public void Reserve(Int32 count)
        {
            if (Buffer is null)
            {
                Buffer = Roundup(count);
            }
            else if (count > Buffer.Length)
            {
                Byte[] newBuffer = Roundup(count);
                SBuffer.BlockCopy(Buffer, 0, newBuffer, 0, Count);
                Buffer = newBuffer;
            }
        }


        /// <summary>
        /// Replace current PacketStream with provided one.
        /// </summary>
        /// <param name="stream">Replace stream.</param>
        /// <returns></returns>
        public PacketStream Replace(PacketStream stream)
        {
            return Replace(stream.Buffer, 0, stream.Count);
        }

        /// <summary>
        /// Replace current PacketStream with provided byte array.
        /// </summary>
        /// <param name="bytes">Array of bytes</param>
        /// <returns></returns>
        public PacketStream Replace(Byte[] bytes)
        {
            return Replace(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Replace current PacketStream with some bytes from provided stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public PacketStream Replace(PacketStream stream, Int32 offset, Int32 count)
        {
            // убрать мусор оставшейся после копирования из PacketStream stream
            return Replace(stream.Buffer, offset, count);
        }

        /// <summary>
        /// Replace current PacketStream with some bytes from provided byte array.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public PacketStream Replace(Byte[] bytes, Int32 offset, Int32 count)
        {
            Reserve(count);
            SBuffer.BlockCopy(bytes, offset, Buffer, 0, count);
            Count = count;
            return this;
        }


        /// <summary>
        /// Clears current stream.
        /// </summary>
        /// <returns></returns>
        public PacketStream Clear()
        {
            Array.Clear(Buffer, 0, Count);
            Count = 0;
            return this;
        }


        public PacketStream PushBack(Byte b)
        {
            Reserve(Count + 1);
            Buffer[Count++] = b;
            return this;
        }


        public PacketStream Swap(PacketStream swapStream)
        {
            Int32 i = Count;
            Count = swapStream.Count;
            swapStream.Count = i;

            Byte[] temp = swapStream.Buffer;
            swapStream.Buffer = Buffer;
            Buffer = temp;
            return this;
        }


        public void Rollback()
        {
            Pos = 0;
        }

        public void Rollback(Int32 len)
        {
            Pos -= len;
        }


        public PacketStream Erase(Int32 from)
        {
            return Erase(from, Count);
        }

        public PacketStream Erase(Int32 from, Int32 to)
        {
            if (from > to)
            {
                throw new ArgumentOutOfRangeException(nameof(@from));
            }

            if (Count < to)
            {
                throw new ArgumentOutOfRangeException(nameof(to));
            }

            // копируем байты с позиции to в позицию from, тем самым затирая то, что между
            SBuffer.BlockCopy(Buffer, to, Buffer, from, Count -= to - from);
            return this;
        }


        public PacketStream Insert(Int32 offset, PacketStream copyStream)
        {
            return Insert(offset, copyStream.Buffer, 0, copyStream.Count);
        }

        public PacketStream Insert(Int32 offset, Byte[] copyArray)
        {
            return Insert(offset, copyArray, 0, copyArray.Length);
        }

        public PacketStream Insert(Int32 offset, PacketStream copyStream, Int32 copyStreamOffset, Int32 count)
        {
            return Insert(offset, copyStream.Buffer, copyStreamOffset, count);
        }

        public PacketStream Insert(Int32 offset, Byte[] copyArray, Int32 copyArrayOffset, Int32 count)
        {
            Reserve(Count + count);
            // передвигаем данные с позиции offset до позиции offset + count
            SBuffer.BlockCopy(Buffer, offset, Buffer, offset + count, Count - offset);
            // копируем новый массив данных в позицию offset
            SBuffer.BlockCopy(copyArray, copyArrayOffset, Buffer, offset, count);
            Count += count;
            return this;
        }


        public Byte[] GetBytes()
        {
            Byte[] temp = new Byte[Count];
            SBuffer.BlockCopy(Buffer, 0, temp, 0, Count);
            return temp;
        }


        public Boolean ReadBoolean()
        {
            return ReadByte() == 1;
        }

        public Byte ReadByte()
        {
            if (Pos + 1 > Count)
            {
                throw new MarshalException();
            }

            return this[Pos++];
        }

        public SByte ReadSByte()
        {
            if (Pos + 1 > Count)
            {
                throw new MarshalException();
            }

            return (SByte) this[Pos++];
        }

        public Byte[] ReadBytes(Int32 count)
        {
            if (Pos + count > Count)
            {
                throw new MarshalException();
            }

            Byte[] result = new Byte[count];
            SBuffer.BlockCopy(Buffer, Pos, result, 0, count);
            Pos += count;
            return result;
        }

        public Byte[] ReadBytes()
        {
            Int16 count = ReadInt16();

            if (Pos + count > Count)
            {
                throw new MarshalException();
            }

            Byte[] result = new Byte[count];
            SBuffer.BlockCopy(Buffer, Pos, result, 0, count);
            Pos += count;
            return result;
        }

        public Char ReadChar()
        {
            if (Pos + 2 > Count)
            {
                throw new MarshalException();
            }

            Char result = Converter.ToChar(Buffer, Pos);
            Pos += 2;

            return result;
        }

        public Char[] ReadChars(Int32 count)
        {
            if (Pos + 2 * count > Count)
            {
                throw new MarshalException();
            }

            Char[] result = new Char[count];
            for (Int32 i = 0; i < count; i++)
            {
                result[i] = ReadChar();
            }

            return result;
        }

        public Int16 ReadInt16()
        {
            if (Pos + 2 > Count)
            {
                throw new MarshalException();
            }

            Int16 result = Converter.ToInt16(Buffer, Pos);
            Pos += 2;

            return result;
        }

        public Int32 ReadInt32()
        {
            if (Pos + 4 > Count)
            {
                throw new MarshalException();
            }

            Int32 result = Converter.ToInt32(Buffer, Pos);
            Pos += 4;

            return result;
        }

        public Int64 ReadInt64()
        {
            if (Pos + 8 > Count)
            {
                throw new MarshalException();
            }

            Int64 result = Converter.ToInt64(Buffer, Pos);
            Pos += 8;

            return result;
        }

        public UInt16 ReadUInt16()
        {
            if (Pos + 2 > Count)
            {
                throw new MarshalException();
            }

            UInt16 result = Converter.ToUInt16(Buffer, Pos);
            Pos += 2;

            return result;
        }

        public UInt32 ReadUInt32()
        {
            if (Pos + 4 > Count)
            {
                throw new MarshalException();
            }

            UInt32 result = Converter.ToUInt32(Buffer, Pos);
            Pos += 4;

            return result;
        }

        public UInt32 ReadBc()
        {
            if (Pos + 3 > Count)
            {
                throw new MarshalException();
            }

            Int32 result = ReadUInt16() + (ReadByte() << 16);

            return (UInt32) result;
        }

        public UInt64 ReadUInt64()
        {
            if (Pos + 8 > Count)
            {
                throw new MarshalException();
            }

            UInt64 result = Converter.ToUInt64(Buffer, Pos);
            Pos += 8;

            return result;
        }

        public Single ReadSingle()
        {
            if (Pos + 4 > Count)
            {
                throw new MarshalException();
            }

            Single result = Converter.ToSingle(Buffer, Pos);
            Pos += 4;

            return result;
        }

        public Double ReadDouble()
        {
            if (Pos + 8 > Count)
            {
                throw new MarshalException();
            }

            Double result = Converter.ToDouble(Buffer, Pos);
            Pos += 8;

            return result;
        }


        public PacketStream ReadPacketStream()
        {
            Int16 i = ReadInt16();
            if (Pos + i > Count)
            {
                throw new MarshalException();
            }

            PacketStream newStream = new PacketStream(Buffer, Pos, i);
            Pos += i;
            return newStream;
        }

        public PacketStream Read(PacketStream stream)
        {
            Int16 i = ReadInt16();
            if (Pos + i > Count)
            {
                throw new MarshalException();
            }

            stream.Replace(Buffer, Pos, i);
            Pos += i;
            return this;
        }

        public void Read(PacketMarshaler paramMarshal)
        {
            paramMarshal.Read(this);
        }

        public T Read<T>() where T : PacketMarshaler, new()
        {
            T t = new T();
            Read(t);
            return t;
        }

        public List<T> ReadCollection<T>() where T : PacketMarshaler, new()
        {
            Int32 count = ReadInt32();
            List<T> collection = new List<T>();
            for (Int32 i = 0; i < count; i++)
            {
                T t = new T();
                Read(t);
                collection.Add(t);
            }

            return collection;
        }

        public DateTime ReadDateTime()
        {
            return TimeUtils.UnixTime(ReadInt64());
        }

        public Int64[] ReadPisc(Int32 count)
        {
            Int64[] result = new Int64[count];
            BitArray pish = new BitArray(new[] {ReadByte()});
            for (Int32 index = 0; index < count * 2; index += 2)
            {
                if (pish[index] && pish[index + 1]) // uint
                {
                    result[index / 2] = ReadUInt32();
                }
                else if (pish[index + 1]) // bc
                {
                    result[index / 2] = ReadBc();
                }
                else if (pish[index]) // ushort
                {
                    result[index / 2] = ReadUInt16();
                }
                else // byte
                {
                    result[index / 2] = ReadByte();
                }
            }

            return result;
        }


        public String ReadString()
        {
            Int16 i = ReadInt16();
            Byte[] strBuf = ReadBytes(i);
            return Encoding.UTF8.GetString(strBuf).Trim('\u0000');
        }

        public String ReadString(Int32 len)
        {
            Byte[] strBuf = ReadBytes(len);
            return Encoding.UTF8.GetString(strBuf).Trim('\u0000');
        }


        public PacketStream Write(Boolean value)
        {
            return Write(value ? (Byte) 0x01 : (Byte) 0x00);
        }

        public PacketStream Write(Byte value)
        {
            PushBack(value);
            return this;
        }

        public PacketStream Write(Byte[] value, Boolean appendSize = false)
        {
            if (appendSize)
            {
                Write((UInt16) value.Length);
            }

            return Insert(Count, value);
        }

        public PacketStream Write(SByte value)
        {
            return Write((Byte) value);
        }

        public PacketStream Write(Char value)
        {
            return Write(Converter.GetBytes(value));
        }

        public PacketStream Write(Char[] value)
        {
            foreach (Char ch in value)
            {
                Write(ch);
            }

            return this;
        }

        public PacketStream Write(Int16 value)
        {
            return Write(Converter.GetBytes(value));
        }

        public PacketStream Write(Int32 value)
        {
            return Write(Converter.GetBytes(value));
        }

        public PacketStream Write(Int64 value)
        {
            return Write(Converter.GetBytes(value));
        }

        public PacketStream Write(UInt16 value)
        {
            return Write(Converter.GetBytes(value));
        }

        public PacketStream Write(UInt32 value)
        {
            return Write(Converter.GetBytes(value));
        }

        public PacketStream Write(UInt64 value)
        {
            return Write(Converter.GetBytes(value));
        }

        public PacketStream Write(Single value)
        {
            return Write(Converter.GetBytes(value));
        }

        public PacketStream Write(Double value)
        {
            return Write(Converter.GetBytes(value));
        }

        public PacketStream WriteBc(UInt32 value)
        {
            return Write(Converter.GetBytes(value, 3));
        }


        public PacketStream Write(PacketMarshaler value)
        {
            return value.Write(this);
        }

        public PacketStream Write<T>(ICollection<T> values) where T : PacketMarshaler
        {
            Write(values.Count);
            foreach (T marshaler in values)
            {
                Write(marshaler);
            }

            return this;
        }

        public PacketStream Write(PacketStream value, Boolean appendSize = true)
        {
            return Write(value.GetBytes(), appendSize);
        }

        public PacketStream Write(DateTime value)
        {
            return Write(value.UnixTime());
        }

        public PacketStream Write(Guid value, Boolean appendSize = true)
        {
            return Write(value.ToByteArray(), appendSize);
        }

        public PacketStream WritePisc(params Int64[] values)
        {
            BitArray pish = new BitArray(8);
            PacketStream temp = new PacketStream();
            Int32 index = 0;
            foreach (Int64 value in values)
            {
                if (value <= Byte.MaxValue)
                {
                    temp.Write((Byte) value);
                }
                else if (value <= UInt16.MaxValue)
                {
                    pish[index] = true;
                    temp.Write((UInt16) value);
                }
                else if (value <= 0xffffff)
                {
                    pish[index + 1] = true;
                    temp.WriteBc((UInt32) value);
                }
                else
                {
                    pish[index] = true;
                    pish[index + 1] = true;
                    temp.Write((UInt32) value);
                }

                index += 2;
            }

            Byte[] res = new Byte[1];
            pish.CopyTo(res, 0);
            Write(res[0]);
            Write(temp, false);
            return this;
        }


        public PacketStream Write(String value, Boolean appendSize = true, Boolean appendTerminator = false)
        {
            Byte[] str = Encoding.UTF8.GetBytes(appendTerminator ? value + '\u0000' : value); // utf-8
            return Write(str, appendSize);
        }


        public override String ToString()
        {
            return BitConverter.ToString(GetBytes());
        }


        public Boolean Equals(PacketStream stream)
        {
            if (Count != stream.Count)
            {
                return false;
            }

            for (Int32 i = 0; i < Count; i++)
            {
                if (this[i] != stream[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override Boolean Equals(Object obj)
        {
            if (obj is PacketStream stream)
            {
                return Equals(stream);
            }

            return false;
        }

        public override Int32 GetHashCode()
        {
            return Buffer.GetHashCode();
        }


        public Object Clone()
        {
            return new PacketStream(this);
        }


        public Int32 CompareTo(Object? obj)
        {
            if (!(obj is PacketStream stream))
            {
                throw new ArgumentException("Object is not an PacketStream instance");
            }

            Int32 count = Math.Min(Count, stream.Count);
            for (Int32 i = 0; i < count; i++)
            {
                Int32 k = this[i] - stream[i];
                if (k != 0)
                {
                    return k;
                }
            }

            return Count - stream.Count;
        }
    }
}