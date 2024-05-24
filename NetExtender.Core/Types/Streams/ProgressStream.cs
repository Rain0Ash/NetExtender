// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Streams
{
    public enum ProgressStreamInfoType : Byte
    {
        Unknown,
        Read,
        Write
    }

    public readonly struct ProgressStreamInfo : IEquatable<ProgressStreamInfo>
    {
        public static Boolean operator ==(ProgressStreamInfo left, ProgressStreamInfo right)
        {
            return left.Equals(right);
        }

        public static Boolean operator !=(ProgressStreamInfo left, ProgressStreamInfo right)
        {
            return !(left == right);
        }

        public ProgressStreamInfoType Type { get; }
        public Int64 Offset { get; }
        public Int64 Count { get; }

        public ProgressStreamInfo(ProgressStreamInfoType type, Int64 offset, Int64 count)
        {
            Offset = offset;
            Count = count;
            Type = type;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Type, Offset, Count);
        }

        public override Boolean Equals(Object? other)
        {
            return other is ProgressStreamInfo info && Equals(info);
        }

        public Boolean Equals(ProgressStreamInfo other)
        {
            return Type == other.Type && Offset == other.Offset && Count == other.Count;
        }

        public override String ToString()
        {
            return $"{Type} {Offset} {Count}";
        }
    }

    public class ProgressStream : Stream
    {
        protected Stream Internal { get; }

        public override Boolean CanRead
        {
            get
            {
                return Internal.CanRead;
            }
        }

        public override Boolean CanSeek
        {
            get
            {
                return Internal.CanSeek;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return Internal.CanWrite;
            }
        }

        public override Int64 Length
        {
            get
            {
                return Internal.Length;
            }
        }

        public override Int64 Position
        {
            get
            {
                return Internal.Position;
            }
            set
            {
                Internal.Position = value;
            }
        }

        public override Boolean CanTimeout
        {
            get
            {
                return Internal.CanTimeout;
            }
        }

        public override Int32 ReadTimeout
        {
            get
            {
                return Internal.ReadTimeout;
            }
            set
            {
                Internal.ReadTimeout = value;
            }
        }

        public override Int32 WriteTimeout
        {
            get
            {
                return Internal.WriteTimeout;
            }
            set
            {
                Internal.WriteTimeout = value;
            }
        }

        private IProgress<ProgressStreamInfo> Progress { get; }

        public ProgressStream(Stream stream, IProgress<ProgressStreamInfo> progress)
        {
            Internal = stream ?? throw new ArgumentNullException(nameof(stream));
            Progress = progress ?? throw new ArgumentNullException(nameof(progress));
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            return Internal.Seek(offset, origin);
        }

        public override void SetLength(Int64 value)
        {
            Internal.SetLength(value);
        }

        protected virtual void OnReadProgress(Int32 count)
        {
            if (!this.TryPosition(out Int64 position))
            {
                position = -1;
            }

            Int64 offset = position - count;
            Progress.Report(new ProgressStreamInfo(ProgressStreamInfoType.Read, offset >= 0 ? offset : -1, count));
        }

        public override Int32 ReadByte()
        {
            Int32 value = Internal.ReadByte();
            OnReadProgress(1);
            return value;
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            Int32 count = Internal.Read(buffer);
            OnReadProgress(count);
            return count;
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            count = Internal.Read(buffer, offset, count);
            OnReadProgress(count);
            return count;
        }

        public override async ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken token = default)
        {
            Int32 count = await Internal.ReadAsync(buffer, token).ConfigureAwait(false);
            OnReadProgress(count);
            return count;
        }

        public override async Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            count = await Internal.ReadAsync(buffer, offset, count, token).ConfigureAwait(false);
            OnReadProgress(count);
            return count;
        }

        protected virtual void OnWriteProgress(Int32 count)
        {
            if (!this.TryPosition(out Int64 position))
            {
                position = -1;
            }

            Int64 offset = position - count;
            Progress.Report(new ProgressStreamInfo(ProgressStreamInfoType.Write, offset >= 0 ? offset : -1, count));
        }

        public override void WriteByte(Byte value)
        {
            Internal.WriteByte(value);
            OnWriteProgress(1);
        }

        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            Internal.Write(buffer);
            OnWriteProgress(buffer.Length);
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            Internal.Write(buffer, offset, count);
            OnWriteProgress(buffer.AsSpan(offset, count).Length);
        }

        public override async ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken token = default)
        {
            await Internal.WriteAsync(buffer, token).ConfigureAwait(false);
            OnWriteProgress(buffer.Length);
        }

        public override async Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            await Internal.WriteAsync(buffer, offset, count, token).ConfigureAwait(false);
            OnWriteProgress(buffer.AsSpan(offset, count).Length);
        }

        public override void Flush()
        {
            Internal.Flush();
        }

        public override Task FlushAsync(CancellationToken token)
        {
            return Internal.FlushAsync(token);
        }

        protected override void Dispose(Boolean disposing)
        {
            Internal.Dispose();
        }
    }
}