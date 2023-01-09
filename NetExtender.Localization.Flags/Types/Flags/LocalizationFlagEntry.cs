using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Localization.Utilities;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Types.Flags
{
    public record LocalizationFlagEntry<T> where T : class
    {
        public static implicit operator LocalizationFlagEntry<T>(LocalizationIdentifier value)
        {
            return new LocalizationFlagEntry<T>(value);
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator String?(LocalizationFlagEntry<T>? value)
        {
            return value?.Name;
        }

        public static implicit operator LocalizationIdentifier(LocalizationFlagEntry<T>? value)
        {
            return value?.Identifier ?? LocalizationIdentifier.Invariant;
        }

        public static implicit operator T?(LocalizationFlagEntry<T>? value)
        {
            return value?.Image;
        }

        public String Name { get; }
        public LocalizationIdentifier Identifier { get; }
        public T? Image { get; }

        public LocalizationFlagEntry(LocalizationIdentifier identifier)
        {
            Identifier = identifier;
            Name = identifier.ToString();
            Image = identifier.GetFlagImage<T>();
        }

        public override String ToString()
        {
            return Name;
        }

        public static LocalizationFlagEntry<T> Convert(LocalizationIdentifier identifier)
        {
            return identifier;
        }
    }
}