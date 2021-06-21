// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Reflection;

namespace NetExtender.Types
{
    /// <summary>
    /// Base class used for singletons
    /// </summary>
    /// <typeparam name="T">The class type</typeparam>
    public class Singleton<T> where T : class
    {
        private static T instance;

        /// <summary>
        /// Gets the instance of the singleton
        /// </summary>
        public static T Instance
        {
            get
            {
                OnInit();

                return instance;
            }
        }

        protected Singleton()
        {
        }

        private static void OnInit()
        {
            if (instance is not null)
            {
                return;
            }

            lock (typeof(T))
            {
                instance = typeof(T).InvokeMember(typeof(T).Name,
                    BindingFlags.CreateInstance |
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic,
                    null, null, null) as T;
            }
        }
    }
}