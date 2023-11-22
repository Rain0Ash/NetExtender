// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Xml;
using System.Xml.Linq;
using NetExtender.Utilities.Network.Formatters;
using NetExtender.Utilities.Types;
using Newtonsoft.Json.Linq;

namespace NetExtender.Types.Network.Formatters
{
    public class MediaTypeFormatterCollection : Collection<MediaTypeFormatter>
    {
        private MediaTypeFormatter[]? _formatters;
        internal MediaTypeFormatter[] WritingFormatters
        {
            get
            {
                return _formatters ??= Items.WhereNotNull(formatter => formatter.CanWriteAnyTypes).ToArray();
            }
        }

        public event EventHandler Changed;

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
            : this(null)
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
            
            foreach (MediaTypeFormatter? formatter in formatters)
            {
                if (formatter is null)
                {
                    continue;
                }

                Add(formatter);
            }

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

        public MediaTypeFormatter? FindReader(Type type, MediaTypeHeaderValue media)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (media is null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            return Items.WhereNotNull(reader => reader.CanReadType(type)).FirstOrDefault(reader => reader.SupportedMediaType.WhereNotNull().Any(value => value.IsSubsetOf(media)));
        }

        public MediaTypeFormatter? FindWriter(Type type, MediaTypeHeaderValue media)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (media is null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            return Items.WhereNotNull(writer => writer.CanWriteType(type)).FirstOrDefault(writer => writer.SupportedMediaType.WhereNotNull().Any(value => value.IsSubsetOf(media)));
        }

        protected override void SetItem(Int32 index, MediaTypeFormatter item)
        {
            base.SetItem(index, item);
            Changed.Invoke(this, EventArgs.Empty);
        }

        public void AddRange(IEnumerable<MediaTypeFormatter> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (MediaTypeFormatter formatter in items)
            {
                Add(formatter);
            }
        }

        protected override void InsertItem(Int32 index, MediaTypeFormatter item)
        {
            base.InsertItem(index, item);
            Changed.Invoke(this, EventArgs.Empty);
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
            Changed.Invoke(this, EventArgs.Empty);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            Changed.Invoke(this, EventArgs.Empty);
        }
    }
}