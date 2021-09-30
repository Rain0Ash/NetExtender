// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.ObjectModel;

namespace NetExtender.Utilities.Types
{
    public static class ReadOnlyCollectionUtilities
    {
        private static class EmptyReadOnlyCollection<T>
        {
            public static ReadOnlyCollection<T> Empty { get; } = Array.AsReadOnly(Array.Empty<T>());
        }
        
        public static ReadOnlyCollection<T> Empty<T>()
        {
            return EmptyReadOnlyCollection<T>.Empty;
        }
    }
}