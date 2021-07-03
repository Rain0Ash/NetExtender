// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Domains.Applications;
using NetExtender.Types.Dispatchers.Interfaces;

namespace NetExtender.Domains.AspNetCore.Applications
{
    public class AspNetCoreApplication : Application
    {
        public override IDispatcher? Dispatcher
        {
            get
            {
                return null;
            }
        }

        public override ApplicationShutdownMode ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnExplicitShutdown;
            }
            set
            {
            }
        }

        public override void Run()
        {
        }

        public override void Run<T>(T window)
        {
            Run();
        }
    }
}