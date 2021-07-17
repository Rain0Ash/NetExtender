// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utils.IO;

namespace NetExtender.Domains.View.Console
{
    public class ConsoleView : ApplicationView
    {
        protected override void InitializeInternal()
        {
            Domain.ShutdownMode = ApplicationShutdownMode.OnExplicitShutdown;
            ConsoleUtils.CancelKeyPress += ExitHandle;
        }

        protected virtual void ExitHandle(Object? sender, ConsoleCancelEventArgs cancel)
        {
            cancel.Cancel = true;
            Domain.Shutdown();
        }
    }
}