using System;
using System.Threading;
using NetExtender.Types.Interception.Interfaces;

namespace NetExtender.Types.Interception
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