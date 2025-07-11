// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NetExtender.Types.Disposable
{
    public readonly struct AwaitableDisposable<T> where T : IDisposable
    {
        public static implicit operator Task<T>(AwaitableDisposable<T> source)
        {
            return source.AsTask();
        }

        private Task<T> Internal { get; }

        public AwaitableDisposable(Task<T> value)
        {
            Internal = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Task<T> AsTask()
        {
            return Internal;
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return Internal.GetAwaiter();
        }

        public ConfiguredTaskAwaitable<T> ConfigureAwait(Boolean continueOnCapturedContext)
        {
            return Internal.ConfigureAwait(continueOnCapturedContext);
        }
    }
}