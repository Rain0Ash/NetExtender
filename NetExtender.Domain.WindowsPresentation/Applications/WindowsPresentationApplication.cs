// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Types.Dispatchers;
using NetExtender.Types.Dispatchers.Interfaces;

namespace NetExtender.Domains.Applications
{
    public class WindowsPresentationApplication : Application
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
        
        public WindowsPresentationApplication(System.Windows.Application application)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
        }
        
        public override Task<IApplication> RunAsync(CancellationToken token)
        {
            RegisterShutdownToken(token);
            InitializeComponent();
            Application.Dispatcher.Invoke(() => Application.Run());
            return Task.FromResult<IApplication>(this);
        }

        public virtual Task<IApplication> RunAsync(Window? window, CancellationToken token)
        {
            if (window is null)
            {
                return RunAsync(token);
            }

            RegisterShutdownToken(token);
            Application.Dispatcher.Invoke(() => Application.Run(window));
            return Task.FromResult<IApplication>(this);
        }

        public override void Shutdown(Int32 code)
        {
            Application.Dispatcher.Invoke(() => Application.Shutdown(code));
        }

        private void InitializeComponent()
        {
            Application.GetType()
                .GetMethod("InitializeComponent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod)?
                .Invoke(Application, null);
        }
    }
}