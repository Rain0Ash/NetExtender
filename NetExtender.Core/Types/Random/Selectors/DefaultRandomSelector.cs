// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Dictionaries;

namespace NetExtender.Types.Random
{
    internal sealed class DefaultRandomSelector<T> : RandomSelector<T>
    {
        public static DefaultRandomSelector<T> Instance { get; } = new DefaultRandomSelector<T>();

        public override IReadOnlyCollection<T> Collection
        {
            get
            {
                return Array.Empty<T>();
            }
        }

        public override Int32 Count
        {
            get
            {
                return 0;
            }
        }
        
        private DefaultRandomSelector()
        {
        }

        public override T GetRandom()
        {
            throw new InvalidOperationException("Items is empty");
        }

        public override T GetRandom(Double value)
        {
            throw new InvalidOperationException("Items is empty");
        }

        [return: NotNullIfNotNull("alternate")]
        public override T? GetRandomOrDefault(T? alternate)
        {
            return alternate;
        }

        [return: NotNullIfNotNull("alternate")]
        public override T? GetRandomOrDefault(Double value, T? alternate)
        {
            return alternate;
        }

        public override T[] ToItemArray()
        {
            return Array.Empty<T>();
        }

        public override List<T> ToItemList()
        {
            return new List<T>(0);
        }

        public override NullableDictionary<T, Double> ToItemDictionary()
        {
            return new NullableDictionary<T, Double>(0);
        }
    }
}