// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Types.Dispatchers.Interfaces;

namespace NetExtender.Domains.WinForms.Applications
{
    public class WinFormsApplication : Application<Form>
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

        [STAThread]
        public override Task<IApplication> RunAsync(Form? form, CancellationToken token)
        {
            RegisterShutdownToken(token);
            System.Windows.Forms.Application.Run(form);
            return Task.FromResult<IApplication>(this);
        }
    }
}