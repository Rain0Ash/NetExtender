// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Http.Headers;

namespace NetExtender.Types.Network.Formatters
{
    public readonly struct FormatterMediaTypeHeaderValue
    {
        private String? MediaType { get; }
        private Int32? Delimiter { get; }

        public Boolean IsAllMediaRange { get; }
        public Boolean IsSubtypeMediaRange { get; }

        public FormatterMediaTypeHeaderValue(MediaTypeHeaderValue media)
        {
            if (media is null)
            {
                throw new ArgumentNullException(nameof(media));
            }

            MediaType = media.MediaType;
            Delimiter = MediaType?.IndexOf('/');
            IsAllMediaRange = false;
            IsSubtypeMediaRange = false;

            if (String.IsNullOrEmpty(MediaType) || Delimiter != MediaType.Length - 2 || MediaType[^1] != '*')
            {
                return;
            }

            IsSubtypeMediaRange = true;
            if (Delimiter != 1 || MediaType[0] != '*')
            {
                return;
            }

            IsAllMediaRange = true;
        }

        public Boolean TypesEqual(in FormatterMediaTypeHeaderValue other)
        {
            if (Delimiter is null && other.Delimiter is null)
            {
                return true;
            }

            return Delimiter == other.Delimiter && String.Compare(MediaType, 0, other.MediaType, 0, Delimiter ?? 0, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public Boolean SubTypesEqual(in FormatterMediaTypeHeaderValue other)
        {
            if (MediaType is null || other.MediaType is null)
            {
                return true;
            }

            Int32 length = MediaType.Length - (Delimiter ?? 0) - 1;
            return length == other.MediaType.Length - (other.Delimiter ?? 0) - 1 && String.Compare(MediaType, Delimiter ?? 0 + 1, other.MediaType, other.Delimiter ?? 0 + 1, length, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}