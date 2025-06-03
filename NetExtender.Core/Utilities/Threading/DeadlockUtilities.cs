// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using NetExtender.Utilities.Types;

#pragma warning disable SYSLIB0003

namespace NetExtender.Utilities.Threading
{
    public delegate void DeadlockHandler(SynchronizationContext context, DeadlockException exception, DeadlockEventArgs args);

    public sealed class DeadlockEventArgs : HandledEventArgs
    {
        public DeadlockEventArgs()
        {
        }

        public DeadlockEventArgs(Boolean handled)
            : base(handled)
        {
        }
    }
    
    public static class DeadlockUtilities
    {
        public static event DeadlockHandler? Deadlock;
        public static Deadlock.Mode Mode { get; set; } = NetExtender.Utilities.Threading.Deadlock.Mode.Actual;
        public static Boolean AllowStackTrace { get; set; } = true;
        private static Detector? Context { get; set; }

        public static Boolean Activate()
        {
            Context ??= Create(Mode);
            return Context is not null;
        }

        public static Boolean Deactivate()
        {
            if (Context is null)
            {
                return false;
            }
            
            Context.Dispose();
            Context = null;
            return true;
        }

        internal static Boolean Raise(SynchronizationContext context, DeadlockException exception)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            DeadlockEventArgs args = new DeadlockEventArgs();
            Deadlock?.Invoke(context, exception, args);
            return args.Handled;
        }

        private static Detector? Create(Deadlock.Mode mode)
        {
            return mode is not Threading.Deadlock.Mode.None ? new Detector(mode) : null;
        }

        public static IDisposable? Detect()
        {
            return Detect(Mode);
        }

        public static IDisposable? Detect(Deadlock.Mode mode)
        {
            return Create(mode);
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private sealed class Detector : IDisposable
        {
            private Deadlock.Mode Mode { get; }
            private Deadlock.SynchronizationContext? Context { get; set; }

            public Detector(Deadlock.Mode mode)
            {
                Mode = mode;
                SynchronizationContext? current = SynchronizationContext.Current;
                
                if (current is null && mode == Threading.Deadlock.Mode.Actual)
                {
                    return;
                }

                Context = new Deadlock.SynchronizationContext(current);
                SynchronizationContext.SetSynchronizationContext(Context);
            }

            public void Dispose()
            {
                if (Context is null)
                {
                    return;
                }

                SynchronizationContext.SetSynchronizationContext(Context.Context);
                Context = null;
            }
        }
    }
    
    public static class Deadlock
    {
        public enum Mode : Byte
        {
            None,
            Actual,
            All
        }

        [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
        internal sealed class SynchronizationContext : System.Threading.SynchronizationContext
        {
            private SyncRoot SyncRoot { get; } = SyncRoot.Create();
            public System.Threading.SynchronizationContext? Context { get; }

            private StackTrace? StackTrace { get; set; }
            private Boolean IsBlocking { get; set; }
            private Thread? Thread { get; set; }

            public SynchronizationContext(System.Threading.SynchronizationContext? context)
            {
                Context = context;
                SetWaitNotificationRequired();
            }

            [SecurityCritical]
            public override Int32 Wait(IntPtr[] source, Boolean all, Int32 timeout)
            {
                if (Thread == Thread.CurrentThread)
                {
                    throw new DeadlockException(StackTrace) { IsPotential = Context is not null };
                }
                
                try
                {
                    lock (SyncRoot)
                    {
                        IsBlocking = true;
                        if (DeadlockUtilities.AllowStackTrace)
                        {
                            StackTrace = new StackTrace();
                        }
                    }

                    System.Threading.SynchronizationContext context = Context ?? this;
                    return context.Wait(source, all, timeout);
                }
                finally
                {
                    IsBlocking = false;
                }
            }

            public override void Send(SendOrPostCallback callback, Object? state)
            {
                if (Context is not null)
                {
                    Context.Send(callback, state);
                    return;
                }

                base.Send(callback, state);
            }

            public override void Post(SendOrPostCallback callback, Object? state)
            {
                Thread = Thread.CurrentThread;

                if (DeadlockUtilities.AllowStackTrace)
                {
                    StackTrace = new StackTrace();
                }

                lock (SyncRoot)
                {
                    if (IsBlocking)
                    {
                        DeadlockException exception = new DeadlockException(StackTrace) { IsPotential = Context is not null };
                        if (DeadlockUtilities.Raise(this, exception))
                        {
                            return;
                        }

                        throw exception;
                    }
                }

                if (Context is null)
                {
                    base.Post(callback, state);
                    return;
                }

                // ReSharper disable once VariableHidesOuterVariable
                void RestoreContextCallback(Object? state)
                {
                    SetSynchronizationContext(this);
                    callback(state);
                }

                Context?.Post(RestoreContextCallback, state);
            }

            public override void OperationStarted()
            {
                if (Context is not null)
                {
                    Context.OperationStarted();
                    return;
                }

                base.OperationStarted();
            }

            public override void OperationCompleted()
            {
                if (Context is not null)
                {
                    Context.OperationCompleted();
                    return;
                }

                base.OperationCompleted();
            }

            public override System.Threading.SynchronizationContext CreateCopy()
            {
                SynchronizationContext copy = new SynchronizationContext(Context?.CreateCopy());
                
                lock (SyncRoot)
                {
                    copy.StackTrace = StackTrace;
                    copy.IsBlocking = IsBlocking;
                }

                return copy;
            }
        }
    }

    [Serializable]
    public sealed class DeadlockException : Exception
    {
        public StackTrace? Trace { get; }
        public Boolean IsPotential { get; init; }
        
        public DeadlockException(StackTrace? trace)
            : base(Format(trace))
        {
            Trace = trace;
        }

        public DeadlockException(StackTrace? trace, String? message)
            : base(message ?? Format(trace))
        {
            Trace = trace;
        }

        public DeadlockException(StackTrace? trace, String? message, Exception? exception)
            : base(message ?? Format(trace), exception)
        {
            Trace = trace;
        }

        public DeadlockException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(StackTrace? trace)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("The blocking operation encountered deadlock.");

            if (trace is null)
            {
                return builder.ToString();
            }

            builder.AppendLine();
            builder.AppendLine("Stack trace where the blocking operation started:");
            builder.Append(trace);

            return builder.ToString();
        }
    }
}