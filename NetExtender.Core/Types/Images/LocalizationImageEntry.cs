// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Culture;

namespace NetExtender.Types.Images
{
    public record LocalizationImageEntry<T> : ImageEntry<T> where T : class
    {
        public static implicit operator LocalizationImageEntry<T>(LocalizationIdentifier value)
        {
            return new LocalizationImageEntry<T>(value);
        }

        public static implicit operator LocalizationIdentifier(LocalizationImageEntry<T>? value)
        {
            return value?.Identifier ?? LocalizationIdentifier.Invariant;
        }

        public LocalizationIdentifier Identifier { get; }

        public LocalizationImageEntry(LocalizationIdentifier identifier)
            : base(identifier.ToString())
        {
            Identifier = identifier;
        }

        public static LocalizationImageEntry<T> Convert(LocalizationIdentifier identifier)
        {
            return identifier;
        }

        public override String ToString()
        {
            return Name;
        }
    }
}