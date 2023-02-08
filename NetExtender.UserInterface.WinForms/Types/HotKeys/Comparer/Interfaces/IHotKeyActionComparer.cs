// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Types.HotKeys.Interfaces;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.Types.HotKeys.Comparer.Interfaces
{
    public interface IHotKeyActionComparer<in T> : IHotKeyActionComparer<T, Keys, HotKeyModifierKeys> where T : struct, IHotKeyAction<T>
    {
    }
    
    public interface IHotKeyActionComparer<in T, TId> : IHotKeyActionComparer<T>, IHotKeyActionComparer<T, TId, Keys, HotKeyModifierKeys> where T : struct, IHotKeyAction<T, TId> where TId : unmanaged, IComparable<TId>, IConvertible
    {
    }
}