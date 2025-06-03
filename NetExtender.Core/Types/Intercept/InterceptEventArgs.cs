// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using NetExtender.Types.Intercept.Interfaces;

namespace NetExtender.Types.Intercept
{
    public abstract class InterceptEventArgs<T> : InterceptEventArgs where T : IInterceptArgumentInfo
    {
        protected T Info { get; set; }

        protected InterceptEventArgs(T value)
        {
            Info = value;
        }
    }

    public abstract class InterceptEventArgs : InterceptionEventArgs, IInterceptEventArgs
    {
        public static class Default
        {
            public static Object Object { get; } = new Object();
            public static Exception Exception { get; } = new Exception();
        }

        public CancellationToken Token { get; init; }
        public Boolean? IsAsync { get; init; }
        public TimeSpan Wait { get; set; }

        protected override void Clear()
        {
            Wait = default;
            base.Clear();
        }
    }
}