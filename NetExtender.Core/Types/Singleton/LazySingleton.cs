// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types
{
    public class LazySingleton<T> where T : LazySingleton<T>, new()
    {
        private static readonly Lazy<LazySingleton<T>> Lazy =
            new Lazy<LazySingleton<T>>(() => new LazySingleton<T>());

        public static LazySingleton<T> Instance
        {
            get
            {
                return Lazy.Value;
            }
        }
    }
}