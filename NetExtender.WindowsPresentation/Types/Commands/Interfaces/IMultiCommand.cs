// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;

namespace System.Windows.Input
{
    public interface IMultiCommand<in T> : IMultiCommand, ICommand<T>, ICommand<IEnumerable<T?>>
    {
    }

    public interface IMultiCommand : ISenderCommand
    {
    }
}