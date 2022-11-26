// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Types
{
    /// <summary>
    /// The <see cref="Exception"/> class extensions.
    /// </summary>
    public static class ExceptionUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Throw(this Exception? exception)
        {
            if (exception is not null)
            {
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast()
        {
            Environment.FailFast(null);
            return new NeverOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast(String? message)
        {
            Environment.FailFast(message);
            return new NeverOperationException(message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast(String? message, Exception? exception)
        {
            Environment.FailFast(message, exception);
            return new NeverOperationException(message, exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast(this Exception? exception)
        {
            Environment.FailFast(exception?.Message, exception);
            return new NeverOperationException(exception);
        }

        public static String ToDiagnosticString(this Exception source)
        {
            return ToDiagnosticString(source, null);
        }

        // ReSharper disable once CognitiveComplexity
        private static String ToDiagnosticString(this Exception source, StringBuilder? builder)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            builder ??= new StringBuilder(200);

            for (Exception? exception = source; exception is not null; exception = exception?.InnerException)
            {
                String text = $"Exception: {exception.GetType()}";

                for (Int32 i = 0; i < text.Length; i++)
                {
                    builder.Append('-');
                }

                builder.AppendLine(text);

                if (exception.Message.IsNotNullOrEmpty())
                {
                    builder.AppendLine(exception.Message);
                }

                if (exception.StackTrace.IsNotNullOrEmpty())
                {
                    builder.AppendLine(exception.StackTrace);
                }

                switch (exception)
                {
                    case FileNotFoundException notfound:
                    {
                        builder.AppendLine($"File Name: {notfound.FileName}");
                        builder.AppendLine(notfound.FusionLog.IsNullOrEmpty() ? "Fusion log is empty or disabled." : notfound.FusionLog);

                        break;
                    }
                    case AggregateException aggregate:
                    {
                        Boolean inner = false;

                        foreach (Exception ex in aggregate.InnerExceptions)
                        {
                            inner = inner || ex != exception.InnerException;
                            ToDiagnosticString(ex, builder);
                        }

                        if (inner)
                        {
                            exception = exception.InnerException;
                        }

                        break;
                    }
                    case ReflectionTypeLoadException reflection:
                    {
                        foreach (Exception inner in reflection.LoaderExceptions.WhereNotNull())
                        {
                            ToDiagnosticString(inner, builder);
                        }

                        break;
                    }
                }
            }

            return builder.ToString();
        }

        /// <inheritdoc cref="Handle(System.AggregateException,System.Func{System.Exception,bool},bool)"/>
        public static void Handle(this AggregateException exception, Func<Exception, Boolean> predicate)
        {
            Handle(exception, predicate, false);
        }

        /// <summary>Invokes a handler on each <see cref="Exception"/> contained by this <see cref="AggregateException"/>.</summary>
        /// <param name="exception">The <see cref="AggregateException"/>.</param>
        /// <param name="predicate">
        /// The predicate to execute for each exception. The predicate accepts as an argument the <see cref="Exception"/>
        /// to be processed and returns a <see cref="Boolean"/> to indicate whether the exception was handled.
        /// </param>
        /// <param name="intact">
        /// Whether the rethrown <see cref="AggregateException"/> should maintain the same hierarchy as the original.
        /// </param>
        public static void Handle(this AggregateException exception, Func<Exception, Boolean> predicate, Boolean intact)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (!intact)
            {
                exception.Handle(predicate);
                return;
            }

            AggregateException? result = HandleRecursively(exception, predicate);
            if (result is not null)
            {
                throw result;
            }
        }

        private static AggregateException? HandleRecursively(AggregateException exception, Func<Exception, Boolean> predicate)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            List<Exception> exceptions = new List<Exception>(4);

            foreach (Exception inner in exception.InnerExceptions)
            {
                if (inner is AggregateException aggregate)
                {
                    exceptions.AddIfNotNull(HandleRecursively(aggregate, predicate));
                    continue;
                }

                exceptions.AddIfNot(inner, predicate);
            }

            return exceptions.Count > 0 ? new AggregateException(exception.Message, exceptions) : null;
        }
    }
}