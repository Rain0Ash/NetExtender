// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using NetExtender.Types.Network.Formatters.Interfaces;
using NetExtender.Utilities.Network.Formatters;
using NetExtender.Utilities.Types;
using Newtonsoft.Json.Linq;

namespace NetExtender.Types.Network.Formatters
{
    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    public class MediaTypeFormatterCollection : Collection<MediaTypeFormatter>, IMediaTypeFormatterCollection, IReadOnlyMediaTypeFormatterCollection
    {
        private MediaTypeFormatter[]? _formatters;
        internal MediaTypeFormatter[] WritingFormatters
        {
            get
            {
                return _formatters ??= Items.WhereNotNull(static formatter => formatter.CanWriteAnyTypes).ToArray();
            }
        }

        public event EventHandler? Changed;

        public XmlMediaTypeFormatter? XmlFormatter
        {
            get
            {
                return Items.OfType<XmlMediaTypeFormatter>().FirstOrDefault();
            }
        }

        public JsonMediaTypeFormatter? JsonFormatter
        {
            get
            {
                return Items.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            }
        }

        public FormUrlEncodedMediaTypeFormatter? FormUrlEncodedFormatter
        {
            get
            {
                return Items.OfType<FormUrlEncodedMediaTypeFormatter>().FirstOrDefault();
            }
        }

        public MediaTypeFormatterCollection()
            : this((IEnumerable<MediaTypeFormatter?>?) null)
        {
        }

        public MediaTypeFormatterCollection(MediaTypeFormatter formatter)
        {
            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            Add(formatter);
            Changed += OnChanged;
        }

        public MediaTypeFormatterCollection(params MediaTypeFormatter?[]? formatters)
            : this((IEnumerable<MediaTypeFormatter?>?) formatters)
        {
        }

        public MediaTypeFormatterCollection(IEnumerable<MediaTypeFormatter?>? formatters)
        {
            formatters ??= new MediaTypeFormatter[]
            {
                new JsonMediaTypeFormatter(),
                new XmlMediaTypeFormatter(),
                new FormUrlEncodedMediaTypeFormatter()
            };
            
            AddRange(formatters);
            Changed += OnChanged;
        }

        protected virtual void OnChanged(Object? sender, EventArgs args)
        {
            _formatters = null;
        }

        public static Boolean IsTypeExcludedFromValidation(Type type)
        {
            return typeof(XmlNode).IsAssignableFrom(type) ||
                   typeof(FormDataCollection).IsAssignableFrom(type) ||
                   typeof(JToken).IsAssignableFrom(type) ||
                   typeof(XObject).IsAssignableFrom(type) ||
                   typeof(Type).IsAssignableFrom(type) ||
                   type == typeof(Byte[]);
        }
        
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public Boolean Contains(MediaTypeHeader media)
        {
            if (media.IsEmpty)
            {
                return false;
            }
            
            for (Int32 i = 0; i < Items.Count; i++)
            {
                if (Items[i] is { } formatter && formatter.Contains(media))
                {
                    return true;
                }
            }
            
            return false;
        }
        
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public Boolean Contains(MediaTypeHeader media, Encoding encoding)
        {
            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }
            
            if (media.IsEmpty)
            {
                return false;
            }
            
            for (Int32 i = 0; i < Items.Count; i++)
            {
                if (Items[i] is { } formatter && formatter.Contains(media, encoding))
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public MediaTypeFormatter? FindReader(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return FindReaders(type).FirstOrDefault();
        }
        
        IMediaTypeFormatter? IMediaTypeFormatterCollection.FindReader(Type type)
        {
            return FindReader(type);
        }
        
        IReadOnlyMediaTypeFormatter? IReadOnlyMediaTypeFormatterCollection.FindReader(Type type)
        {
            return FindReader(type);
        }
        
        public MediaTypeFormatter? FindReader(Type type, MediaTypeHeader media)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return FindReaders(type, media).FirstOrDefault();
        }

        IMediaTypeFormatter? IMediaTypeFormatterCollection.FindReader(Type type, MediaTypeHeader media)
        {
            return FindReader(type, media);
        }
        
        IReadOnlyMediaTypeFormatter? IReadOnlyMediaTypeFormatterCollection.FindReader(Type type, MediaTypeHeader media)
        {
            return FindReader(type, media);
        }
        
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public IEnumerable<MediaTypeFormatter> FindReaders(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            for (Int32 i = 0; i < Items.Count; i++)
            {
                if (Items[i] is not { } reader || !reader.CanReadType(type))
                {
                    continue;
                }
                
                yield return reader;
            }
        }
        
        IEnumerable<IMediaTypeFormatter> IMediaTypeFormatterCollection.FindReaders(Type type)
        {
            return FindReaders(type);
        }
        
        IEnumerable<IReadOnlyMediaTypeFormatter> IReadOnlyMediaTypeFormatterCollection.FindReaders(Type type)
        {
            return FindReaders(type);
        }
        
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public IEnumerable<MediaTypeFormatter> FindReaders(Type type, MediaTypeHeader media)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (media.IsEmpty)
            {
                yield break;
            }
            
            for (Int32 i = 0; i < Items.Count; i++)
            {
                if (Items[i] is not { } reader || !reader.CanReadType(type))
                {
                    continue;
                }

                foreach (MediaTypeHeader header in reader.SupportedMediaType)
                {
                    if (header is { Value: { } value } && value.IsSubsetOf(media.Value))
                    {
                        yield return reader;
                    }
                }
            }
        }

