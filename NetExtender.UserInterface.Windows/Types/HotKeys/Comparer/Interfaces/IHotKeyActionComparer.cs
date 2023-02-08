// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.HotKeys.Interfaces;

namespace NetExtender.Types.HotKeys.Comparer.Interfaces
{
    public interface IHotKeyActionComparer<in T, TKey, TModifier> : IEqualityComparer<T>, IComparer<T> where T : struct, IHotKeyAction<T, TKey, TModifier> where TKey : unmanaged where TModifier : unmanaged
    {
        public Boolean Title { get; }
        public Boolean Key { get; }
        public Boolean Modifier { get; }
        public IEqualityComparer<String?>? Comparer { get; }
    }
    
    public interface IHotKeyActionComparer<in T, TId, TKey, TModifier> : IHotKeyActionComparer<T, TKey, TModifier> where T : struct, IHotKeyAction<T, TId, TKey, TModifier> where TId : unmanaged, IComparable<TId>, IConvertible where TKey : unmanaged where TModifier : unmanaged
    {
        public Boolean Id { get; }
    }
}