// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Storages.Interfaces
{
    public interface IReadOnlyStorage<TKey, TValue> : IReadOnlyWeakDictionary<TKey, TValue> where TKey : class
    {
    }
}