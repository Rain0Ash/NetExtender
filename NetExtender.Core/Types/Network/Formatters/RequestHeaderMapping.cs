// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network.Formatters
{
    public class RequestHeaderMapping : MediaTypeMapping
    {
        public String Name { get; }
        public String Value { get; }
        public StringComparison Comparison { get; }
        public Boolean IsSubstring { get; }
        
        public RequestHeaderMapping(String name, String value, Boolean substring, String media, StringComparison comparison)
            : base(media)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            IsSubstring = substring;
            Comparison = comparison;
        }

        public RequestHeaderMapping(String name, String value, Boolean substring, MediaTypeHeaderValue media, StringComparison comparison)
            : base(media)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            IsSubstring = substring;
            Comparison = comparison;
        }

        public override Double TryMatchMediaType(HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Boolean Predicate(String value)
            {
                return IsSubstring && value.Contains(Value, Comparison) || value.Equals(Value, Comparison);
            }

            return request.Headers.TryGetValues(Name, out IEnumerable<String?>? values) && values.WhereNotNull().Any(Predicate) ? 1 : 0;
        }
    }
}