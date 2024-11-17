// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Storages.Interfaces
{
    public interface IReadOnlyStorage<T> : IReadOnlyWeakSet<T> where T : class
    {
    }
    
    public interface IReadOnlyStorage<TKey, TValue> : IReadOnlyWeakDictionary<TKey, TValue> where TKey : class
    {
    }
}