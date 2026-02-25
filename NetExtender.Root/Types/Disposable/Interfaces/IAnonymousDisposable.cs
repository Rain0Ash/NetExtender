using System;

namespace NetExtender.Types.Disposable.Interfaces
{
    public interface IAnonymousDisposable<T> : IAnonymousDisposable
    {
    }

    public interface IAnonymousDisposable : IDisposable
    {
        public Boolean Alive { get; }
    }
}