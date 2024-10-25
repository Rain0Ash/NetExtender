// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Threading;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Types;

namespace NetExtender.Utilities.Windows.IO
{
    public enum KeyState
    {
        Up,
        Down,
        Toggle
    }

    public static partial class KeyboardUtilities
    {
        public static ModifierKeys Modifiers
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ThreadUtilities.STA(() => Keyboard.Modifiers);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsKeyActive(Key value, Func<Key, Boolean> handler)
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

            return ThreadUtilities.STA(handler, value);
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

        private static Keys Keys(Func<Key, Boolean> handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            ImmutableArray<Key> values = EnumUtilities.GetValuesWithoutDefault<Key>();
            Span<Key> keys = stackalloc Key[values.Length];

            Int32 counter = 0;
            foreach (Key key in values)
            {
                if (handler(key))
                {
                    keys[counter++] = key;
                }
            }

            Keys result = new Keys(keys.Slice(0, counter));
            return result;
        }

        public static Keys GetKeys(this KeyboardDevice device, KeyState state)
        {
            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            Func<Key, Boolean> handler = state switch
            {
                KeyState.Up => device.IsKeyUp,
                KeyState.Down => device.IsKeyDown,
                KeyState.Toggle => device.IsKeyToggled,
                _ => throw new EnumUndefinedOrNotSupportedException<KeyState>(state, nameof(state), null)
            };

            return ThreadUtilities.STA(Keys, handler);
        }
    }
}