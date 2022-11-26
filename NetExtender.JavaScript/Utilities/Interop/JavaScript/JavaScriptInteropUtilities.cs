// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace NetExtender.JavaScript.Utilities.Interop.JavaScript
{
    public static class JavaScriptInteropUtilities
    {
        public static Task<Object> Evaluate(this IJSRuntime runtime, String javascript)
        {
            return Evaluate<Object>(runtime, javascript);
        }

        public static async Task<T> Evaluate<T>(this IJSRuntime runtime, String javascript)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (String.IsNullOrEmpty(javascript))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(javascript));
            }

            return await runtime.InvokeAsync<T>("eval", javascript);
        }

        public static async Task Alert(this IJSRuntime runtime, String message)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            await runtime.InvokeAsync<Object>("alert", message);
        }

        public static async Task<String> Prompt(this IJSRuntime runtime, String message)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return await runtime.InvokeAsync<String>("prompt", message);
        }

        public static async Task<Boolean> Confirm(this IJSRuntime runtime, String message)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            return await runtime.InvokeAsync<Boolean>("confirm", message);
        }

        public static async Task<T> JsonParse<T>(this IJSRuntime runtime, String value)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return await runtime.InvokeAsync<T>("JSON.parse", value);
        }

        public static async Task<String> JsonStringify(this IJSRuntime runtime, Object? value)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            return await runtime.InvokeAsync<String>("JSON.stringify", value);
        }

        public static async Task Print(this IJSRuntime runtime)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            await runtime.InvokeAsync<String>("print");
        }

        public static async Task ConsoleLog(this IJSRuntime runtime, Object obj)
        {
            if (runtime is null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }

            await runtime.InvokeAsync<String>("console.log", obj);
        }
    }
}