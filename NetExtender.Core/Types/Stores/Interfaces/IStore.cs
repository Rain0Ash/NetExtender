// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Stores.Interfaces
{
    public interface IStore<TKey, TValue> : IWeakDictionary<TKey, TValue> where TKey : class where TValue : class?
    {
    }
}