// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using NetExtender.Apps.Domains.GUIViews.Common;
using NetExtender.Utils.IO;

namespace NetExtender.Apps.Domains.GUIViews.Console
{
    public class AppConsoleView : AppGUIView
    {
        private protected override void InitializeInternal()
        {
            Domain.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            ConsoleUtils.CancelKeyPress += ExitHandle;
            ConsoleUtils.VTCode = true;
        }

        protected virtual void ExitHandle(Object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            Domain.Shutdown();
        }
    }
}