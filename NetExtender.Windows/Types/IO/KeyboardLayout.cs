// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Linq;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Types;
using NetExtender.Windows.Utilities.IO;

namespace NetExtender.Windows.Types.IO
{
    [Serializable]
    public readonly struct KeyboardLayout : IEquatable<KeyboardLayout>
    {
        internal static KeyboardLayout Create(IntPtr handle)
        {
            return new KeyboardLayout(handle);
        }

        public static explicit operator IntPtr(KeyboardLayout layout)
        {
            return layout.Handle;
        }

        public static implicit operator LocalizationIdentifier(KeyboardLayout layout)
        {
            return layout.Culture?.LCID ?? default;
        }

        public static implicit operator CultureInfo?(KeyboardLayout layout)
        {
            return layout.Culture;
        }

        public static explicit operator KeyboardLayout(IntPtr handle)
        {
            return new KeyboardLayout(handle);
        }

        public static explicit operator KeyboardLayout(CultureIdentifier lcid)
        {
            return new KeyboardLayout(lcid);
        }

        public static explicit operator KeyboardLayout(LocalizationIdentifier lcid)
        {
            return new KeyboardLayout(lcid);
        }

        public static explicit operator KeyboardLayout(CultureInfo culture)
        {
            return new KeyboardLayout(culture);
        }

        public static Boolean operator ==(KeyboardLayout first, KeyboardLayout second)
        {
            return first.Handle == second.Handle;
        }

        public static Boolean operator !=(KeyboardLayout first, KeyboardLayout second)
        {
            return !(first == second);
        }

        private IntPtr Handle { get; }

        public Boolean HasValue
        {
            get
            {
                return Handle != IntPtr.Zero;
            }
        }

        public CultureInfo? Culture
        {
            get
            {
                return HasValue && CultureUtilities.TryGetCultureInfo((Int32) Handle.ToInt16(out _), out CultureInfo culture) ? culture : default;
            }
        }

        private KeyboardLayout(IntPtr handle)
        {
            Handle = handle;
        }

        public KeyboardLayout(LocalizationIdentifier lcid)
            : this(lcid.GetCultureInfo())
        {
        }

        public KeyboardLayout(CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Int32 lcid = info.KeyboardLayoutId;
            Handle = KeyboardLayoutUtilities.KeyboardLayouts.FirstOrDefault(item => item.Handle.ToInt16(out _) == lcid).Handle;

            if (Handle == IntPtr.Zero)
            {
                throw new ArgumentException($"The specified keyboard layout '{info}' is not installed on the system.", nameof(info));
            }
        }

        public override Int32 GetHashCode()
        {
            return Handle.GetHashCode();
        }

        public override Boolean Equals(Object? value)
        {
            return value is KeyboardLayout other && Equals(other);
        }

        public Boolean Equals(KeyboardLayout other)
        {
            return Handle == other.Handle;
        }

        public override String? ToString()
        {
            return Culture?.EnglishName;
        }
    }
}