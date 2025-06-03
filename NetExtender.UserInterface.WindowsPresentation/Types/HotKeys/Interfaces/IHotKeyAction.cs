// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;

namespace NetExtender.Types.HotKeys.Interfaces
{
    public interface IHotKeyAction<T> : IHotKeyAction<T, Key, ModifierKeys> where T : struct, IStruct<T>
    {
    }
    
    public interface IHotKeyAction<T, out TId> : IHotKeyAction<T>, IHotKeyAction<T, TId, Key, ModifierKeys> where T : struct, IStruct<T> where TId : unmanaged, IComparable<TId>, IConvertible
    {
    }
}