        IEnumerable<IMediaTypeFormatter> IMediaTypeFormatterCollection.FindReaders(Type type, MediaTypeHeader media)
        {
            return FindReaders(type, media);
        }
        
        IEnumerable<IReadOnlyMediaTypeFormatter> IReadOnlyMediaTypeFormatterCollection.FindReaders(Type type, MediaTypeHeader media)
        {
            return FindReaders(type, media);
        }
        
        public MediaTypeFormatter? FindWriter(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return FindWriters(type).FirstOrDefault();
        }
        
        IMediaTypeFormatter? IMediaTypeFormatterCollection.FindWriter(Type type)
        {
            return FindWriter(type);
        }
        
        IReadOnlyMediaTypeFormatter? IReadOnlyMediaTypeFormatterCollection.FindWriter(Type type)
        {
            return FindWriter(type);
        }
        
        public MediaTypeFormatter? FindWriter(Type type, MediaTypeHeader media)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return FindWriters(type, media).FirstOrDefault();
        }
        
        IMediaTypeFormatter? IMediaTypeFormatterCollection.FindWriter(Type type, MediaTypeHeader media)
        {
            return FindWriter(type, media);
        }
        
        IReadOnlyMediaTypeFormatter? IReadOnlyMediaTypeFormatterCollection.FindWriter(Type type, MediaTypeHeader media)
        {
            return FindWriter(type, media);
        }
        
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public IEnumerable<MediaTypeFormatter> FindWriters(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            for (Int32 i = 0; i < Items.Count; i++)
            {
                if (Items[i] is not { } writer || !writer.CanWriteType(type))
                {
                    continue;
                }
                
                yield return writer;
            }
        }
        
        IEnumerable<IMediaTypeFormatter> IMediaTypeFormatterCollection.FindWriters(Type type)
        {
            return FindWriters(type);
        }
        
        IEnumerable<IReadOnlyMediaTypeFormatter> IReadOnlyMediaTypeFormatterCollection.FindWriters(Type type)
        {
            return FindWriters(type);
        }
        
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public IEnumerable<MediaTypeFormatter> FindWriters(Type type, MediaTypeHeader media)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (media.IsEmpty)
            {
                yield break;
            }
            
            for (Int32 i = 0; i < Items.Count; i++)
            {
                if (Items[i] is not { } writer || !writer.CanWriteType(type))
                {
                    continue;
                }
                
                foreach (MediaTypeHeader header in writer.SupportedMediaType.Select(static header => header.Value))
                {
                    if (header is { Value: { } value } && value.IsSubsetOf(media.Value))
                    {
                        yield return writer;
                    }
                }
            }
        }
        
        IEnumerable<IMediaTypeFormatter> IMediaTypeFormatterCollection.FindWriters(Type type, MediaTypeHeader media)
        {
            return FindWriters(type, media);
        }
        
        IEnumerable<IReadOnlyMediaTypeFormatter> IReadOnlyMediaTypeFormatterCollection.FindWriters(Type type, MediaTypeHeader media)
        {
            return FindWriters(type, media);
        }

        protected override void SetItem(Int32 index, MediaTypeFormatter item)
        {
            base.SetItem(index, item);
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void AddRange(IEnumerable<MediaTypeFormatter?> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (MediaTypeFormatter? formatter in items)
            {
                if (formatter is null)
                {
                    continue;
                }
                
                Add(formatter);
            }
        }

        protected override void InsertItem(Int32 index, MediaTypeFormatter item)
        {
            base.InsertItem(index, item);
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void InsertRange(Int32 index, IEnumerable<MediaTypeFormatter> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (MediaTypeFormatter formatter in items)
            {
                Insert(index++, formatter);
            }
        }

        protected override void RemoveItem(Int32 index)
        {
            base.RemoveItem(index);
            Changed?.Invoke(this, EventArgs.Empty);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            Changed?.Invoke(this, EventArgs.Empty);
        }
        
        IEnumerator<IMediaTypeFormatter> IEnumerable<IMediaTypeFormatter>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<IReadOnlyMediaTypeFormatter> IEnumerable<IReadOnlyMediaTypeFormatter>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}