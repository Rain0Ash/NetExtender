// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NetExtender.Utilities.Windows.IO
{
    public static partial class KeyboardUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsKeyActive(this Key value, Func<Key, Boolean> handler)
        {
            return IsKeyActive(handler, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsKeyActive(Func<Key, Boolean> handler, Key value)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return handler(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsKeyActive(Func<Key, Boolean> handler, Key first, Key second)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return IsKeyActive(handler, first) || IsKeyActive(handler, second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsKeyActive(Func<Key, Boolean> handler, Key first, Key second, Key third)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return IsKeyActive(handler, first) || IsKeyActive(handler, second) || IsKeyActive(handler, third);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsKeyActive(Func<Key, Boolean> handler, params Key[] keys)
        {
            return IsKeyActive(handler, (IEnumerable<Key>) keys);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsKeyActive(this IEnumerable<Key> keys, Func<Key, Boolean> handler)
        {
            return IsKeyActive(handler, keys);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsKeyActive(Func<Key, Boolean> handler, IEnumerable<Key> keys)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (keys is null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            return keys.Any(handler);
        }
    }
}