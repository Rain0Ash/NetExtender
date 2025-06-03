// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;

namespace System.Windows.Input
{
    public interface IRevertMultiCommand<in T> : IRevertMultiCommand, IMultiCommand<T>, IRevertCommand<T>, IRevertCommand<IEnumerable<T?>>
    {
        public new IMultiCommand<T> Reverter { get; }
    }
    
    public interface IRevertMultiCommand : IMultiCommand, IRevertCommand
    {
    }
}