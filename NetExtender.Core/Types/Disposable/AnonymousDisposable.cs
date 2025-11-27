// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using NetExtender.Types.Disposable.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Disposable
{
    public sealed class AnonymousWeakDisposable<T> : IAnonymousDisposable<T> where T : class
    {
        public static implicit operator Boolean(AnonymousWeakDisposable<T>? value)
        {
            return value?.Alive ?? false;
        }
        
        private WeakMaybe<T> _object;
        private volatile Action<T>? _dispose;

        public Boolean Alive
        {
            get
            {
                return _dispose is not null;
            }
        }

        private AnonymousWeakDisposable()
        {
        }

        public AnonymousWeakDisposable(T @object, Action<T> dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
            _object = @object;
        }

        public AnonymousWeakDisposable(WeakReference<T> @object, Action<T> dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
            _object = @object;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // ReSharper disable once UnusedParameter.Local
        private void Dispose(Boolean disposing)
        {
            if (Interlocked.Exchange(ref _dispose, null) is not { } dispose || _object is not { IsAlive: true, Internal: { } @object })
            {
                _object = default;
                return;
            }

            dispose.Invoke(@object);
            _object = default;
        }

        ~AnonymousWeakDisposable()
        {
            Dispose(false);
        }
    }
    
    public sealed class AnonymousDisposable<T> : IAnonymousDisposable<T>
    {
        public static implicit operator Boolean(AnonymousDisposable<T>? value)
        {
            return value?.Alive ?? false;
        }
        
        private Maybe<T> _object;
        private volatile Action<T>? _dispose;

        public Boolean Alive
        {
            get
            {
                return _dispose is not null;
            }
        }

        private AnonymousDisposable()
        {
        }

        public AnonymousDisposable(T @object, Action<T> dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
            _object = @object;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // ReSharper disable once UnusedParameter.Local
        private void Dispose(Boolean disposing)
        {
            if (Interlocked.Exchange(ref _dispose, null) is not { } dispose)
            {
                _object = default;
                return;
            }

            if (_object is not { HasValue: true, Internal: var @object })
            {
                return;
            }

            dispose.Invoke(@object);
            _object = default;
        }

        ~AnonymousDisposable()
        {
            Dispose(false);
        }
    }
    
    public sealed class AnonymousDisposable : IAnonymousDisposable
    {
        public static implicit operator Boolean(AnonymousDisposable? value)
        {
            return value?.Alive ?? false;
        }
        
        public static IDisposable Null { get; } = new AnonymousDisposable();
        private volatile Action? _dispose;

        public Boolean Alive
        {
            get
            {
                return _dispose is not null;
            }
        }

        private AnonymousDisposable()
        {
        }

        public AnonymousDisposable(Action dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // ReSharper disable once UnusedParameter.Local
        private void Dispose(Boolean disposing)
        {
            if (Interlocked.Exchange(ref _dispose, null) is { } dispose)
            {
                dispose.Invoke();
            }
        }

        ~AnonymousDisposable()
        {
            Dispose(false);
        }
    }
}