// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace NetExtender.Types.Network
{
    [Serializable]
    internal class HttpValueCollection : NameValueCollection
    {
        private Int32 _maximumHttpCollectionKeys = Int32.MaxValue;
        public Int32 MaximumHttpCollectionKeys
        {
            get
            {
                return _maximumHttpCollectionKeys;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }

                _maximumHttpCollectionKeys = value;
            }
        }

        protected HttpValueCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private HttpValueCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        internal static HttpValueCollection Create()
        {
            return new HttpValueCollection();
        }

        internal static HttpValueCollection Create(IEnumerable<KeyValuePair<String, String>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            HttpValueCollection collection = new HttpValueCollection();
            
            foreach (KeyValuePair<String, String> pair in source)
            {
                collection.Add(pair.Key, pair.Value);
            }

            collection.IsReadOnly = true;
            return collection;
        }

        public override void Add(String? name, String? value)
        {
            if (Count >= MaximumHttpCollectionKeys)
            {
                throw new InvalidOperationException($"The number of keys in a {GetType()} has exceeded the limit of '{MaximumHttpCollectionKeys}'.");
            }
            
            base.Add(name ?? String.Empty, value ?? String.Empty);
        }

        protected virtual void AppendNameValuePair(StringBuilder builder, ref Boolean first, Boolean encode, String? name, String? value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            name = (encode ? WebUtility.UrlEncode(name) : name) ?? String.Empty;
            value ??= (encode ? WebUtility.UrlEncode(value) : value) ?? String.Empty;

            if (!first)
            {
                builder.Append('&');
            }
            
            first = false;
            builder.Append(name);
            if (String.IsNullOrEmpty(value))
            {
                return;
            }

            builder.Append('=');
            builder.Append(value);
        }

        public override String ToString()
        {
            return ToString(true);
        }

        protected virtual String ToString(Boolean encode)
        {
            if (Count <= 0)
            {
                return String.Empty;
            }

            Boolean first = true;
            StringBuilder builder = new StringBuilder();
            foreach (String name in this)
            {
                String[]? values = GetValues(name);
                if (values is not { Length: > 0 })
                {
                    AppendNameValuePair(builder, ref first, encode, name, String.Empty);
                    continue;
                }

                foreach (String value in values)
                {
                    AppendNameValuePair(builder, ref first, encode, name, value);
                }
            }
            
            return builder.ToString();
        }
    }
}