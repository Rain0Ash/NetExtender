// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.HotKeys.Interfaces;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.Types.HotKeys.Comparer.Interfaces
{
    public interface IWindowsHotKeyActionComparer<in T> : IHotKeyActionComparer<T, Char, HotKeyModifierKeys> where T : struct, IWindowsHotKeyAction<T>
    {
    }
    
    public interface IWindowsHotKeyActionComparer<in T, TId> : IWindowsHotKeyActionComparer<T>, IHotKeyActionComparer<T, TId, Char, HotKeyModifierKeys> where T : struct, IWindowsHotKeyAction<T, TId> where TId : unmanaged, IComparable<TId>, IConvertible
    {
    }
}