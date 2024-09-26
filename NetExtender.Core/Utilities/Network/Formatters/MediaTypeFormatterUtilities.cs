// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Network.Formatters;
using NetExtender.Types.Network.Formatters.Exceptions;
using NetExtender.Types.Network.Formatters.Interfaces;

namespace NetExtender.Utilities.Network.Formatters
{
    public enum MediaTypeHeaderValueRange
    {
        None,
        SubtypeMediaRange,
        AllMediaRange
    }

    public enum MediaTypeHeaderValueType : Byte
    {
        Text,
        TextJson,
        TextXml,
        Json,
        Xml,
        Bson,
        OctetStream,
        FormUrlEncoded
    }
    
    public static class MediaTypeFormatterUtilities
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public static class UTF8
        {
            public static MediaTypeHeaderValue ApplicationOctetStreamMediaType
            {
                get
                {
                    return new MediaTypeHeaderValue("application/octet-stream") { CharSet = "utf-8" };
                }
            }

            public static MediaTypeHeaderValue ApplicationJsonMediaType
            {
                get
                {
                    return new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
                }
            }

            public static MediaTypeHeaderValue ApplicationXmlMediaType
            {
                get
                {
                    return new MediaTypeHeaderValue("application/xml") { CharSet = "utf-8" };
                }
            }

            public static MediaTypeHeaderValue TextPlainMediaType
            {
                get
                {
                    return new MediaTypeHeaderValue("text/plain") { CharSet = "utf-8" };
                }
            }

            public static MediaTypeHeaderValue TextJsonMediaType
            {
                get
                {
                    return new MediaTypeHeaderValue("text/json") { CharSet = "utf-8" };
                }
            }

            public static MediaTypeHeaderValue TextXmlMediaType
            {
                get
                {
                    return new MediaTypeHeaderValue("text/xml") { CharSet = "utf-8" };
                }
            }

            public static MediaTypeHeaderValue ApplicationFormUrlEncodedMediaType
            {
                get
                {
                    return new MediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = "utf-8" };
                }
            }

