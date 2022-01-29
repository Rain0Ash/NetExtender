// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Disposable.Interfaces;

namespace NetExtender.Types.Disposable
{
    public abstract class DisposableToken : IDisposableToken
    {
        public static IDisposableToken Empty { get; } = new NullDisposableToken();

        private sealed class NullDisposableToken : IDisposableToken
        {
            public Boolean Active
            {
                get
                {
                    return true;
                }
            }

            public void Dispose()
            {
            }
        }
        
        public Boolean Active { get; private set; }

        public DisposableToken()
        {
            Active = true;
        }
        
        public void Dispose()
        {
            if (!Active)
            {
                return;
            }
            
            Active = false;
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract Boolean Dispose(Boolean dispose);

        ~DisposableToken()
        {
            if (!Active)
            {
                return;
            }
            
            Dispose(false);
        }
    }
}