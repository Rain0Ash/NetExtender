// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;

namespace NetExtender.Domains.WindowsPresentation.Applications
{
    public class WindowsPresentationConsoleApplication<T> : WindowsPresentationConsoleApplication where T : System.Windows.Application, new()
    {
        public WindowsPresentationConsoleApplication()
            : base(new T())
        {
        }

        public WindowsPresentationConsoleApplication(T application)
            : base(application)
        {
        }
    }

    public class WindowsPresentationConsoleApplication : WindowsPresentationApplication
    {
        public WindowsPresentationConsoleApplication(System.Windows.Application application)
            : base(application)
        {
        }

        public override Task<IApplication> RunAsync(CancellationToken token)
        {
            RegisterShutdownToken(token);
            Application.Run();
            return Task.FromResult<IApplication>(this);
        }
    }
}