            public static MediaTypeHeaderValue ApplicationBsonMediaType
            {
                get
                {
                    return new MediaTypeHeaderValue("application/bson") { CharSet = "utf-8" };
                }
            }
        }
        
        public static MediaTypeHeaderValue ApplicationOctetStreamMediaType
        {
            get
            {
                return new MediaTypeHeaderValue("application/octet-stream");
            }
        }

        public static MediaTypeHeaderValue ApplicationJsonMediaType
        {
            get
            {
                return new MediaTypeHeaderValue("application/json");
            }
        }

        public static MediaTypeHeaderValue ApplicationXmlMediaType
        {
            get
            {
                return new MediaTypeHeaderValue("application/xml");
            }
        }

        public static MediaTypeHeaderValue TextPlainMediaType
        {
            get
            {
                return new MediaTypeHeaderValue("text/plain");
            }
        }

        public static MediaTypeHeaderValue TextJsonMediaType
        {
            get
            {
                return new MediaTypeHeaderValue("text/json");
            }
        }

        public static MediaTypeHeaderValue TextXmlMediaType
        {
            get
            {
                return new MediaTypeHeaderValue("text/xml");
            }
        }

        public static MediaTypeHeaderValue ApplicationFormUrlEncodedMediaType
        {
            get
            {
                return new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            }
        }

        public static MediaTypeHeaderValue ApplicationBsonMediaType
        {
            get
            {
                return new MediaTypeHeaderValue("application/bson");
            }
        }

        public static MediaTypeHeaderValue Create(this MediaTypeHeaderValueType type)
        {
            return type switch
            {
                MediaTypeHeaderValueType.Text => TextPlainMediaType,
                MediaTypeHeaderValueType.TextJson => TextJsonMediaType,
                MediaTypeHeaderValueType.TextXml => TextXmlMediaType,
                MediaTypeHeaderValueType.Json => ApplicationJsonMediaType,
                MediaTypeHeaderValueType.Xml => ApplicationXmlMediaType,
                MediaTypeHeaderValueType.Bson => ApplicationBsonMediaType,
                MediaTypeHeaderValueType.OctetStream => ApplicationOctetStreamMediaType,
                MediaTypeHeaderValueType.FormUrlEncoded => ApplicationFormUrlEncodedMediaType,
                _ => throw new EnumUndefinedOrNotSupportedException<MediaTypeHeaderValueType>(type, nameof(type), null)
            };
        }

        public static MediaTypeHeaderValue Create(this MediaTypeHeaderValueType type, Boolean utf8)
        {
            return !utf8 ? Create(type) : type switch
            {
                MediaTypeHeaderValueType.Text => UTF8.TextPlainMediaType,
                MediaTypeHeaderValueType.TextJson => UTF8.TextJsonMediaType,
                MediaTypeHeaderValueType.TextXml => UTF8.TextXmlMediaType,
                MediaTypeHeaderValueType.Json => UTF8.ApplicationJsonMediaType,
                MediaTypeHeaderValueType.Xml => UTF8.ApplicationXmlMediaType,
                MediaTypeHeaderValueType.Bson => UTF8.ApplicationBsonMediaType,
                MediaTypeHeaderValueType.OctetStream => UTF8.ApplicationOctetStreamMediaType,
                MediaTypeHeaderValueType.FormUrlEncoded => UTF8.ApplicationFormUrlEncodedMediaType,
                _ => throw new EnumUndefinedOrNotSupportedException<MediaTypeHeaderValueType>(type, nameof(type), null)
            };
        }

        public static MediaTypeHeaderValue Create(this MediaTypeHeaderValueType type, String? charset)
        {
            MediaTypeHeaderValue header = Create(type);
            header.CharSet = charset;
            return header;
        }

        public static void AddQueryStringMapping(this MediaTypeFormatter formatter, String name, String value, String media)
        {
            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (media is null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            QueryStringMapping mapping = new QueryStringMapping(name, value, media);
            formatter.MediaTypeFormatterMapping.Add(mapping);
        }

        public static void AddQueryStringMapping(this MediaTypeFormatter formatter, String name, String value, MediaTypeHeaderValue media)
        {
            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (media is null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            QueryStringMapping mapping = new QueryStringMapping(name, value, media);
            formatter.MediaTypeFormatterMapping.Add(mapping);
        }

        public static void AddRequestHeaderMapping(this MediaTypeFormatter formatter, String name, String value, Boolean substring, String media, StringComparison comparison)
        {
            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (media is null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            RequestHeaderMapping mapping = new RequestHeaderMapping(name, value, substring, media, comparison);
            formatter.MediaTypeFormatterMapping.Add(mapping);
        }

        public static void AddRequestHeaderMapping(this MediaTypeFormatter formatter, String name, String value, Boolean substring, MediaTypeHeaderValue media, StringComparison comparison)
        {
            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (media is null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            RequestHeaderMapping mapping = new RequestHeaderMapping(name, value, substring, media, comparison);
            formatter.MediaTypeFormatterMapping.Add(mapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSubsetOf(this MediaTypeHeaderValue source, MediaTypeHeaderValue destination)
        {
            return IsSubsetOf(source, destination, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsSubsetOf(this MediaTypeHeaderValue source, MediaTypeHeaderValue? destination, out MediaTypeHeaderValueRange result)
        {
            if (destination is null)
            {
                result = MediaTypeHeaderValueRange.None;
                return false;
            }
            
            FormatterMediaTypeHeaderValue value = new FormatterMediaTypeHeaderValue(source);
            FormatterMediaTypeHeaderValue other = new FormatterMediaTypeHeaderValue(destination);
            result = other.IsAllMediaRange ? MediaTypeHeaderValueRange.AllMediaRange : other.IsSubtypeMediaRange ? MediaTypeHeaderValueRange.SubtypeMediaRange : MediaTypeHeaderValueRange.None;
            
            if (!value.TypesEqual(other))
            {
                if (result != MediaTypeHeaderValueRange.AllMediaRange)
                {
                    return false;
                }
            }
            else if (!value.SubTypesEqual(other) && result != MediaTypeHeaderValueRange.SubtypeMediaRange)
            {
                return false;
            }

            return source.Parameters.Select(item => Enumerable.Contains(destination.Parameters, item)).All(flag => flag);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<MediaTypeHeader> MediaType(this IReadOnlyMediaTypeFormatter source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.SupportedMediaType.ToImmutableSortedSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedSet<MediaTypeHeader> MediaType(this IEnumerable<IReadOnlyMediaTypeFormatter> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.SelectMany(static formatter => formatter.SupportedMediaType).ToImmutableSortedSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? Lower(this MediaTypeHeader source)
        {
            return source.ToString()?.ToLowerInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Lower(this MediaTypeHeaderValue source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToString().ToLowerInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? Upper(this MediaTypeHeader source)
        {
            return source.ToString()?.ToUpperInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Upper(this MediaTypeHeaderValue source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToString().ToUpperInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? LowerCharSet(this MediaTypeHeader source)
        {
            return source.CharSet?.ToLowerInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? LowerCharSet(this MediaTypeHeaderValue source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.CharSet?.ToLowerInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? UpperCharSet(this MediaTypeHeader source)
        {
            return source.CharSet?.ToUpperInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? UpperCharSet(this MediaTypeHeaderValue source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.CharSet?.ToUpperInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? LowerMediaType(this MediaTypeHeader source)
        {
            return source.MediaType?.ToLowerInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? LowerMediaType(this MediaTypeHeaderValue source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.MediaType?.ToLowerInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? UpperMediaType(this MediaTypeHeader source)
        {
            return source.MediaType?.ToUpperInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? UpperMediaType(this MediaTypeHeaderValue source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.MediaType?.ToUpperInvariant();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? Lower(this MediaTypeNotSupportedException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return exception.MediaType is { } media ? Lower(media) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? Upper(this MediaTypeNotSupportedException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
            
            return exception.MediaType is { } media ? Upper(media) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? LowerCharSet(this MediaTypeNotSupportedException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
            
            return exception.MediaType is { } media ? LowerCharSet(media) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? UpperCharSet(this MediaTypeNotSupportedException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
            
            return exception.MediaType is { } media ? UpperCharSet(media) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? LowerMediaType(this MediaTypeNotSupportedException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
            
            return exception.MediaType is { } media ? LowerMediaType(media) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? UpperMediaType(this MediaTypeNotSupportedException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
            
            return exception.MediaType is { } media ? UpperMediaType(media) : null;
        }
    }
}