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
using NetExtender.WindowsPresentation.Types;
using NetExtender.WindowsPresentation.Types.Applications.Interfaces;

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

    public class WindowsPresentationApplication : Application<Window>, IDependencyApplication
    {
        public System.Windows.Application Application { get; }
        
        public WindowsPresentationServiceProvider Provider
        {
            get
            {
                return WindowsPresentationServiceProvider.Internal;
            }
        }

        public override IDispatcher Dispatcher { get; }
        
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
                    Dispatcher.Invoke(() => Application.ShutdownMode = (ShutdownMode) value);
                }
                catch(InvalidOperationException)
                {
                    //ignore
                }
            }
        }
        
        ShutdownMode IDependencyApplication.ShutdownMode
        {
            get
            {
                return Dispatcher.Invoke(() => Application.ShutdownMode);
            }
            set
            {
                Dispatcher.Invoke(() => Application.ShutdownMode = value);
            }
        }

        public WindowsPresentationApplication()
            : this(new System.Windows.Application())
        {
        }

        public WindowsPresentationApplication(System.Windows.Application application)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
            Dispatcher = new DispatcherWrapper(Application.Dispatcher);
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

        [ReflectionSignature(typeof(System.Windows.Application))]
        private void InitializeComponent()
        {
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod;
            Application.GetType().GetMethod(nameof(InitializeComponent), binding, Type.EmptyTypes)?.Invoke(Application, null);
        }
    }
}