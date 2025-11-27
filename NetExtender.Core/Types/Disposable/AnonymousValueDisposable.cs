// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using NetExtender.Types.Disposable.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Disposable
{
    public struct AnonymousValueWeakDisposable<T> : IAnonymousDisposable<T> where T : class
    {
        public static implicit operator Boolean(AnonymousValueWeakDisposable<T> value)
        {
            return value.Alive;
        }
        
        private WeakMaybe<T> _object;
        private volatile Action<T>? _dispose;

        public readonly Boolean Alive
        {
            get
            {
                return _dispose is not null;
            }
        }

        public AnonymousValueWeakDisposable(T @object, Action<T> dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
            _object = @object;
        }

        public AnonymousValueWeakDisposable(WeakReference<T> @object, Action<T> dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
            _object = @object;
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref _dispose, null) is not { } dispose || _object is not { IsAlive: true, Internal: { } @object })
            {
                _object = default;
                return;
            }

            dispose.Invoke(@object);
            _object = default;
        }
    }
    
    public struct AnonymousValueDisposable<T> : IAnonymousDisposable<T>
    {
        public static implicit operator Boolean(AnonymousValueDisposable<T> value)
        {
            return value.Alive;
        }
        
        private Maybe<T> _object;
        private volatile Action<T>? _dispose;

        public readonly Boolean Alive
        {
            get
            {
                return _dispose is not null;
            }
        }

        public AnonymousValueDisposable(T @object, Action<T> dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
            _object = @object;
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref _dispose, null) is not { } dispose)
            {
                return;
            }

            if (_object is not { HasValue: true, Internal: var @object })
            {
                return;
            }

            dispose.Invoke(@object);
            _object = default;
        }
    }
    
    public struct AnonymousValueDisposable : IAnonymousDisposable
    {
        public static implicit operator Boolean(AnonymousValueDisposable value)
        {
            return value.Alive;
        }
        
        public static AnonymousValueDisposable Null
        {
            get
            {
                return new AnonymousValueDisposable();
            }
        }

        private volatile Action? _dispose;

        public readonly Boolean Alive
        {
            get
            {
                return _dispose is not null;
            }
        }

        public AnonymousValueDisposable(Action dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
        }

        public void Dispose()
        {
            if (Interlocked.Exchange(ref _dispose, null) is { } dispose)
            {
                dispose.Invoke();
            }
        }
    }
}