// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Exceptions;
using NetExtender.Types.Dispatchers.Interfaces;

namespace NetExtender.Domains.Applications
{
    public class WinFormsApplication : Application
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
                return ApplicationShutdownMode.OnLastWindowClose;
            }
            set
            {
            }
        }

        protected internal WinFormsApplication()
        {
        }
        
        [STAThread]
        public override void Run()
        {
            System.Windows.Forms.Application.Run();
        }

        [STAThread]
        public void Run(Form? form)
        {
            if (form is not null)
            {
                System.Windows.Forms.Application.Run(form);
                return;
            }
            
            Run();
        }
        
        public override void Run<T>(T window)
        {
            Run(window as Form);
        }
    }
}