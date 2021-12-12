// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Types.Monads
{
    public class EventLazy<T> : ILazy<T>
    {
        [return: NotNullIfNotNull("wrapper")]
        public static implicit operator Lazy<T>?(EventLazy<T>? wrapper)
        {
            return wrapper?.Internal;
        }
        
        private Lazy<T> Internal { get; }

        private event Action<T> Init = null!;
        public event Action<T> Initialized
        {
            add
            {
                if (!IsValueCreated)
                {
                    Init += value;
                }
            }
            remove
            {
                Init -= value;
            }
        }

        public T Value
        {
            get
            {
                return Internal.Value;
            }
        }

        public Boolean IsValueCreated
        {
            get
            {
                return Internal.IsValueCreated;
            }
        }

        public EventLazy(Func<T> valueFactory)
        {
            if (valueFactory is null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }

            Internal = new Lazy<T>(Wrapper(valueFactory));
        }

        public EventLazy(Boolean isThreadSafe)
        {
            Internal = new Lazy<T>(isThreadSafe);
        }

        public EventLazy(LazyThreadSafetyMode mode)
        {
            Internal = new Lazy<T>(mode);
        }

        public EventLazy(Func<T> valueFactory, Boolean isThreadSafe)
        {
            if (valueFactory is null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }

            Internal = new Lazy<T>(Wrapper(valueFactory), isThreadSafe);
        }

        public EventLazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
        {
            if (valueFactory is null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }

            Internal = new Lazy<T>(Wrapper(valueFactory), mode);
        }

        private Func<T> Wrapper(Func<T> valueFactory)
        {
            if (valueFactory is null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }

            T Handler()
            {
                T value = valueFactory.Invoke();
                Init?.Invoke(value);
                Init = null!;
                return value;
            }

            return Handler;
        }
    }
}