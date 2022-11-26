// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Utilities.IO;

namespace NetExtender.Domains.View.Console
{
    public class ConsoleView : ApplicationView
    {
        protected override ApplicationShutdownMode? ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnExplicitShutdown;
            }
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            ConsoleUtilities.CancelKeyPress += ExitHandle;
            return base.RunAsync(token);
        }

        protected virtual void ExitHandle(Object? sender, ConsoleCancelEventArgs cancel)
        {
            cancel.Cancel = true;
            Domain.Shutdown();
        }
    }
}