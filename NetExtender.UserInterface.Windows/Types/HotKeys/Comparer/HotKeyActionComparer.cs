// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.HotKeys.Comparer.Interfaces;
using NetExtender.Types.HotKeys.Interfaces;

namespace NetExtender.Types.HotKeys.Comparer
{
    public class HotKeyActionComparer<T, TKey, TModifier> : IHotKeyActionComparer<T, TKey, TModifier> where T : struct, IHotKeyAction<T, TKey, TModifier> where TKey : unmanaged where TModifier : unmanaged
    {
        public Boolean Title { get; init; } = true;
        public Boolean Key { get; init; } = true;
        public Boolean Modifier { get; init; } = true;
        public IEqualityComparer<String?>? Comparer { get; init; }

        public Int32 GetHashCode(T hotkey)
        {
            HashCode code = new HashCode();
            return GetHashCode(ref code, hotkey).ToHashCode();
        }

        protected virtual ref HashCode GetHashCode(ref HashCode code, T hotkey)
        {
            if (Title)
            {
                code.Add(hotkey.Title, Comparer);
            }

            if (Key)
            {
                code.Add(hotkey.Key);
            }

            if (Modifier)
            {
                code.Add(Modifier);
            }

            return ref code;
        }

        public virtual Boolean Equals(T x, T y)
        {
            if (Title && (!Comparer?.Equals(x.Title, y.Title) ?? x.Title != y.Title))
            {
                return false;
            }

            if (Key && !EqualityComparer<TKey>.Default.Equals(x.Key, y.Key))
            {
                return false;
            }

            if (Modifier && !EqualityComparer<TModifier>.Default.Equals(x.Modifier, y.Modifier))
            {
                return false;
            }

            return true;
        }

        public virtual Int32 Compare(T x, T y)
        {
            Int32 comparison = 0;
            
            if (Key)
            {
                comparison = Comparer<TKey>.Default.Compare(x.Key, y.Key);
            }

            if (comparison == 0 && Modifier)
            {
                comparison = Comparer<TModifier>.Default.Compare(x.Modifier, y.Modifier);
            }

            return comparison;
        }
    }
    
    public class HotKeyActionComparer<T, TId, TKey, TModifier> : HotKeyActionComparer<T, TKey, TModifier>, IHotKeyActionComparer<T, TId, TKey, TModifier> where T : struct, IHotKeyAction<T, TId, TKey, TModifier> where TId : unmanaged, IComparable<TId>, IConvertible where TKey : unmanaged where TModifier : unmanaged
    {
        public Boolean Id { get; init; } = true;

        protected override ref HashCode GetHashCode(ref HashCode code, T hotkey)
        {
            if (Id)
            {
                code.Add(hotkey.Id);
            }

            return ref base.GetHashCode(ref code, hotkey);
        }

        public override Boolean Equals(T x, T y)
        {
            if (Id && !EqualityComparer<TId>.Default.Equals(x.Id, y.Id))
            {
                return false;
            }

            return base.Equals(x, y);
        }

        public override Int32 Compare(T x, T y)
        {
            if (!Id)
            {
                return base.Compare(x, y);
            }

            Int32 compare = Comparer<TId>.Default.Compare(x.Id, y.Id);
            return compare != 0 ? compare : base.Compare(x, y);
        }
    }
}