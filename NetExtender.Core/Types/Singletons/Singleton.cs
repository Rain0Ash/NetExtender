// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Singletons.Interfaces;

namespace NetExtender.Types.Singletons
{
    public class Singleton<T> : ISingleton<T> where T : new()
    {
        private Lazy<T> Internal { get; }

        public T Instance
        {
            get
            {
                return Internal.Value;
            }
        }

        public Singleton()
        {
            Internal = new Lazy<T>(static () => new T(), true);
        }

        public Singleton(Func<T> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            Internal = new Lazy<T>(factory, true);
        }
    }
}