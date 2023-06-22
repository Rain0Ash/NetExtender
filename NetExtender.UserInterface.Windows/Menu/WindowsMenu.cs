// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.UserInterface.Windows.Menu
{
    public class WindowsMenu
    {
        protected IntPtr Handle { get; }
        protected List<WindowsMenuItem> Items { get; }

        public Byte Count
        {
            get
            {
                return (Byte) Items.Count.ToRange(255);
            }
        }

        protected internal WindowsMenu(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(hwnd));
            }

            Handle = hwnd;
            Items = new List<WindowsMenuItem>();
        }

        [SuppressMessage("ReSharper", "ConditionalAccessQualifierIsNonNullableAccordingToAPIContract")]
        public WindowsMenu(IWindow window)
            : this(window?.Handle ?? throw new ArgumentNullException(nameof(window)))
        {
        }

        public virtual Boolean Install()
        {
            Boolean successful = false;
            for (Byte i = 0; i < Count; i++)
            {
                successful |= Items[i].Insert(Handle, i);
            }

            return successful;
        }

        public Boolean Contains(WindowsMenuItem item)
        {
            return Items.Contains(item);
        }

        public Int32 IndexOf(WindowsMenuItem item)
        {
            return Items.IndexOf(item);
        }

        public WindowsMenu Add(WindowsMenuItem item)
        {
            if (Count >= Byte.MaxValue)
            {
                throw new InvalidOperationException();
            }

            Items.Add(item);
            return this;
        }

        public WindowsMenu Insert(Byte index, WindowsMenuItem item)
        {
            if (Count >= Byte.MaxValue)
            {
                throw new InvalidOperationException();
            }

            Items.Insert(index, item);
            return this;
        }

        public WindowsMenu Remove(WindowsMenuItem item)
        {
            Items.Remove(item);
            return this;
        }

        public WindowsMenu Clear()
        {
            Items.Clear();
            return this;
        }

        public WindowsMenuItem this[Byte index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public IEnumerator<WindowsMenuItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}