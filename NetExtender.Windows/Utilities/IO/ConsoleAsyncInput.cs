// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.IO
{
    public static partial class ConsoleAsyncInputUtilities
    {
        private static class AsyncInput
        {
            private static CancellationTokenSource? source;

            private static ConsoleInputType inputType;
            public static ConsoleInputType InputType
            {
                get
                {
                    return inputType;
                }
                set
                {
                    if (value == inputType)
                    {
                        return;
                    }

                    inputType = value;

                    StartAsyncInputAsync(inputType).Start();
                }
            }

            private static async Task StartAsyncInputAsync(ConsoleInputType type)
            {
                await StopAsyncInputAsync().ConfigureAwait(false);

                source = new CancellationTokenSource();

                Func<Task> input = type switch
                {
                    ConsoleInputType.None => StopAsyncInputAsync,
                    ConsoleInputType.Line => LineInputHandlerAsync,
                    ConsoleInputType.KeyInfo => KeyInfoInputHandlerAsync,
                    ConsoleInputType.KeyInfoIntercept => KeyInfoInterceptInputHandlerAsync,
                    ConsoleInputType.KeyCode => KeyCodeInputHandlerAsync,
                    _ => throw new EnumUndefinedOrNotSupportedException<ConsoleInputType>(type, nameof(type), null)
                };

                try
                {
                    await Task.Run(input, source.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    await StopAsyncInputAsync().ConfigureAwait(false);
                }
            }

            private static Task StopAsyncInputAsync()
            {
                StopRead();
                source?.Cancel();
                source?.Dispose();
                source = null;

                return Task.CompletedTask;
            }

            private static async Task InputHandlerAsync<T>(Func<CancellationToken, Task<T>> handler, Action<T> action)
            {
                while (source?.Token.IsCancellationRequested is false)
                {
                    T value = await handler(source.Token).ConfigureAwait(false);

                    if (value?.Equals(default) is not false)
                    {
                        continue;
                    }

                    action.Invoke(value);
                }
            }

            private static Task LineInputHandlerAsync()
            {
                return InputHandlerAsync(ReadLineAsync, OnConsoleLineInput);
            }

            private static Task KeyInfoInputHandlerAsync()
            {
                return InputHandlerAsync(ReadKeyAsync, OnConsoleKeyInfoInput);
            }

            private static Task KeyInfoInterceptInputHandlerAsync()
            {
                return InputHandlerAsync(ReadKeyInterceptAsync, OnConsoleKeyInfoInput);
            }

            private static Task KeyCodeInputHandlerAsync()
            {
                return InputHandlerAsync(ReadAsync, OnConsoleKeyCodeInput);
            }
        }
    }
}