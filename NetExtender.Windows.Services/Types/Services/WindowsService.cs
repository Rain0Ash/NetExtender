// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ServiceProcess;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Windows.Services.Types.Services
{
    public class WindowsService : ServiceBase, IWindowsService
    {
        public ServiceBase Service
        {
            get
            {
                return this;
            }
        }
    }
}