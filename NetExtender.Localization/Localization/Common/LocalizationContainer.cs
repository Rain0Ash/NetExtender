// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Common
{
    [Serializable]
    public sealed class LocalizationContainer : Dictionary<LocalizationIdentifier, String>
    {
        public LocalizationContainer()
            : this(4)
        {
        }

        public LocalizationContainer(IDictionary<LocalizationIdentifier, String> dictionary)
            : base(dictionary)
        {
        }

        public LocalizationContainer(IDictionary<LocalizationIdentifier, String> dictionary, IEqualityComparer<LocalizationIdentifier>? comparer)
            : base(dictionary, comparer)
        {
        }

        public LocalizationContainer(IEnumerable<KeyValuePair<LocalizationIdentifier, String>> collection)
            : base(collection)
        {
        }

        public LocalizationContainer(IEnumerable<KeyValuePair<LocalizationIdentifier, String>> collection, IEqualityComparer<LocalizationIdentifier>? comparer)
            : base(collection, comparer)
        {
        }

        public LocalizationContainer(IEqualityComparer<LocalizationIdentifier>? comparer)
            : base(comparer)
        {
        }

        public LocalizationContainer(Int32 capacity)
            : base(capacity)
        {
        }

        public LocalizationContainer(Int32 capacity, IEqualityComparer<LocalizationIdentifier>? comparer)
            : base(capacity, comparer)
        {
        }

        private LocalizationContainer(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}