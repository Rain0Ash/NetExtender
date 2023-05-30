// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace NetExtender.JavaScript.Utilities.Interop.JavaScript
{
    public static class JavaScriptInteropUtilities
    {
        public static Task<Object> EvaluateAsync(this IJSRuntime runtime, String javascript)
        {
            return EvaluateAsync<Object>(runtime, javascript);
        }

        public static async Task<T> EvaluateAsync<T>(this IJSRuntime runtime, String javascript)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (String.IsNullOrEmpty(javascript))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(javascript));
            }

            return await runtime.InvokeAsync<T>("eval", javascript).ConfigureAwait(false);
        }

        public static async Task AlertAsync(this IJSRuntime runtime, String message)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            await runtime.InvokeAsync<Object>("alert", message).ConfigureAwait(false);
        }

        public static async Task<String> PromptAsync(this IJSRuntime runtime, String message)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return await runtime.InvokeAsync<String>("prompt", message).ConfigureAwait(false);
        }

        public static async Task<Boolean> ConfirmAsync(this IJSRuntime runtime, String message)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            return await runtime.InvokeAsync<Boolean>("confirm", message).ConfigureAwait(false);
        }

        public static async Task<T> JsonParseAsync<T>(this IJSRuntime runtime, String value)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return await runtime.InvokeAsync<T>("JSON.parse", value).ConfigureAwait(false);
        }

        public static async Task<String> JsonStringifyAsync(this IJSRuntime runtime, Object? value)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            return await runtime.InvokeAsync<String>("JSON.stringify", value).ConfigureAwait(false);
        }

        public static async Task PrintAsync(this IJSRuntime runtime)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            await runtime.InvokeAsync<String>("print").ConfigureAwait(false);
        }

        public static async Task ConsoleLogAsync(this IJSRuntime runtime, Object obj)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            await runtime.InvokeAsync<String>("console.log", obj).ConfigureAwait(false);
        }
    }
}