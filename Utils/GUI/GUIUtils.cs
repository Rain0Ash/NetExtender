// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using JetBrains.Annotations;
using NetExtender.GUI;
using NetExtender.Utils.Types;
using NetExtender.Utils.WPF;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.Types.Native.Windows;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utils.OS;

namespace NetExtender.Utils.GUI
{
    public class GUIMessage
    {
        public IString Text { get; }
        public IString Title { get; }
        public MessageBoxButtons Buttons { get; set; }
        public MessageBoxIcon Icon { get; set; }
        public MessageBoxDefaultButton DefaultButton { get; set; }
        public System.Windows.Forms.MessageBoxOptions Options { get; set; }
        public Boolean ShowHelpButton { get; set; }

        public GUIMessage(String text)
            : this(text, text)
        {
        }
        
        public GUIMessage(String text, String title)
            : this(text.ToIString(), title.ToIString())
        {
        }
        
        public GUIMessage(IString text)
            : this(text, text)
        {
        }
        
        public GUIMessage(IString text, IString title)
        {
            Text = text;
            Title = title;
        }

        public DialogResult ToMessageBox()
        {
            return System.Windows.Forms.MessageBox.Show(Text.ToString(), Title.ToString(), Buttons, Icon, DefaultButton, Options, ShowHelpButton);
        }
    }
    
