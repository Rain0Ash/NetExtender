// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads
{
    public class ResettableLazy<T> : IResettableLazy<T>
    {
        [return: NotNullIfNotNull("lazy")]
        public static implicit operator Lazy<T>?(ResettableLazy<T>? lazy)
        {
            return lazy?.Internal;
        }

        private Lazy<T> Internal { get; set; }
        private LazyThreadSafetyMode Mode { get; }

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

        public ResettableLazy(Func<T> valueFactory)
        {
            Internal = new Lazy<T>(valueFactory ?? throw new ArgumentNullException(nameof(valueFactory)));
        }

        public ResettableLazy(Boolean isThreadSafe)
        {
            Mode = LazyUtilities.GetModeFromIsThreadSafe(isThreadSafe);
            Internal = new Lazy<T>(Mode);
        }

        public ResettableLazy(LazyThreadSafetyMode mode)
        {
            Mode = mode;
            Internal = new Lazy<T>(Mode);
        }

        public ResettableLazy(Func<T> valueFactory, Boolean isThreadSafe)
        {
            if (valueFactory is null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }

            Mode = LazyUtilities.GetModeFromIsThreadSafe(isThreadSafe);
            Internal = new Lazy<T>(valueFactory, Mode);
        }

        public ResettableLazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
        {
            if (valueFactory is null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }

            Mode = mode;
            Internal = new Lazy<T>(valueFactory, Mode);
        }

        public virtual ResettableLazy<T> Reset()
        {
            Internal = new Lazy<T>(Mode);
            return this;
        }
        
        IResettableLazy<T> IResettableLazy<T>.Reset()
        {
            return Reset();
        }

        public virtual ResettableLazy<T> Reset(T value)
        {
            Internal = new Lazy<T>(() => value, Mode);
            return this;
        }
        
        IResettableLazy<T> IResettableLazy<T>.Reset(T value)
        {
            return Reset(value);
        }

        public virtual ResettableLazy<T> Reset(Func<T> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            
            Internal = new Lazy<T>(factory, Mode);
            return this;
        }
        
        IResettableLazy<T> IResettableLazy<T>.Reset(Func<T> factory)
        {
            return Reset(factory);
        }
    }
}