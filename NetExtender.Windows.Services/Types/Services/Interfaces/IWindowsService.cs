// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;

namespace NetExtender.Windows.Services.Types.Services.Interfaces
{
    public interface IWindowsService : IDisposable
    {
        public ServiceBase Service { get; }
        public IContainer? Container { get; }
        public ISite? Site { get; set; }
        public String ServiceName { get; set; }
        public EventLog EventLog { get; }
        public Boolean AutoLog { get; set; }
        public Boolean CanHandlePowerEvent { get; set; }
        public Boolean CanHandleSessionChangeEvent { get; set; }
        public Boolean CanPauseAndContinue { get; set; }
        public Boolean CanShutdown { get; set; }
        public Boolean CanStop { get; set; }
        public Int32 ExitCode { get; set; }
    }
}