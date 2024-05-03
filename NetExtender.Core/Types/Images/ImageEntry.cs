using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Images
{
    public record ImageEntry<T> where T : class
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator String?(ImageEntry<T>? value)
        {
            return value?.Name;
        }

        public static implicit operator T?(ImageEntry<T>? value)
        {
            return value?.Image;
        }

        public String Name { get; }
        public T? Image { get; init; }

        public ImageEntry(String name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override String ToString()
        {
            return Name;
        }
    }
}