using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.Exceptions.Interfaces;

namespace NetExtender.Monads
{
    public partial struct RayIdContext
    {
        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        internal static class Scope
        {
            private static readonly AsyncLocal<RayIdContext> context = new AsyncLocal<RayIdContext>();
            internal static RayIdContext Context
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return context.Value;
                }
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                set
                {
                    context.Value = value;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static void Set(IUnsafeTraceException exception, in RayIdContext context)
            {
                exception.Set(context);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [return: NotNullIfNotNull("exception")]
            public static TException? Set<TException>(TException? exception) where TException : Exception
            {
                if (exception is IUnsafeTraceException @unsafe)
                {
                    Set(@unsafe, Context);
                }

                return exception;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [return: NotNullIfNotNull("exception")]
            public static TException? Set<TException>(TException? exception, in RayIdContext context) where TException : Exception
            {
                if (exception is IUnsafeTraceException @unsafe)
                {
                    Set(@unsafe, in context);
                }

                return exception;
            }

            public static Switch Enter(RayIdContext context)
            {
                RayIdContext previous = Context;
                Context = context;
                return new Switch(previous);
            }

            public static ref RayIdContext With(ref RayIdContext context)
            {
                Context = context;
                return ref context;
            }

            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            public struct Switch : IDisposable
            {
                private RayIdContext? Context;

                public Switch(RayIdContext context)
                {
                    Context = context;
                }

                public void Dispose()
                {
                    if (Context is not { } context)
                    {
                        return;
                    }

                    Context = null;
                    Scope.Context = context;
                }
            }
        }
    }
}