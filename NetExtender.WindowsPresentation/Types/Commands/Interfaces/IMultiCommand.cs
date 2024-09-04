using System.Collections.Generic;

namespace System.Windows.Input
{
    public interface IMultiCommand<in T> : IMultiCommand, ICommand<T>, ICommand<IEnumerable<T?>>
    {
    }

    public interface IMultiCommand : ICommand
    {
    }
}