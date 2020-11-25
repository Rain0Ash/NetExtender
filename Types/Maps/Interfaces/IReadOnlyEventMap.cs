// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Interfaces;

namespace NetExtender.Types.Maps
{
    public interface IReadOnlyEventMap<TKey, TValue> : IReadOnlyMap<TKey, TValue>, IEventType
    {
        public event TypeKeyValueHandler<TKey, TValue> OnAdd;

        public event TypeKeyValueHandler<TKey, TValue> OnSet;

        public event TypeKeyValueHandler<TKey, TValue> OnRemove;

        public event TypeKeyValueHandler<TKey, TValue> OnChange;
    }
}