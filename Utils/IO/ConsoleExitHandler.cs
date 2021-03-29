// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.GUI.WinForms.Forms;

namespace NetExtender.Utils.IO
{
    public static partial class ConsoleUtils
    {
        private static class ConsoleExitHandler
        {
            private delegate Boolean EventHandler(ConsoleCtrlType type);

            [DllImport("kernel32.dll")]
            private static extern Boolean SetConsoleCtrlHandler(EventHandler handler, Boolean add);

            private static EventHandler handler;

            private static Boolean IsHandled { get; set; }

            public static Boolean ExitHandle
            {
                get
                {
                    return IsHandled;
                }
                set
                {
                    if (value)
                    {
                        if (IsHandled)
                        {
                            return;
                        }

                        handler += ExitHandler;
                        SetConsoleCtrlHandler(handler, true);
                        IsHandled = true;
                        return;
                    }

                    if (!IsHandled)
                    {
                        return;
                    }

                    handler -= ExitHandler;
                    SetConsoleCtrlHandler(handler, false);
                    IsHandled = false;
                }
            }

            private static IWindowExitHandler window;
            public static IWindowExitHandler ExitWindow
            {
                get
                {
                    return window;
                }
                set
                {
                    if (window == value)
                    {
                        return;
                    }
                    
                    if (window is not null)
                    {
                        window.WindowClosing -= ExitHandler;
                    }

                    window = value;

                    if (window is not null)
                    {
                        window.WindowClosing += ExitHandler;
                    }
                }
            }

            private static void ExitHandler(Object sender, FormClosingEventArgs e)
            {
                if (e.Cancel || e.CloseReason == CloseReason.None)
                {
                    return;
                }

                ConsoleCtrlType type = e.CloseReason switch
                {
                    CloseReason.WindowsShutDown => ConsoleCtrlType.CtrlShutdownEvent,
                    CloseReason.MdiFormClosing => ConsoleCtrlType.CtrlShutdownEvent,
                    CloseReason.UserClosing => ConsoleCtrlType.CtrlCloseEvent,
                    CloseReason.TaskManagerClosing => ConsoleCtrlType.CtrlTaskManagerClosing,
                    CloseReason.FormOwnerClosing => ConsoleCtrlType.CtrlShutdownEvent,
                    CloseReason.ApplicationExitCall => ConsoleCtrlType.CtrlCloseEvent,
                    CloseReason.None => throw new NotSupportedException(),
                    _ => throw new NotSupportedException()
                };
                
                e.Cancel = ExitHandler(type);
            }

            private static Boolean ExitHandler(ConsoleCtrlType type)
            {
                return ExitHandle && OnConsoleExit(type);
            }

            public static void SetDefaultForm(Boolean force)
            {
                if (!force && ExitWindow is not null)
                {
                    return;
                }
                
                ExitWindow = new ConsoleForm();
            }

            private sealed class ConsoleForm : FixedForm
            {
                public ConsoleForm()
                {
                    FormBorderStyle = FormBorderStyle.None;
                    ShowInTaskbar = false;
                    Visible = false;
                    Load += OnLoad;
                }

                private void OnLoad(Object sender, EventArgs e)
                {
                    Size = new Size(0, 0);
                    Opacity = 0;
                }
            }
        }
    }
}