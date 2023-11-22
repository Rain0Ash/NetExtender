// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NetExtender.Types.Network.Formatters
{
    public abstract class MediaTypeMapping
    {
        public MediaTypeHeaderValue MediaType { get; }

        protected MediaTypeMapping(String media)
        {
            if (String.IsNullOrWhiteSpace(media))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(media));
            }

            MediaType = new MediaTypeHeaderValue(media);
        }

        protected MediaTypeMapping(MediaTypeHeaderValue media)
        {
            MediaType = media ?? throw new ArgumentNullException(nameof(media));
        }

        public abstract Double TryMatchMediaType(HttpRequestMessage request);
    }
}