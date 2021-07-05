// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.View.Interfaces;

namespace NetExtender.Domains.Interfaces
{
    public interface IDomain : IApplication, IDisposable
    {
        public IApplication Application { get; }
        
        public Guid Guid { get; }
        
        public ApplicationVersion Version { get; }
        
        public ApplicationInfo Information { get; }
        
        public DateTime StartedAt { get; }
        
        public IApplicationData Data { get; }

        public ApplicationStatus Status { get; }

        public String StatusData { get; }
        
        public ApplicationBranch Branch { get; }
        
        public String BranchData { get; }

        public String AppName { get; }
        
        public String AppShortName { get; }
        
        public CultureInfo Culture { get; set; }
        
        public Boolean AlreadyStarted { get; }
        
        public IDomain Initialize(IApplication application);
        
        public IDomain View(IApplicationView view);
        public IDomain View(IApplicationView view, String[]? args);
    }
}