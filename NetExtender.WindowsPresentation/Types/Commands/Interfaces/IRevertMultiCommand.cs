using System.Collections;
using System.Collections.Generic;

namespace System.Windows.Input
{
    public interface IRevertMultiCommand<in T> : IRevertMultiCommand, IMultiCommand<T>, IRevertCommand<T>, IRevertCommand<IEnumerable<T?>>
    {
        public new IMultiCommand<T> Revertor { get; }
    }
    
    public interface IRevertMultiCommand : IMultiCommand, IRevertCommand<IEnumerable>
    {
    }
}