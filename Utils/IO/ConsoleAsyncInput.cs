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

                    StartAsyncInput(inputType);
                }
            }

            private static async void StartAsyncInput(ConsoleInputType type)
            {
                StopAsyncInput();
                cancellation = new CancellationTokenSource();

                Action input = type switch
                {
                    ConsoleInputType.None => StopAsyncInput,
                    ConsoleInputType.Line => LineInputHandler,
                    ConsoleInputType.KeyInfo => KeyInfoInputHandler,
                    ConsoleInputType.KeyInfoIntercept => KeyInfoInterceptInputHandler,
                    ConsoleInputType.KeyCode => KeyCodeInputHandler,
                    _ => throw new NotSupportedException()
                };

                try
                {
                    await Task.Run(input, cancellation.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    StopAsyncInput();
                }
            }
            
            private static void StopAsyncInput()
            {
                StopRead();
                cancellation?.Cancel();
                cancellation?.Dispose();
                cancellation = null;
            }

            private static async Task InputHandlerAsync<T>(Func<CancellationToken, Task<T>> handler, Action<T> @event)
            {
                while (cancellation?.Token.IsCancellationRequested == false)
                {
                    T value = await handler(cancellation.Token).ConfigureAwait(false);

                    if (value?.Equals(default) != false)
                    {
                        continue;
                    }

                    @event.Invoke(value);
                }
            }
            
            private static async void LineInputHandler()
            {
                // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
                await InputHandlerAsync(ReadLineAsync, OnConsoleLineInput).ConfigureAwait(false);
            }

            private static async void KeyInfoInputHandler()
            {
                // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
                await InputHandlerAsync(ReadKeyAsync, OnConsoleKeyInfoInput).ConfigureAwait(false);
            }

            private static async void KeyInfoInterceptInputHandler()
            {
                // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
                await InputHandlerAsync(ReadKeyInterceptAsync, OnConsoleKeyInfoInput).ConfigureAwait(false);
            }

            private static async void KeyCodeInputHandler()
            {
                // ReSharper disable once AsyncConverter.AsyncAwaitMayBeElidedHighlighting
                await InputHandlerAsync(ReadAsync, OnConsoleKeyCodeInput).ConfigureAwait(false);
            }
        }
    }
}