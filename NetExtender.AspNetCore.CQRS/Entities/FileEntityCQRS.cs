using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Types.Files;
using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.CQRS
{
    public abstract record FileIdEntityCQRS<T> : FileEntityCQRS, IIdEntityCQRS<T>
    {
        public T Id { get; init; }

        protected FileIdEntityCQRS()
        {
            Id = default!;
        }

        protected FileIdEntityCQRS(T id)
        {
            Id = id;
        }
    }

    public abstract record FileEntityCQRS : EntityCQRS, IDisposable
    {
        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean HasFile
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _file.IsEmpty;
            }
        }

        private UploadFormFile _file;

        [Microsoft.AspNetCore.Mvc.FromForm]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual IFormFile? File
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _file.File;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                _file = UploadFormFile.From(value);
            }
        }

        /// <inheritdoc cref="IFormFile.Name" />
        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public String Name
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _file.Name;
            }
        }

        /// <inheritdoc cref="IFormFile.FileName" />
        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public String FileName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _file.FileName;
            }
        }

        /// <inheritdoc cref="IFormFile.Length" />
        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Int64 Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _file.Length;
            }
        }

        /// <inheritdoc cref="IFormFile.ContentType" />
        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public String ContentType
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _file.ContentType;
            }
        }

        /// <inheritdoc cref="IFormFile.ContentDisposition" />
        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public String ContentDisposition
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _file.ContentDisposition;
            }
        }

        /// <inheritdoc cref="IFormFile.Headers" />
        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public IHeaderDictionary Headers
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _file.Headers;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Int64? MaximumFileSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _file.MaximumFileSize;
            }
        }

        /// <inheritdoc cref="IFormFile.OpenReadStream" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<Stream> OpenReadStream()
        {
            return _file.OpenReadStream();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<StreamReader> OpenStreamReader()
        {
            return _file.OpenStreamReader();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<StreamReader> OpenStreamReader(Encoding? encoding)
        {
            return _file.OpenStreamReader(encoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<Int32> Read(Span<Byte> buffer)
        {
            return _file.Read(buffer);
        }

        public Result<Int32> Read(Span<Byte> buffer, Boolean forget)
        {
            Result<Int32> result = Read(buffer);

            if (result.Unwrap(out _) && forget)
            {
                Dispose();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async<Int32> ReadAsync(Memory<Byte> buffer)
        {
            return _file.ReadAsync(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken token)
        {
            return _file.ReadAsync(buffer, token);
        }

        public async Async<Int32> ReadAsync(Memory<Byte> buffer, Boolean forget)
        {
            Int32 result = await ReadAsync(buffer).Throw();

            if (forget)
            {
                Dispose();
            }

            return result;
        }

        public async Async<Int32> ReadAsync(Memory<Byte> buffer, Boolean forget, CancellationToken token)
        {
            Int32 result = await ReadAsync(buffer, token).Throw();

            if (forget)
            {
                Dispose();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<Byte[]> Read()
        {
            return _file.Read();
        }

        public Result<Byte[]> Read(Boolean forget)
        {
            Result<Byte[]> result = Read();

            if (result.Unwrap(out _) && forget)
            {
                Dispose();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async<Byte[]> ReadAsync()
        {
            return _file.ReadAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async<Byte[]> ReadAsync(CancellationToken token)
        {
            return _file.ReadAsync(token);
        }

        public async Async<Byte[]> ReadAsync(Boolean forget)
        {
            Byte[] result = await ReadAsync().Throw();

            if (forget)
            {
                Dispose();
            }

            return result;
        }

        public async Async<Byte[]> ReadAsync(Boolean forget, CancellationToken token)
        {
            Byte[] result = await ReadAsync(token).Throw();

            if (forget)
            {
                Dispose();
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String Read(Encoding? encoding)
        {
            return _file.Read(encoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async<String> ReadAsync(Encoding? encoding)
        {
            return _file.ReadAsync(encoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async<String> ReadAsync(Encoding? encoding, CancellationToken token)
        {
            return _file.ReadAsync(encoding, token);
        }

        public async Async<String> ReadAsync(Encoding? encoding, Boolean forget)
        {
            String result = await ReadAsync(encoding).Throw();

            if (forget)
            {
                Dispose();
            }

            return result;
        }

        public async Async<String> ReadAsync(Encoding? encoding, Boolean forget, CancellationToken token)
        {
            String result = await ReadAsync(encoding, token).Throw();

            if (forget)
            {
                Dispose();
            }

            return result;
        }

        /// <inheritdoc cref="IFormFile.CopyTo" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Exception? CopyTo(Stream target)
        {
            return _file.CopyTo(target);
        }

        /// <inheritdoc cref="IFormFile.CopyTo" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Exception? CopyTo(Stream target, Boolean forget)
        {
            if (CopyTo(target) is { } exception)
            {
                return exception;
            }

            if (forget)
            {
                Dispose();
            }

            return null;
        }

        /// <inheritdoc cref="IFormFile.CopyToAsync" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async CopyToAsync(Stream target)
        {
            return _file.CopyToAsync(target);
        }

        /// <inheritdoc cref="IFormFile.CopyToAsync" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Async CopyToAsync(Stream target, CancellationToken token)
        {
            return _file.CopyToAsync(target, token);
        }

        /// <inheritdoc cref="IFormFile.CopyToAsync" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Async CopyToAsync(Stream target, Boolean forget)
        {
            await CopyToAsync(target).Throw();

            if (forget)
            {
                Dispose();
            }
        }

        /// <inheritdoc cref="IFormFile.CopyToAsync" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Async CopyToAsync(Stream target, Boolean forget, CancellationToken token)
        {
            await CopyToAsync(target, token).Throw();

            if (forget)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            _file = default;
        }
    }
}