using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Identity;
using NetExtender.Types.Monads;

namespace NetExtender.AspNetCore.Types.Files
{
    public readonly struct UploadFormFile : IStruct<UploadFormFile>
    {
        internal IFormFile? File { get; private init; }

        /// <inheritdoc cref="IFormFile.Name" />
        public String Name
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return File is not null ? File.Name : throw new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }
        }

        /// <inheritdoc cref="IFormFile.FileName" />
        public String FileName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return File is not null ? File.FileName : throw new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }
        }

        /// <inheritdoc cref="IFormFile.Length" />
        public Int64 Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return File?.Length ?? throw new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }
        }

        /// <inheritdoc cref="IFormFile.ContentType" />
        public String ContentType
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return File is not null ? File.ContentType : throw new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }
        }

        /// <inheritdoc cref="IFormFile.ContentDisposition" />
        public String ContentDisposition
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return File is not null ? File.ContentDisposition : throw new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }
        }

        /// <inheritdoc cref="IFormFile.Headers" />
        public IHeaderDictionary Headers
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return File?.Headers ?? throw new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }
        }

        public Int64? MaximumFileSize { get; init; } = null;
        public Int32 BufferSize { get; init; } = -1;

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return File is null;
            }
        }

        public UploadFormFile(IFormFile file)
        {
            File = file ?? throw new ArgumentNullException(nameof(file));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UploadFormFile From(IFormFile? file)
        {
            return From(file, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UploadFormFile From(IFormFile? file, Int64? maximum)
        {
            return new UploadFormFile { File = file, MaximumFileSize = maximum };
        }

        /// <inheritdoc cref="IFormFile.OpenReadStream" />
        public Result<Stream> OpenReadStream()
        {
            if (File is null)
            {
                return new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }

            Int64 length = Length;

            if (length > MaximumFileSize)
            {
                return new UploadFileTooLongException { FileName = FileName, FileSize = length, MaximumFileSize = MaximumFileSize };
            }

            try
            {
                return File.OpenReadStream();
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<StreamReader> OpenStreamReader()
        {
            return OpenStreamReader(null);
        }

        public Result<StreamReader> OpenStreamReader(Encoding? encoding)
        {
            if (File is null)
            {
                return new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }

            Int64 length = Length;

            if (length > MaximumFileSize)
            {
                return new UploadFileTooLongException { FileName = FileName, FileSize = length, MaximumFileSize = MaximumFileSize };
            }

            try
            {
                Stream stream = File.OpenReadStream();
                return new StreamReader(stream, encoding ?? Encoding.UTF8, true, BufferSize, false);
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        public Result<Int32> Read(Span<Byte> buffer)
        {
            if (File is null)
            {
                return new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }

            try
            {
                using Stream stream = File.OpenReadStream();
                Int32 read, result = 0;

                while (result < buffer.Length && (read = stream.Read(buffer.Slice(result))) > 0)
                {
                    result += read;
                }

                return result;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        public Async<Int32> ReadAsync(Memory<Byte> buffer)
        {
            return ReadAsync(buffer, CancellationToken.None);
        }

        public async Async<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken token)
        {
            if (File is null)
            {
                throw new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }

            await using Stream stream = File.OpenReadStream();
            Int32 read, result = 0;

            while (result < buffer.Length && (read = await stream.ReadAsync(buffer.Slice(result), token)) > 0)
            {
                result += read;
            }

            return result;
        }

        public Result<Byte[]> Read()
        {
            if (File is null)
            {
                return new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }

            Int64 length = Length;

            if (length > Int32.MaxValue || length > MaximumFileSize)
            {
                return new UploadFileTooLongException { FileName = FileName, FileSize = length, MaximumFileSize = MaximumFileSize };
            }

            try
            {
                using Stream stream = File.OpenReadStream();
                using MemoryStream memory = new MemoryStream((Int32) length);

                stream.CopyTo(memory);
                return memory.ToArray();
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async<Byte[]> ReadAsync()
        {
            return ReadAsync(CancellationToken.None);
        }

        public async Async<Byte[]> ReadAsync(CancellationToken token)
        {
            if (File is null)
            {
                throw new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }

            token.ThrowIfCancellationRequested();
            Int64 length = Length;

            if (length > Int32.MaxValue || length > MaximumFileSize)
            {
                throw new UploadFileTooLongException { FileName = FileName, FileSize = length, MaximumFileSize = MaximumFileSize };
            }

            await using Stream stream = File.OpenReadStream();
            using MemoryStream memory = new MemoryStream((Int32) length);

            await stream.CopyToAsync(memory, token);
            return memory.ToArray();
        }

        public Result<String> Read(Encoding? encoding)
        {
            if (File is null)
            {
                return new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }

            if (Length > MaximumFileSize)
            {
                return new UploadFileTooLongException { FileName = FileName, FileSize = Length, MaximumFileSize = MaximumFileSize };
            }

            try
            {
                using Stream stream = File.OpenReadStream();
                using StreamReader reader = new StreamReader(stream, encoding ?? Encoding.UTF8, true, BufferSize, false);

                return reader.ReadToEnd();
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async<String> ReadAsync(Encoding? encoding)
        {
            return ReadAsync(encoding, CancellationToken.None);
        }

        public async Async<String> ReadAsync(Encoding? encoding, CancellationToken token)
        {
            if (File is null)
            {
                throw new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }

            token.ThrowIfCancellationRequested();

            if (Length > MaximumFileSize)
            {
                throw new UploadFileTooLongException { FileName = FileName, FileSize = Length, MaximumFileSize = MaximumFileSize };
            }

            await using Stream stream = File.OpenReadStream();
            using StreamReader reader = new StreamReader(stream, encoding ?? Encoding.UTF8, true, BufferSize, false);

            return await reader.ReadToEndAsync();
        }

        /// <inheritdoc cref="IFormFile.CopyTo" />
        public Exception? CopyTo(Stream stream)
        {
            if (File is null)
            {
                return new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            try
            {
                File.CopyTo(stream);
                return null;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        /// <inheritdoc cref="IFormFile.CopyToAsync" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async CopyToAsync(Stream stream)
        {
            return CopyToAsync(stream, CancellationToken.None);
        }

        /// <inheritdoc cref="IFormFile.CopyToAsync" />
        public async Async CopyToAsync(Stream stream, CancellationToken token)
        {
            if (File is null)
            {
                throw new UploadFileNotProvidedException { MaximumFileSize = MaximumFileSize };
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            await File.CopyToAsync(stream, token);
        }
    }
}