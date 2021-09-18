// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace NetExtender.Utilities.Types
{
    public static class ThreadPoolUtilities
    {
        public static Int32 ThreadCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ThreadPool.ThreadCount;
            }
        }
        
        public static Int64 CompletedWorkItemCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ThreadPool.CompletedWorkItemCount;
            }
        }
        
        public static Int64 PendingWorkItemCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ThreadPool.PendingWorkItemCount;
            }
        }
        
        public static Int32 AvailableThreads
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetAvailableThreads();
            }
        }
        
        public static Int32 AvailableWorkerThreads
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetAvailableWorkerThreads();
            }
        }
        
        public static Int32 AvailableCompletionThreads
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetAvailableCompletionThreads();
            }
        }
        
        public static Int32 MinThreads
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetMinThreads();
            }
        }
        
        public static Int32 MinWorkerThreads
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetMinWorkerThreads();
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (!SetMinWorkerThreads(value))
                {
                    throw new InvalidOperationException();
                }
            }
        }
        
        public static Int32 MinCompletionThreads
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetMinCompletionThreads();
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (!SetMinCompletionThreads(value))
                {
                    throw new InvalidOperationException();
                }
            }
        }
        
        public static Int32 MaxThreads
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetMaxThreads();
            }
        }
        
        public static Int32 MaxWorkerThreads
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetMaxWorkerThreads();
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (!SetMaxWorkerThreads(value))
                {
                    throw new InvalidOperationException();
                }
            }
        }
        
        public static Int32 MaxCompletionThreads
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetMaxCompletionThreads();
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (!SetMaxCompletionThreads(value))
                {
                    throw new InvalidOperationException();
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetAvailableThreads()
        {
            GetAvailableThreads(out Int32 worker, out Int32 completion);
            return worker + completion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetAvailableThreads(out Int32 worker, out Int32 completion)
        {
            ThreadPool.GetAvailableThreads(out worker, out completion);
            return worker + completion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetAvailableWorkerThreads()
        {
            ThreadPool.GetAvailableThreads(out Int32 worker, out _);
            return worker;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetAvailableCompletionThreads()
        {
            ThreadPool.GetAvailableThreads(out _, out Int32 completion);
            return completion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetMinThreads()
        {
            GetMinThreads(out Int32 worker, out Int32 completion);
            return worker + completion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetMinThreads(out Int32 worker, out Int32 completion)
        {
            ThreadPool.GetMinThreads(out worker, out completion);
            return worker + completion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetMinWorkerThreads()
        {
            ThreadPool.GetMinThreads(out Int32 worker, out _);
            return worker;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetMinCompletionThreads()
        {
            ThreadPool.GetMinThreads(out _, out Int32 completion);
            return completion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetMaxThreads()
        {
            GetMaxThreads(out Int32 worker, out Int32 completion);
            return worker + completion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetMaxThreads(out Int32 worker, out Int32 completion)
        {
            ThreadPool.GetMaxThreads(out worker, out completion);
            return worker + completion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetMaxWorkerThreads()
        {
            ThreadPool.GetMaxThreads(out Int32 worker, out _);
            return worker;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetMaxCompletionThreads()
        {
            ThreadPool.GetMaxThreads(out _, out Int32 completion);
            return completion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetMinThreads(Int32 worker, Int32 completion)
        {
            return ThreadPool.SetMinThreads(worker, completion);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetMinWorkerThreads(Int32 worker)
        {
            return ThreadPool.SetMinThreads(worker, MinCompletionThreads);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetMinCompletionThreads(Int32 completion)
        {
            return ThreadPool.SetMinThreads(MinWorkerThreads, completion);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetMaxThreads(Int32 worker, Int32 completion)
        {
            return ThreadPool.SetMaxThreads(worker, completion);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetMaxWorkerThreads(Int32 worker)
        {
            return ThreadPool.SetMaxThreads(worker, MaxCompletionThreads);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetMaxCompletionThreads(Int32 completion)
        {
            return ThreadPool.SetMaxThreads(MaxWorkerThreads, completion);
        }
    }
}