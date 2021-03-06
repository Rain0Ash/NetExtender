// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utils.IO
{
    public static partial class ConsoleUtils
    {
        private static class AsyncInput
        {
            private static CancellationTokenSource cancellation;
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
                cancellation = new CancellationTokenSource();

                Func<Task> input = type switch
                {
                    ConsoleInputType.None => StopAsyncInputAsync,
                    ConsoleInputType.Line => LineInputHandlerAsync,
                    ConsoleInputType.KeyInfo => KeyInfoInputHandlerAsync,
                    ConsoleInputType.KeyInfoIntercept => KeyInfoInterceptInputHandlerAsync,
                    ConsoleInputType.KeyCode => KeyCodeInputHandlerAsync,
                    _ => throw new NotSupportedException()
                };

                try
                {
                    await Task.Run(input, cancellation.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    await StopAsyncInputAsync().ConfigureAwait(false);
                }
            }
            
            private static Task StopAsyncInputAsync()
            {
                StopRead();
                cancellation?.Cancel();
                cancellation?.Dispose();
                cancellation = null;
                return Task.CompletedTask;
            }

            private static async Task InputHandlerAsync<T>(Func<CancellationToken, Task<T>> handler, Action<T> action)
            {
                while (cancellation?.Token.IsCancellationRequested == false)
                {
                    T value = await handler(cancellation.Token).ConfigureAwait(false);

                    if (value?.Equals(default) != false)
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