// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Net.Http;
using NetExtender.Utilities.Network.Formatters;

namespace NetExtender.Types.Network.Formatters
{
    public class XmlRequestHeaderMapping : RequestHeaderMapping
    {
        public XmlRequestHeaderMapping()
            : base("x-requested-with", "XMLHttpRequest", true, MediaTypeFormatterUtilities.ApplicationJsonMediaType, StringComparison.OrdinalIgnoreCase)
        {
        }

        public override Double TryMatchMediaType(HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers.Accept.Count switch
            {
                0 => base.TryMatchMediaType(request),
                1 => request.Headers.Accept.First().MediaType?.Equals("*/*", StringComparison.Ordinal) == true ? base.TryMatchMediaType(request) : 0,
                _ => 0
            };
        }
    }
}