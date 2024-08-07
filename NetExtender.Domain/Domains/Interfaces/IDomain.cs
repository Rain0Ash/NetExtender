// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.View.Interfaces;

namespace NetExtender.Domains.Interfaces
{
    public interface IDomain : IApplication, IDisposable
    {
        public IApplication Application { get; }
        public Boolean IsReady { get; }
        public Guid Guid { get; }
        public ApplicationVersion Version { get; }
        public ApplicationInformation Information { get; }
        public DateTime StartedAt { get; }
        public IApplicationInfo Info { get; }
        public ApplicationStatus Status { get; }
        public String StatusInfo { get; }
        public ApplicationBranch Branch { get; }
        public String BranchInfo { get; }
        public String ApplicationName { get; }
        public String ApplicationIdentifier { get; }
        public CultureInfo Culture { get; set; }
        public Boolean HasAnotherInstance { get; }

        public new IDomain Run();
        public new Task<IDomain> RunAsync();
        public new Task<IDomain> RunAsync(CancellationToken token);

        public IDomain Initialize<T>() where T : IApplication, new();
        public IDomain Initialize(IApplication application);

        public IDomain View<T>() where T : IApplicationView, new();
        public IDomain View(IApplicationView view);
        public IDomain View<T>(IEnumerable<String>? args) where T : IApplicationView, new();
        public IDomain View(IApplicationView view, IEnumerable<String>? args);

        public IDomain View<T>(params String[]? args) where T : IApplicationView, new();
        public IDomain View(IApplicationView view, params String[]? args);
        public Task<IDomain> ViewAsync<T>() where T : IApplicationView, new();
        public Task<IDomain> ViewAsync(IApplicationView view);
        public Task<IDomain> ViewAsync<T>(CancellationToken token) where T : IApplicationView, new();
        public Task<IDomain> ViewAsync(IApplicationView view, CancellationToken token);
        public Task<IDomain> ViewAsync<T>(IEnumerable<String>? args) where T : IApplicationView, new();
        public Task<IDomain> ViewAsync(IApplicationView view, IEnumerable<String>? args);
        public Task<IDomain> ViewAsync<T>(params String[]? args) where T : IApplicationView, new();
        public Task<IDomain> ViewAsync(IApplicationView view, params String[]? args);
        public Task<IDomain> ViewAsync<T>(IEnumerable<String>? args, CancellationToken token) where T : IApplicationView, new();
        public Task<IDomain> ViewAsync(IApplicationView view, IEnumerable<String>? args, CancellationToken token);
        public Task<IDomain> ViewAsync<T>(CancellationToken token, params String[] args) where T : IApplicationView, new();
        public Task<IDomain> ViewAsync(IApplicationView view, CancellationToken token, params String[] args);
    }
}