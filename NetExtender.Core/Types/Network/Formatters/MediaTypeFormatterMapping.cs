// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Network.Formatters
{
    public abstract class MediaTypeFormatterMapping
    {
        public MediaTypeHeaderValue MediaType { get; private set; }

        protected MediaTypeFormatterMapping(String media)
            : this(!String.IsNullOrWhiteSpace(media) ? new MediaTypeHeaderValue(media) : throw new ArgumentNullOrWhiteSpaceStringException(media, nameof(media)))
        {
        }

        protected MediaTypeFormatterMapping(MediaTypeHeaderValue media)
        {
            MediaType = media ?? throw new ArgumentNullException(nameof(media));
        }

        public abstract Double TryMatchMediaType(HttpRequestMessage request);
    }
}