// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Network.Formatters
{
    public class QueryStringMapping : MediaTypeMapping
    {
        public String Name { get; }
        public String Value { get; }

        public QueryStringMapping(String name, String value, String media)
            : base(media)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(name, nameof(name));
            }

            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(value, nameof(value));
            }

            Name = name.Trim();
            Value = value.Trim();
        }

        public QueryStringMapping(String name, String value, MediaTypeHeaderValue media)
            : base(media)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(name, nameof(name));
            }

            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(value, nameof(value));
            }

            Name = name.Trim();
            Value = value.Trim();
        }

        public override Double TryMatchMediaType(HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.RequestUri is not { } uri)
            {
                throw new InvalidOperationException($"A non-null request {nameof(Uri)} must be provided to determine if a '{GetType()}' matches a given request or response message.");
            }

            NameValueCollection query = new FormDataCollection(uri).Collection;
            return DoesQueryStringMatch(query) ? 1.0 : 0.0;
        }

        protected virtual Boolean DoesQueryStringMatch(NameValueCollection? query)
        {
            Boolean Predicate(String? key)
            {
                return String.Equals(key, Name, StringComparison.OrdinalIgnoreCase) && String.Equals(query[key], Value, StringComparison.OrdinalIgnoreCase);
            }

            return query is not null && query.AllKeys.Any(Predicate);
        }
    }
}