    public static class GUIUtils
    {
        public const Int32 Distance = 5;
        public const MessageBoxButtons DefaultMessageBoxButtons = MessageBoxButtons.OK;
        public const MessageBoxIcon DefaultMessageBoxIcon = MessageBoxIcon.Warning;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T>(this T text)
        {
            return ToMessageBox(text, DefaultMessageBoxButtons);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T1, T2>(this T1 text, T2 title)
        {
            return ToMessageBox(text, title, DefaultMessageBoxButtons);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T>(this T text, MessageBoxIcon icon)
        {
            return ToMessageBox(text, DefaultMessageBoxButtons, icon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T1, T2>(this T1 text, T2 title, MessageBoxIcon icon)
        {
            return ToMessageBox(text, title, DefaultMessageBoxButtons, icon);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T>(this T text, MessageBoxButtons buttons)
        {
            return ToMessageBox(text, buttons, DefaultMessageBoxIcon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T1, T2>(this T1 text, T2 title, MessageBoxButtons buttons)
        {
            return ToMessageBox(text, title, buttons, DefaultMessageBoxIcon);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T>(this T text, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            return ToMessageBox(text, buttons, icon);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T1, T2>(this T1 text, T2 title, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            return ToMessageBox(text, title, buttons, icon);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T>(this T text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return System.Windows.Forms.MessageBox.Show(text.GetString(), null, buttons, icon);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DialogResult ToMessageBox<T1, T2>(this T1 text, T2 title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return System.Windows.Forms.MessageBox.Show(text.GetString(), title?.GetString(), buttons, icon);
        }

        public static DialogResult ToDialogResult(this MessageBoxResult result)
        {
            return result switch
            {
                MessageBoxResult.None => DialogResult.None,
                MessageBoxResult.OK => DialogResult.OK,
                MessageBoxResult.Cancel => DialogResult.Cancel,
                MessageBoxResult.Yes => DialogResult.Yes,
                MessageBoxResult.No => DialogResult.No,
                _ => throw new NotSupportedException()
            };
        }
        
        public static MessageBoxResult ToMessageBoxResult(this DialogResult result)
        {
            return result switch
            {
                DialogResult.None => MessageBoxResult.None,
                DialogResult.OK => MessageBoxResult.OK,
                DialogResult.Cancel => MessageBoxResult.Cancel,
                DialogResult.Abort => MessageBoxResult.Cancel,
                DialogResult.Retry => MessageBoxResult.Cancel,
                DialogResult.Ignore => MessageBoxResult.Cancel,
                DialogResult.Yes => MessageBoxResult.Yes,
                DialogResult.No => MessageBoxResult.No,
                _ => throw new NotSupportedException()
            };
        }

        public static Boolean ToBoolean(this MessageBoxResult value)
        {
            return value switch
            {
                MessageBoxResult.None => false,
                MessageBoxResult.OK => true,
                MessageBoxResult.Cancel => false,
                MessageBoxResult.Yes => true,
                MessageBoxResult.No => false,
                _ => throw new NotSupportedException()
            };
        }
        
        public static Boolean ToBoolean(this DialogResult value)
        {
            return value switch
            {
                DialogResult.None => false,
                DialogResult.OK => true,
                DialogResult.Cancel => false,
                DialogResult.Abort => false,
                DialogResult.Retry => true,
                DialogResult.Ignore => false,
                DialogResult.Yes => true,
                DialogResult.No => false,
                _ => throw new NotSupportedException()
            };
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, out UInt32 lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        [DllImport("kernel32.dll")]
        private static extern UInt32 GetCurrentThreadId();

        [DllImport("user32.dll")] 
        private static extern IntPtr GetForegroundWindow(); 

        [DllImport("user32.dll")]
        private static extern Boolean AttachThreadInput(UInt32 idAttach, UInt32 idAttachTo, Boolean fAttach); 

        [DllImport("user32.dll", SetLastError = true)] 
        private static extern Boolean BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, UInt32 nCmdShow);
        
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetWindowRect(IntPtr hWnd, out WinRectangle lpRect);

        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            if (!GetWindowRect(handle, out WinRectangle rectangle))
            {
                InteropUtils.ThrowLastWin32Exception();
            }

            return rectangle;
        }
        
        public static Rectangle GetWindowRectangle([NotNull] this Form form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return GetWindowRectangle(form.Handle);
        }
        
        public static Rectangle GetWindowRectangle([NotNull] this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return GetWindowRectangle(window.GetHandle());
        }

        public static Rectangle GetWindowRectangle<T>([NotNull] this T window) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return GetWindowRectangle(window.Handle);
        }

        //TODO: добавить установку, проверку и удаление пунктов меню
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, Boolean bRevert);
    
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean AppendMenu(IntPtr hMenu, Int32 uFlags, Int32 uIDNewItem, String lpNewItem);
    
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Boolean InsertMenu(IntPtr hMenu, Int32 uPosition, Int32 uFlags, Int32 uIDNewItem, String lpNewItem);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern Int32 DeleteMenu(IntPtr hMenu, Int32 nPosition, Int32 wFlags);
        
        public static Boolean ShowWindow(IntPtr handle, WindowStateType state)
        {
            return ShowWindow(handle, (UInt32) state);
        }
        
        public static Boolean ShowWindow([NotNull] this Form form, WindowStateType state)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return ShowWindow(form.Handle, state);
        }
        
        public static Boolean ShowWindow([NotNull] this Window window, WindowStateType state)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return ShowWindow(window.GetHandle(), state);
        }
        
        public static Boolean ShowWindow<T>([NotNull] this T window, WindowStateType state) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return ShowWindow(window.Handle, state);
        }

        private static Boolean Bring(IntPtr hWnd)
        {
            BringWindowToTop(hWnd);
            ShowWindow(hWnd, 5);
            return true;
        }

        private static Boolean AttachAndBring(UInt32 thread, UInt32 application, IntPtr hWnd)
        {
            AttachThreadInput(thread, application, true);
            Bring(hWnd);
            AttachThreadInput(thread, application, false);
            return true;
        }
        
        private static Boolean BringToForegroundWindow(IntPtr hWnd)
        {
            UInt32 thread = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            UInt32 application = GetCurrentThreadId();

            return thread == application ? Bring(hWnd) : AttachAndBring(thread, application, hWnd);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean BringToForeground([NotNull] this Form form)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return BringToForegroundWindow(form.Handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean BringToForeground([NotNull] this Window window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return BringToForegroundWindow(window.GetHandle());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean BringToForeground([NotNull] IGUIHandle handle)
        {
            if (handle is null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            return BringToForegroundWindow(handle.Handle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize([NotNull] this Form form)
        {
            SetPrimaryScreenPercentageSize(form, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize([NotNull] this Form form, Byte percentage)
        {
            SetPrimaryScreenPercentageSize(form, percentage, percentage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize([NotNull] this Form form, Byte width, Byte height)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            SetScreenPercentageSize(form, Screen.PrimaryScreen, width, height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize([NotNull] this Form form)
        {
            SetHandleScreenPercentageSize(form, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize([NotNull] this Form form, Byte percentage)
        {
            SetHandleScreenPercentageSize(form, percentage, percentage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize([NotNull] this Form form, Byte width, Byte height)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            SetScreenPercentageSize(form, Screen.FromHandle(form.Handle), width, height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize([NotNull] this Form form, Screen screen)
        {
            SetScreenPercentageSize(form, screen, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize([NotNull] this Form form, Screen screen, Byte percentage)
        {
            SetScreenPercentageSize(form, screen, percentage, percentage);
        }
        
        public static void SetScreenPercentageSize([NotNull] this Form form, Screen screen, Byte width, Byte height)
        {
            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            if (width > 100)
            {
                width = 100;
            }

            if (height > 100)
            {
                height = 100;
            }

            form.Width = (Int32) (width / 100d * screen.WorkingArea.Width);
            form.Height = (Int32) (height / 100d * screen.WorkingArea.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize([NotNull] this Window window)
        {
            SetPrimaryScreenPercentageSize(window, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize([NotNull] this Window window, Byte percentage)
        {
            SetPrimaryScreenPercentageSize(window, percentage, percentage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize([NotNull] this Window window, Byte width, Byte height)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            SetScreenPercentageSize(window, Screen.PrimaryScreen, width, height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize([NotNull] this Window window)
        {
            SetHandleScreenPercentageSize(window, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize([NotNull] this Window window, Byte percentage)
        {
            SetHandleScreenPercentageSize(window, percentage, percentage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize([NotNull] this Window window, Byte width, Byte height)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            SetScreenPercentageSize(window, Screen.FromHandle(window.GetHandle()), width, height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize([NotNull] this Window window, Screen screen)
        {
            SetScreenPercentageSize(window, screen, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize([NotNull] this Window window, Screen screen, Byte percentage)
        {
            SetScreenPercentageSize(window, screen, percentage, percentage);
        }
        
        public static void SetScreenPercentageSize([NotNull] this Window window, Screen screen, Byte width, Byte height)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (width > 100)
            {
                width = 100;
            }

            if (height > 100)
            {
                height = 100;
            }

            window.Width = width / 100d * screen.WorkingArea.Width;
            window.Height = height / 100d * screen.WorkingArea.Height;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize<T>([NotNull] this T window) where T : IWindow
        {
            SetPrimaryScreenPercentageSize(window, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize<T>([NotNull] this T window, Byte percentage) where T : IWindow
        {
            SetPrimaryScreenPercentageSize(window, percentage, percentage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPrimaryScreenPercentageSize<T>([NotNull] this T window, Byte width, Byte height) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            SetScreenPercentageSize(window, Screen.PrimaryScreen, width, height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize<T>([NotNull] this T window) where T : IWindow
        {
            SetHandleScreenPercentageSize(window, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize<T>([NotNull] this T window, Byte percentage) where T : IWindow
        {
            SetHandleScreenPercentageSize(window, percentage, percentage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHandleScreenPercentageSize<T>([NotNull] this T window, Byte width, Byte height) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            SetScreenPercentageSize(window, Screen.FromHandle(window.Handle), width, height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize<T>([NotNull] this T window, Screen screen) where T : IWindow
        {
            SetScreenPercentageSize(window, screen, 50);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScreenPercentageSize<T>([NotNull] this T window, Screen screen, Byte percentage) where T : IWindow
        {
            SetScreenPercentageSize(window, screen, percentage, percentage);
        }

        public static void SetScreenPercentageSize<T>([NotNull] this T window, Screen screen, Byte width, Byte height) where T : IWindow
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (width > 100)
            {
                width = 100;
            }

            if (height > 100)
            {
                height = 100;
            }

            window.Width = width / 100d * screen.WorkingArea.Width;
            window.Height = height / 100d * screen.WorkingArea.Height;
        }
    }
}