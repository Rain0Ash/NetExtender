// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Input;

namespace NetExtender.Utils.IO
{
    public static partial class KeyboardUtils
    {
        private static Boolean IsKeyActive(Func<Key, Boolean> handler, Key first, Key second)
        {
            return handler(first) || handler(second);
        }
    }
}