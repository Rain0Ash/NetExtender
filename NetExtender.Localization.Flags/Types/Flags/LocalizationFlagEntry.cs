// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Localization.Utilities;
using NetExtender.Types.Culture;
using NetExtender.Types.Images;

namespace NetExtender.Localization.Types.Flags
{
    public record LocalizationFlagEntry<T> : LocalizationImageEntry<T> where T : class
    {
        public static implicit operator LocalizationFlagEntry<T>(LocalizationIdentifier value)
        {
            return new LocalizationFlagEntry<T>(value);
        }

        public static implicit operator LocalizationIdentifier(LocalizationFlagEntry<T>? value)
        {
            return value?.Identifier ?? LocalizationIdentifier.Invariant;
        }

        public LocalizationFlagEntry(LocalizationIdentifier identifier)
            : base(identifier)
        {
            Image = identifier.GetFlagImage<T>();
        }

        public new static LocalizationFlagEntry<T> Convert(LocalizationIdentifier identifier)
        {
            return identifier;
        }

        public override String ToString()
        {
            return Name;
        }
    }
}