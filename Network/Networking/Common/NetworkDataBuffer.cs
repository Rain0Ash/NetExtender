using System;
using System.Text;

namespace NetExtender.Network.Networking.Common
{
    /// <summary>
    ///     Dynamic byte buffer
    /// </summary>
    public class NetworkDataBuffer
    {
        /// <summary>
        ///     Initialize a new expandable buffer with zero capacity
        /// </summary>
        public NetworkDataBuffer()
        {
            Data = Array.Empty<Byte>();
            Size = 0;
            Offset = 0;
        }

        /// <summary>
        ///     Initialize a new expandable buffer with the given capacity
        /// </summary>
        public NetworkDataBuffer(Int64 capacity)
        {
            Data = new Byte[capacity];
            Size = 0;
            Offset = 0;
        }

        /// <summary>
        ///     Initialize a new expandable buffer with the given data
        /// </summary>
        public NetworkDataBuffer(Byte[] data)
        {
            Data = data;
            Size = data.Length;
            Offset = 0;
        }

        /// <summary>
        ///     Is the buffer empty?
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return Data is null || Size == 0;
            }
        }

        /// <summary>
        ///     Bytes memory buffer
        /// </summary>
        public Byte[] Data { get; private set; }

        /// <summary>
        ///     Bytes memory buffer capacity
        /// </summary>
        public Int64 Capacity
        {
            get
            {
                return Data.LongLength;
            }
        }

        /// <summary>
        ///     Bytes memory buffer size
        /// </summary>
        public Int64 Size { get; private set; }

        /// <summary>
        ///     Bytes memory buffer offset
        /// </summary>
        public Int64 Offset { get; private set; }

        /// <summary>
        ///     Buffer indexer operator
        /// </summary>
        public Byte this[Int32 index]
        {
            get
            {
                return Data[index];
            }
        }

        /// <summary>
        ///     Get string from the current buffer
        /// </summary>
        public override String ToString()
        {
            return ExtractString(0, Size);
        }

        // Clear the current buffer and its offset
        public void Clear()
        {
            Size = 0;
            Offset = 0;
        }

        /// <summary>
        ///     Extract the string from buffer of the given offset and size
        /// </summary>
        public String ExtractString(Int64 offset, Int64 size)
        {
            if (offset + size > Size)
            {
                throw new ArgumentException(@"Invalid offset & size!", nameof(offset));
            }

            return Encoding.UTF8.GetString(Data, (Int32) offset, (Int32) size);
        }

        /// <summary>
        ///     Remove the buffer of the given offset and size
        /// </summary>
        public void Remove(Int64 offset, Int64 size)
        {
            if (offset + size > Size)
            {
                throw new ArgumentException(@"Invalid offset & size!", nameof(offset));
            }

            Array.Copy(Data, offset + size, Data, offset, Size - size - offset);

            Size -= size;

            if (Offset >= offset + size)
            {
                Offset -= size;
            }
            else if (Offset >= offset)
            {
                Offset -= Offset - offset;
                if (Offset > Size)
                {
                    Offset = Size;
                }
            }
        }

        /// <summary>
        ///     Reserve the buffer of the given capacity
        /// </summary>
        public void Reserve(Int64 capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException(@"Invalid reserve capacity!", nameof(capacity));
            }

            if (capacity <= Capacity)
            {
                return;
            }

            Byte[] data = new Byte[Math.Max(capacity, 2 * Capacity)];
            Array.Copy(Data, 0, data, 0, Size);
            Data = data;
        }

        // Resize the current buffer
        public void Resize(Int64 size)
        {
            Reserve(size);

            Size = size;
            if (Offset > Size)
            {
                Offset = Size;
            }
        }

        // Shift the current buffer offset
        public void Shift(Int64 offset)
        {
            Offset += offset;
        }

        // Unshift the current buffer offset
        public void Unshift(Int64 offset)
        {
            Offset -= offset;
        }

        /// <summary>
        ///     Append the given buffer
        /// </summary>
        /// <param name="buffer">Buffer to append</param>
        /// <returns>Count of append bytes</returns>
        public Int64 Append(Byte[] buffer)
        {
            Reserve(Size + buffer.Length);
            Array.Copy(buffer, 0, Data, Size, buffer.Length);
            Size += buffer.Length;
            return buffer.Length;
        }

        /// <summary>
        ///     Append the given buffer fragment
        /// </summary>
        /// <param name="buffer">Buffer to append</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Count of append bytes</returns>
        public Int64 Append(Byte[] buffer, Int64 offset, Int64 size)
        {
            Reserve(Size + size);
            Array.Copy(buffer, offset, Data, Size, size);

            Size += size;
            return size;
        }

        /// <summary>
        ///     Append the given text in UTF-8 encoding
        /// </summary>
        /// <param name="text">Text to append</param>
        /// <returns>Count of append bytes</returns>
        public Int64 Append(String text)
        {
            Reserve(Size + Encoding.UTF8.GetMaxByteCount(text.Length));
            Int64 result = Encoding.UTF8.GetBytes(text, 0, text.Length, Data, (Int32) Size);

            Size += result;
            return result;
        }
    }
}