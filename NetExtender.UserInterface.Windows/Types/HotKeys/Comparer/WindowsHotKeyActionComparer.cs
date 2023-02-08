// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.HotKeys.Comparer.Interfaces;
using NetExtender.Types.HotKeys.Interfaces;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.Types.HotKeys.Comparer
{
    public class WindowsHotKeyActionComparer<T> : HotKeyActionComparer<T, Char, HotKeyModifierKeys>, IWindowsHotKeyActionComparer<T> where T : struct, IWindowsHotKeyAction<T>
    {
    }
    
    public class WindowsHotKeyActionComparer<T, TId> : HotKeyActionComparer<T, TId, Char, HotKeyModifierKeys>, IWindowsHotKeyActionComparer<T, TId> where T : struct, IWindowsHotKeyAction<T, TId> where TId : unmanaged, IComparable<TId>, IConvertible
    {
    }
}