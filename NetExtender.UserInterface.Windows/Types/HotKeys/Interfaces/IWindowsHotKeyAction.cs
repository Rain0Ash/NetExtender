// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.Types.HotKeys.Interfaces
{
    public interface IWindowsHotKeyAction<T> : IHotKeyAction<T, Char, HotKeyModifierKeys> where T : struct
    {
    }
    
    public interface IWindowsHotKeyAction<T, out TId> : IWindowsHotKeyAction<T>, IHotKeyAction<T, TId, Char, HotKeyModifierKeys> where T : struct where TId : unmanaged, IComparable<TId>, IConvertible
    {
    }
}