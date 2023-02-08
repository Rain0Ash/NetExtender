// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;
using NetExtender.Types.HotKeys.Comparer.Interfaces;
using NetExtender.Types.HotKeys.Interfaces;

namespace NetExtender.Types.HotKeys.Comparer
{
    public class HotKeyActionComparer<T> : HotKeyActionComparer<T, Key, ModifierKeys>, IHotKeyActionComparer<T> where T : struct, IHotKeyAction<T>
    {
    }
    
    public class HotKeyActionComparer<T, TId> : HotKeyActionComparer<T, TId, Key, ModifierKeys>, IHotKeyActionComparer<T, TId> where T : struct, IHotKeyAction<T, TId> where TId : unmanaged, IComparable<TId>, IConvertible
    {
    }
}