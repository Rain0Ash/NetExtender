// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using JetBrains.Annotations;

namespace NetExtender.Utils.Types
{
    public static class LazyUtils
    {
        public static T Initialize<T>([NotNull] this Lazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Value;
        }

        public static T ValueOr<T>([NotNull] this Lazy<T> source, T @default)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.IsValueCreated ? source.Value : @default;
        }
        
        public static T ValueOr<T>([NotNull] this Lazy<T> source, [NotNull] Func<T> @default)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (@default is null)
            {
                throw new ArgumentNullException(nameof(@default));
            }

            return source.IsValueCreated ? source.Value : @default.Invoke();
        }
        
        public static T ValueOrDefault<T>([NotNull] this Lazy<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.IsValueCreated ? source.Value : default;
        }
    }
}