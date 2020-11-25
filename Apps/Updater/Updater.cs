// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Interfaces;
using NetExtender.Types.Network;
using NetExtender.Utils.Types;

namespace NetExtender.Apps.Updater
{
    public sealed partial class Updater : IAsyncTask, IDisposable
    {
        public Boolean IsStarted { get; private set; }
        public Boolean IsPaused { get; private set; }

        private readonly String _link;
        private readonly CancellationTokenSource _source;
        private readonly WebClient _client;

        public Updater(String link, WebClient client = null)
        {
            _link = link;
            _source = new CancellationTokenSource();
            _client = client ?? new FixedWebClient();
            RemoveBackupFile();
        }

        private Task WaitAsync()
        {
            return TaskUtils.TryWaitAsync(() => IsPaused, _source.Token);
        }
        
        public Task StartAsync()
        {
            IsStarted = true;
            return Task.CompletedTask;
        }

        public void Start()
        {
            // ReSharper disable once AsyncConverter.AsyncWait
            StartAsync().Wait();
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }
        
        public void Stop()
        {
            IsStarted = false;
            _source?.Cancel();
            _source?.Dispose();
            _client.Dispose();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}