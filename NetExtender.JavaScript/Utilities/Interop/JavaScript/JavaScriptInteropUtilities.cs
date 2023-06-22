// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace NetExtender.JavaScript.Utilities.Interop.JavaScript
{
    public static class JavaScriptInteropUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<Object> EvaluateAsync(this IJSRuntime runtime, String javascript)
        {
            return EvaluateAsync(runtime, javascript, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<Object> EvaluateAsync(this IJSRuntime runtime, String javascript, CancellationToken token)
        {
            return EvaluateAsync<Object>(runtime, javascript, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<T> EvaluateAsync<T>(this IJSRuntime runtime, String javascript)
        {
            return EvaluateAsync<T>(runtime, javascript, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<T> EvaluateAsync<T>(this IJSRuntime runtime, String javascript, CancellationToken token)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (String.IsNullOrEmpty(javascript))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(javascript));
            }

            return runtime.InvokeAsync<T>("eval", token, javascript);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<String> ReadTextAsync(this IJSRuntime runtime)
        {
            return ReadTextAsync(runtime, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<String> ReadTextAsync(this IJSRuntime runtime, CancellationToken token)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            return runtime.InvokeAsync<String>("navigator.clipboard.readText", token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask WriteTextAsync(this IJSRuntime runtime, String? text)
        {
            return WriteTextAsync(runtime, text, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask WriteTextAsync(this IJSRuntime runtime, String? text, CancellationToken token)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            return runtime.InvokeVoidAsync("navigator.clipboard.writeText", token, text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask AlertAsync(this IJSRuntime runtime, String message)
        {
            return AlertAsync(runtime, message, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask AlertAsync(this IJSRuntime runtime, String message, CancellationToken token)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            return runtime.InvokeVoidAsync("alert", token, message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<String> PromptAsync(this IJSRuntime runtime, String message)
        {
            return PromptAsync(runtime, message, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<String> PromptAsync(this IJSRuntime runtime, String message, CancellationToken token)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return runtime.InvokeAsync<String>("prompt", token, message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<Boolean> ConfirmAsync(this IJSRuntime runtime, String message)
        {
            return ConfirmAsync(runtime, message, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<Boolean> ConfirmAsync(this IJSRuntime runtime, String message, CancellationToken token)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            return runtime.InvokeAsync<Boolean>("confirm", token, message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<T> JsonParseAsync<T>(this IJSRuntime runtime, String value)
        {
            return JsonParseAsync<T>(runtime, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<T> JsonParseAsync<T>(this IJSRuntime runtime, String value, CancellationToken token)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return runtime.InvokeAsync<T>("JSON.parse", token, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<String> JsonStringifyAsync(this IJSRuntime runtime, Object? value)
        {
            return JsonStringifyAsync(runtime, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask<String> JsonStringifyAsync(this IJSRuntime runtime, Object? value, CancellationToken token)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            return runtime.InvokeAsync<String>("JSON.stringify", token, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask PrintAsync(this IJSRuntime runtime)
        {
            return PrintAsync(runtime, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask PrintAsync(this IJSRuntime runtime, CancellationToken token)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            await runtime.InvokeAsync<String>("print", token).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask ConsoleLogAsync(this IJSRuntime runtime, Object? value)
        {
            return ConsoleLogAsync(runtime, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async ValueTask ConsoleLogAsync(this IJSRuntime runtime, Object? value, CancellationToken token)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            await runtime.InvokeAsync<String>("console.log", token, value).ConfigureAwait(false);
        }
    }
}