// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Maps.Interfaces
{
    public interface IEventMap<TKey, TValue> : IMap<TKey, TValue>
    {
        public event TypeKeyValueHandler<TKey, TValue> OnAdd;

        public event TypeKeyValueHandler<TKey, TValue> OnSet;

        public event TypeKeyValueHandler<TKey, TValue> OnRemove;

        public event TypeKeyValueHandler<TKey, TValue> OnChange;
    }
}