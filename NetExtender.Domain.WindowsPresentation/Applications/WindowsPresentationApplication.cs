// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Types.Dispatchers;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Domains.WindowsPresentation.Applications
{
    public class WindowsPresentationApplication<T> : WindowsPresentationApplication where T : System.Windows.Application, new()
    {
        public WindowsPresentationApplication()
            : base(new T())
        {
        }

        public WindowsPresentationApplication(T application)
            : base(application)
        {
        }
    }

    public class WindowsPresentationApplication : Application<Window>
    {
        public System.Windows.Application Application { get; }

        public override IDispatcher Dispatcher { get; } = new DispatcherWrapper(System.Windows.Application.Current.Dispatcher);

        public override ApplicationShutdownMode ShutdownMode
        {
            get
            {
                return Dispatcher.Invoke(() => (ApplicationShutdownMode) Application.ShutdownMode);
            }
            set
            {
                if (ShutdownMode == value)
                {
                    return;
                }

                try
                {
                    Application.ShutdownMode = (ShutdownMode) value;
                }
                catch(InvalidOperationException)
                {
                    //ignore
                }
            }
        }

        public WindowsPresentationApplication()
            : this(new System.Windows.Application())
        {
        }

        public WindowsPresentationApplication(System.Windows.Application application)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public override Task<IApplication> RunAsync(Window? window, CancellationToken token)
        {
            Context = window;
            RegisterShutdownToken(token);

            if (window is null)
            {
                InitializeComponent();
            }
            
            Application.Dispatcher.Invoke(() => Application.Run(Context));
            return Task.FromResult<IApplication>(this);
        }

        public override void Shutdown(Int32 code)
        {
            Application.Dispatcher.Invoke(() => Application.Shutdown(code));
        }

        [ReflectionNaming]
        private void InitializeComponent()
        {
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod;
            Application.GetType().GetMethod(nameof(InitializeComponent), binding)?.Invoke(Application, null);
        }
    }
}