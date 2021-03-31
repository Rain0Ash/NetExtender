// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Data.Interfaces;
using NetExtender.Apps.Domains.Applications.Interfaces;
using NetExtender.Apps.Reader;
using NetExtender.Events.Args;
using NetExtender.GUI;
using WPFApp = System.Windows.Application;

namespace NetExtender.Apps.Domains.Interfaces
{
    public interface IDomain : IApplication, IDisposable
    {
        public event TypeHandler<TypeHandledEventArgs<AppDataMessage>> AnotherDomainStarted;
        public event TypeHandler<TypeHandledEventArgs<Domain.StartedAppDataMessage>> AnotherDomainHandled;
        
        public ExternalReader MessagesReader { get; }

        public IApplication Application { get; }
        
        public Guid Guid { get; }
        
        public AppVersion Version { get; }
        
        public AppInformation Information { get; }
        
        public DateTime StartedAt { get; }
        
        public IIPCAppData Data { get; }

        public AppStatus Status { get; }

        public String StatusData { get; }
        
        public AppBranch Branch { get; }
        
        public String BranchData { get; }

        public String AppName { get; }
        
        public CultureInfo Culture { get; set; }
        
        public Boolean AlreadyStarted { get; }
        
        public Boolean UseProtocol { get; set; }

        public String ProtocolName { get; }

        public IDomain Initialize(GUIType type);
        public IDomain Initialize<TApp>(GUIType type) where TApp : WPFApp, new();
        public IDomain Initialize(WPFApp app, GUIType type);

        public Task SendMessageAsync(Byte[] message);

        public Task SendMessageAsync(IEnumerable<Byte[]> message);
    }
}