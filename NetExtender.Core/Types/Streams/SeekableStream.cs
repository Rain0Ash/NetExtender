// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;

namespace NetExtender.Types.Streams
{
    /// <summary>
    /// Implements a seekable read-only stream which uses buffering if
    /// underlying stream is not seekable.
    /// </summary>
    public class SeekableStream : Stream
    {
        protected Stream Stream { get; }
        protected Stream Buffer { get; }

        public override Int64 Length
        {
            get
            {
                if (Stream.CanSeek)
                {
                    return Stream.Length;
                }

                Int64 position = Position;
                Seek(0, SeekOrigin.End);

                Int64 length = Position;
                Position = position;

                return length;
            }
        }

        public override Int64 Position
        {
            get
            {
                return Stream.CanSeek ? Stream.Position : Buffer.Position;
            }
            set
            {
                if (Stream.CanSeek)
                {
                    Stream.Position = value;
                    return;
                }

                if (Buffer.Position == value)
                {
                    return;
                }

                if (value < Buffer.Position || value < Buffer.Length)
                {
                    Buffer.Position = value;
                    return;
                }

                Buffer.Seek(0, SeekOrigin.End);

                Byte[] buffer = new Byte[4096];
                Int64 total = value - Buffer.Position;
                while (total > 0)
                {
                    Int32 read = Stream.Read(buffer, 0, (Int32) Math.Min(total, buffer.Length));

                    if (read <= 0)
                    {
                        break;
                    }

                    Buffer.Write(buffer, 0, read);

                    total -= read;
                }
            }
        }

        public override Boolean CanRead
        {
            get
            {
                return true;
            }
        }

        public override Boolean CanSeek
        {
            get
            {
                return true;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return false;
            }
        }

        public SeekableStream(Stream stream)
            : this(stream ?? throw new ArgumentNullException(nameof(stream)), new RecyclableMemoryStream())
        {
        }

        public SeekableStream(Stream stream, Stream buffer)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            Buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));

            if (!buffer.CanSeek)
            {
                throw new NotSupportedException("Buffer stream must be seekable");
            }
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            if (Stream.CanSeek)
            {
                return Stream.Seek(offset, origin);
            }

            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    return Position;
                case SeekOrigin.Current:
                    Position += offset;
                    return Position;
                case SeekOrigin.End:
                {
                    Buffer.Seek(0, SeekOrigin.End);

                    Byte[] buffer = new Byte[4096];
                    while (true)
                    {
                        Int32 read = Stream.Read(buffer, 0, buffer.Length);

                        if (read <= 0)
                        {
                            break;
                        }

                        Buffer.Write(buffer, 0, read);
                    }

                    Position = Buffer.Length - offset;
                    return Position;
                }
                default:
                    throw new NotSupportedException("Not supported SeekOrigin");
            }
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            if (Stream.CanSeek)
            {
                return Stream.Read(buffer, offset, count);
            }

            Int32 total = 0;

            if (Buffer.Length > Buffer.Position)
            {
                total = Buffer.Read(buffer, offset, (Int32) Math.Min(Buffer.Length - Buffer.Position, count));

                count -= total;
                offset += total;
            }

            if (count <= 0)
            {
                return total;
            }

            Int32 read = Stream.Read(buffer, offset, count);

            if (read > 0)
            {
                Buffer.Write(buffer, offset, read);
            }

            total += read;

            return total;
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(Int64 value)
        {
            throw new NotSupportedException();
        }

        public override void Close()
        {
            Stream.Close();
            Buffer.Close();
        }

        public override void Flush()
        {
            Buffer.Flush();
        }
    }
}