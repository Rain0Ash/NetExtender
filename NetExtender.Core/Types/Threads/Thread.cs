// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Versioning;
using System.Threading;
#pragma warning disable CA1041

namespace NetExtender.Types.Threads
{
    public delegate void ParameterizedThreadStart<in T>(T parameter);
    
    public sealed class Thread<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator Thread?(Thread<T>? value)
        {
            return value?.Internal;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator Thread<T>?(Thread? value)
        {
            return value is not null ? new Thread<T>(value) : null;
        }
        
        private Thread Internal { get; }

        /// <inheritdoc cref="Thread.Name"/>
        public String? Name
        {
            get
            {
                return Internal.Name;
            }
            set
            {
                Internal.Name = value;
            }
        }

        /// <inheritdoc cref="Thread.ManagedThreadId"/>
        public Int32 ManagedThreadId
        {
            get
            {
                return Internal.ManagedThreadId;
            }
        }

        /// <inheritdoc cref="Thread.Priority"/>
        public ThreadPriority Priority
        {
            get
            {
                return Internal.Priority;
            }
            set
            {
                Internal.Priority = value;
            }
        }

        /// <inheritdoc cref="Thread.ApartmentState"/>
        [Obsolete]
        public ApartmentState ApartmentState
        {
            get
            {
                return Internal.ApartmentState;
            }
            set
            {
                Internal.ApartmentState = value;
            }
        }

        /// <inheritdoc cref="Thread.ThreadState"/>
        public ThreadState ThreadState
        {
            get
            {
                return Internal.ThreadState;
            }
        }

        /// <inheritdoc cref="Thread.IsAlive"/>
        public Boolean IsAlive
        {
            get
            {
                return Internal.IsAlive;
            }
        }

        /// <inheritdoc cref="Thread.IsBackground"/>
        public Boolean IsBackground
        {
            get
            {
                return Internal.IsBackground;
            }
            set
            {
                Internal.IsBackground = value;
            }
        }

        /// <inheritdoc cref="Thread.IsThreadPoolThread"/>
        public Boolean IsThreadPoolThread
        {
            get
            {
                return Internal.IsThreadPoolThread;
            }
        }

        /// <inheritdoc cref="Thread.ExecutionContext"/>
        public ExecutionContext? ExecutionContext
        {
            get
            {
                return Internal.ExecutionContext;
            }
        }

        /// <inheritdoc cref="Thread.CurrentCulture"/>
        public CultureInfo CurrentCulture
        {
            get
            {
                return Internal.CurrentCulture;
            }
            set
            {
                Internal.CurrentCulture = value;
            }
        }

        /// <inheritdoc cref="Thread.CurrentUICulture"/>
        public CultureInfo CurrentUICulture
        {
            get
            {
                return Internal.CurrentUICulture;
            }
            set
            {
                Internal.CurrentUICulture = value;
            }
        }

        /// <inheritdoc cref="Thread(ThreadStart)"/>
        public Thread(ThreadStart thread)
        {
            if (thread is null)
            {
                throw new ArgumentNullException(nameof(thread));
            }

            Internal = new Thread(thread);
        }

        /// <inheritdoc cref="Thread(ThreadStart,Int32)"/>
        public Thread(ThreadStart thread, Int32 stack)
        {
            if (thread is null)
            {
                throw new ArgumentNullException(nameof(thread));
            }

            Internal = new Thread(thread, stack);
        }

        /// <inheritdoc cref="Thread(ParameterizedThreadStart)"/>
        public Thread(ParameterizedThreadStart<T> thread)
        {
            if (thread is null)
            {
                throw new ArgumentNullException(nameof(thread));
            }

            Internal = new Thread(@object => thread.Invoke((T) @object!));
        }

        /// <inheritdoc cref="Thread(ParameterizedThreadStart,Int32)"/>
        public Thread(ParameterizedThreadStart<T> thread, Int32 stack)
        {
            if (thread is null)
            {
                throw new ArgumentNullException(nameof(thread));
            }

            Internal = new Thread(@object => thread.Invoke((T) @object!), stack);
        }

        private Thread(Thread value)
        {
            Internal = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc cref="Thread.GetApartmentState"/>
        public ApartmentState GetApartmentState()
        {
            return Internal.GetApartmentState();
        }

        /// <inheritdoc cref="Thread.SetApartmentState(System.Threading.ApartmentState)"/>
        [SupportedOSPlatform("windows")]
        public Thread<T> SetApartmentState(ApartmentState state)
        {
            Internal.SetApartmentState(state);
            return this;
        }

        /// <inheritdoc cref="Thread.TrySetApartmentState(System.Threading.ApartmentState)"/>
        public Boolean TrySetApartmentState(ApartmentState state)
        {
            return Internal.TrySetApartmentState(state);
        }

        /// <inheritdoc cref="Thread.GetCompressedStack()"/>
        [Obsolete]
        public CompressedStack GetCompressedStack()
        {
            return Internal.GetCompressedStack();
        }

        /// <inheritdoc cref="Thread.SetCompressedStack(System.Threading.CompressedStack)"/>
        [Obsolete]
        public Thread<T> SetCompressedStack(CompressedStack stack)
        {
            Internal.SetCompressedStack(stack);
            return this;
        }

        /// <inheritdoc cref="Thread.DisableComObjectEagerCleanup()"/>
        public Thread<T> DisableComObjectEagerCleanup()
        {
            Internal.DisableComObjectEagerCleanup();
            return this;
        }

        /// <inheritdoc cref="Thread.Start()"/>
        public Thread<T> Start()
        {
            Internal.Start();
            return this;
        }

        /// <inheritdoc cref="Thread.Start(System.Object?)"/>
        public Thread<T> Start(T parameter)
        {
            Internal.Start(parameter);
            return this;
        }

        /// <inheritdoc cref="Thread.UnsafeStart()"/>
        public Thread<T> UnsafeStart()
        {
            Internal.UnsafeStart();
            return this;
        }

        /// <inheritdoc cref="Thread.UnsafeStart(System.Object?)"/>
        public Thread<T> UnsafeStart(T parameter)
        {
            Internal.UnsafeStart(parameter);
            return this;
        }

        /// <inheritdoc cref="Thread.Join()"/>
        public void Join()
        {
            Internal.Join();
        }

        /// <inheritdoc cref="Thread.Join(System.Int32)"/>
        public Boolean Join(Int32 timeout)
        {
            return Internal.Join(timeout);
        }

        /// <inheritdoc cref="Thread.Join(System.TimeSpan)"/>
        public Boolean Join(TimeSpan timeout)
        {
            return Internal.Join(timeout);
        }

        /// <inheritdoc cref="Thread.Resume()"/>
        [Obsolete]
        public void Resume()
        {
            Internal.Resume();
        }

        /// <inheritdoc cref="Thread.Suspend()"/>
        [Obsolete]
        public void Suspend()
        {
            Internal.Suspend();
        }

        /// <inheritdoc cref="Thread.Interrupt()"/>
        public void Interrupt()
        {
            Internal.Interrupt();
        }

        /// <inheritdoc cref="Thread.Abort()"/>
        [Obsolete]
        public void Abort()
        {
            Internal.Abort();
        }

        /// <inheritdoc cref="Thread.Abort(System.Object?)"/>
        [Obsolete]
        public void Abort(Object? state)
        {
            Internal.Abort(state);
        }
    }
}