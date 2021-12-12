// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads
{
    public sealed class ResettableLazy<T> : ILazy<T>
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

        public ResettableLazy<T> Reset()
        {
            lock (Internal)
            {
                Internal = new Lazy<T>(Mode);
            }
            
            return this;
        }

        public ResettableLazy<T> Reset(Func<T> valueFactory)
        {
            if (valueFactory is null)
            {
                throw new ArgumentNullException(nameof(valueFactory));
            }

            lock (Internal)
            {
                Internal = new Lazy<T>(valueFactory, Mode);
            }
            
            return this;
        }
    }
}