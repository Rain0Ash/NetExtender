using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Types.Files;
using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Lists;
using Newtonsoft.Json;

namespace NetExtender.CQRS
{
    public abstract record MultiFileIdEntityCQRS<T> : MultiFileEntityCQRS, IIdEntityCQRS<T>
    {
        public T Id { get; init; }

        protected MultiFileIdEntityCQRS()
        {
            Id = default!;
        }

        protected MultiFileIdEntityCQRS(T id)
        {
            Id = id;
        }
    }

    public abstract record MultiFileEntityCQRS : EntityCQRS, IReadOnlyList<UploadFormFile>, IDisposable
    {
        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean HasInput
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Files is not null;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Boolean HasFiles
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Files is { Count: > 0 };
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Int32 Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Files?.Count ?? 0;
            }
        }

        [Microsoft.AspNetCore.Mvc.FromForm]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual IFormFileCollection? Files { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UploadFormFile GetFile(String name)
        {
            return UploadFormFile.From(Files?.GetFile(name));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<UploadFormFile> GetFiles(String name)
        {
            return Files is not null ? new ReadOnlySelectorListWrapper<IFormFile, UploadFormFile>(Files.GetFiles(name), UploadFormFile.From) : Array.Empty<UploadFormFile>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<UploadFormFile> GetEnumerator()
        {
            if (Files is null)
            {
                yield break;
            }

            foreach (IFormFile file in Files)
            {
                yield return UploadFormFile.From(file);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Files = null;
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public UploadFormFile this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Files is not null ? UploadFormFile.From(Files[index]) : throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public UploadFormFile this[String name]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return UploadFormFile.From(Files?[name]);
            }
        }
    }
}