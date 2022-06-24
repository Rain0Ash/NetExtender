// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Types.Input;

namespace NetExtender.Utilities.Windows.IO
{
    public static partial class KeyboardUtilities
    {
        public static ModifierKeys Modifiers
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Keyboard.Modifiers;
            }
        }

        public static Keys Keys
        {
            get
            {
                return Keyboard.PrimaryDevice.DownKeys();
            }
        }

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

        private static Keys GetKeys(Func<Key, Boolean> handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            ReadOnlyCollection<Key> keys = EnumUtilities.GetValuesWithoutDefault<Key>();
            Key[] rent = ArrayPool<Key>.Shared.Rent(keys.Count);

            Int32 counter = 0;
            foreach (Key key in keys)
            {
                if (handler(key))
                {
                    rent[counter++] = key;
                }
            }
            
            Keys result = new Keys(rent);
            ArrayPool<Key>.Shared.Return(rent);
            return result;
        }

        public static Keys DownKeys(this KeyboardDevice device)
        {
            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            return GetKeys(device.IsKeyDown);
        }
        
        public static Keys ToggleKeys(this KeyboardDevice device)
        {
            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            return GetKeys(device.IsKeyToggled);
        }
        
        public static Keys UpKeys(this KeyboardDevice device)
        {
            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            return GetKeys(device.IsKeyUp);
        }
    }
}