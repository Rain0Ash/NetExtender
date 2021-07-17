// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Domains.Applications.Interfaces;
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
        public override IApplication Run()
        {
            System.Windows.Forms.Application.Run();
            return this;
        }

        [STAThread]
        public IApplication Run(Form? form)
        {
            if (form is null)
            {
                return Run();
            }

            System.Windows.Forms.Application.Run(form);
            return this;
        }
    }
}