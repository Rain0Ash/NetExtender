// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace NetExtender.Types.Network
{
    public class FormDataCollection : IReadOnlyCollection<KeyValuePair<String, String>>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator NameValueCollection?(FormDataCollection? value)
        {
            return value?.Collection;
        }
        
        private KeyValuePair<String, String>[] Pairs { get; }
        
        private NameValueCollection? _collection;
        public NameValueCollection Collection
        {
            get
            {
                return _collection ??= HttpValueCollection.Create(this);
            }
        }

        public Int32 Count
        {
            get
            {
                return _collection?.Count ?? 0;
            }
        }

        public FormDataCollection(IEnumerable<KeyValuePair<String, String>> pairs)
        {
            if (pairs is null)
            {
                throw new ArgumentNullException(nameof(pairs));
            }

            Pairs = pairs.ToArray();
        }

        public FormDataCollection(Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            String query = uri.Query;
            if (query.Length > 0 && query[0] == '?')
            {
                query = query.Substring(1);
            }

            Pairs = ParseQueryString(query);
        }

        public FormDataCollection(String? query)
        {
            Pairs = query is not null ? ParseQueryString(query) : Array.Empty<KeyValuePair<String, String>>();
        }

        private static KeyValuePair<String, String>[] ParseQueryString(String? query)
        {
            if (String.IsNullOrWhiteSpace(query))
            {
                return Array.Empty<KeyValuePair<String, String>>();
            }

            List<KeyValuePair<String, String>> pairs = new List<KeyValuePair<String, String>>();
            
            Byte[] bytes = Encoding.UTF8.GetBytes(query);
            FormUrlEncodedParser parser = new FormUrlEncodedParser(pairs, Int64.MaxValue);
            
            Int32 read = 0;
            Int32 length = bytes.Length;
            
            if (parser.ParseBuffer(bytes, length, ref read, true) != HttpParserState.Done)
            {
                throw new InvalidOperationException($"Error parsing HTML form URL-encoded data, byte {read}.");
            }

            return pairs.ToArray();
        }

        public String? Get(String? key)
        {
            return Collection.Get(key);
        }

        public String[]? GetValues(String? key)
        {
            return Collection.GetValues(key);
        }

        public IEnumerator GetEnumerator()
        {
            return Pairs.GetEnumerator();
        }

        IEnumerator<KeyValuePair<String, String>> IEnumerable<KeyValuePair<String, String>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<String, String>>) Pairs).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Pairs.GetEnumerator();
        }

        public String? this[String? key]
        {
            get
            {
                return Get(key);
            }
        }

        public KeyValuePair<String, String> this[Int32 index]
        {
            get
            {
                return Pairs[index];
            }
        }
    }
}