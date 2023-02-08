// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.HotKeys.Interfaces
{
    public interface IHotKeyAction<T, out TKey, out TModifier> : IEquatable<T>, IComparable<T>, IFormattable where T : struct where TKey : unmanaged where TModifier : unmanaged
    {
        public String? Title { get; }
        public TKey Key { get; }
        public TModifier Modifier { get; }
        public Char VirtualKey { get; }
        public Boolean IsEmpty { get; }
    }
    
    public interface IHotKeyAction<T, out TId, out TKey, out TModifier> : IHotKeyAction<T, TKey, TModifier> where T : struct where TId : unmanaged, IComparable<TId>, IConvertible where TKey : unmanaged where TModifier : unmanaged
    {
        public TId Id { get; }
    }
}