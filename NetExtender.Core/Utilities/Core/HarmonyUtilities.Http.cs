// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using HarmonyLib;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Intercept.Interfaces;
using NetExtender.Types.Network;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        public static class HttpClient
        {
            public readonly struct Handler
            {
                public HttpInterceptEventHandler? Intercept { get; init; }
                public HttpInterceptEventHandler? Request { get; init; }
                public HttpInterceptEventHandler? Response { get; init; }
            }
        }
        
        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public abstract class HttpInterceptHarmonyClient : HttpInterceptClient, IInterceptIdentifierTarget<HttpInterceptHarmonyClient>
        {
            public new static event HttpInterceptEventHandler? Intercept;
            public new static event HttpInterceptEventHandler? SendingRequest;
            public new static event HttpInterceptEventHandler? ResponseReceived;

            static HttpInterceptHarmonyClient()
            {
                Intercept<HttpInterceptHarmonyClient>.Regex = new Regex("(Http|Intercept|Client)", RegexOptions.Compiled);
            }
            
            public sealed override String Identifier
            {
                get
                {
                    return _identifier ??= Intercept<HttpInterceptClient>.GetName(GetType());
                }
                init
                {
                    throw new NotSupportedException();
                }
            }
            
            public HttpInterceptHarmonyClient()
            {
                Initialize();
            }

            public HttpInterceptHarmonyClient(HttpClientHandler? handler)
                : base(handler)
            {
                Initialize();
            }

            protected HttpInterceptHarmonyClient(HttpClientInterceptHandler? handler)
                : base(handler)
            {
                Initialize();
            }

            private static void OnIntercept(HttpInterceptClient sender, HttpInterceptEventArgs args)
            {
                Intercept?.Invoke(sender, args);
            }

            private static void OnSendingRequest(HttpInterceptClient sender, HttpInterceptEventArgs args)
            {
                SendingRequest?.Invoke(sender, args);
            }

            private static void OnResponseReceived(HttpInterceptClient sender, HttpInterceptEventArgs args)
            {
                ResponseReceived?.Invoke(sender, args);
            }

            private void Initialize()
            {
                HttpClient.Handler handler = Intercept<HttpInterceptHarmonyClient, HttpClient.Handler>.Add(this);

                if (handler.Intercept is not null)
                {
                    base.Intercept += handler.Intercept;
                }

                if (handler.Request is not null)
                {
                    base.SendingRequest += handler.Request;
                }

                if (handler.Response is not null)
                {
                    base.ResponseReceived += handler.Response;
                }
                
                base.Intercept += OnIntercept;
                base.SendingRequest += OnSendingRequest;
                base.ResponseReceived += OnResponseReceived;
            }

            protected override void Dispose(Boolean disposing)
            {
                base.Dispose(disposing);
                Intercept -= OnIntercept;
                SendingRequest -= OnSendingRequest;
                ResponseReceived -= OnResponseReceived;
                Intercept<HttpInterceptHarmonyClient>.Remove(this);
            }
        }
        
        private static IEnumerable<CodeInstruction> HttpClientTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (InstantiationMemory.New is not { } @new || @new != typeof(HttpInterceptHarmonyClient) || InstantiationMemory.Old is not { } old || InstantiationMemory.Method is not { } method)
            {
                throw new InvalidOperationException();
            }

            Boolean any = false;
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.operand is not ConstructorInfo constructor || constructor.DeclaringType != old)
                {
                    yield return instruction;
                    continue;
                }
                
                constructor = FindConstructorForInstantiation(@new, constructor) ?? throw new NeverOperationException();
                @new = Intercept<HttpInterceptHarmonyClient>.Seal(method);
                constructor = FindConstructorForInstantiation(@new, constructor) ?? throw new NeverOperationException();

                instruction.operand = constructor;
                yield return instruction;
                any = true;
            }

            if (!any)
            {
                throw new SuccessfulOperationException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? InterceptHttpClient(this MethodBase original)
        {
            return InterceptHttpClient(NetExtender, original);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? InterceptHttpClient(this HarmonyLib.Harmony harmony, MethodBase original)
        {
            return InterceptHttpClient(harmony, original, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? InterceptHttpClient(this MethodBase original, HttpInterceptEventHandler? intercept)
        {
            return InterceptHttpClient(NetExtender, original, intercept, intercept);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? InterceptHttpClient(this HarmonyLib.Harmony harmony, MethodBase original, HttpInterceptEventHandler? request, HttpInterceptEventHandler? response)
        {
            return InterceptHttpClient(harmony, original, new HttpClient.Handler { Request = request, Response = response });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? InterceptHttpClient(MethodBase original, HttpClient.Handler handler)
        {
            return InterceptHttpClient(NetExtender, original, handler);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static MethodInfo? InterceptHttpClient(this HarmonyLib.Harmony harmony, MethodBase original, HttpClient.Handler handler)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            Intercept<HttpInterceptHarmonyClient, HttpClient.Handler>.Information[Intercept<HttpInterceptHarmonyClient>.GetSealName(original)] = handler;
            return ChangeInstantiation(harmony, original, Transpilers.HttpClientTranspiler, typeof(System.Net.Http.HttpClient), typeof(HttpInterceptHarmonyClient));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(this Type type)
        {
            InterceptHttpClient(NetExtender, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(this HarmonyLib.Harmony harmony, Type type)
        {
            InterceptHttpClient(harmony, (HttpInterceptEventHandler?) null, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(this Type type, HttpInterceptEventHandler? intercept)
        {
            InterceptHttpClient(NetExtender, intercept, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(this HarmonyLib.Harmony harmony, HttpInterceptEventHandler? intercept, Type type)
        {
            InterceptHttpClient(harmony, intercept, intercept, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(this Type type, HttpInterceptEventHandler? request, HttpInterceptEventHandler? response)
        {
            InterceptHttpClient(NetExtender, request, response, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(this HarmonyLib.Harmony harmony, HttpInterceptEventHandler? request, HttpInterceptEventHandler? response, Type type)
        {
            InterceptHttpClient(harmony, new HttpClient.Handler { Request = request, Response = response }, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(HttpClient.Handler handler, Type type)
        {
            InterceptHttpClient(NetExtender, handler, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void InterceptHttpClient(this HarmonyLib.Harmony harmony, HttpClient.Handler handler, Type type)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            List<Exception> exceptions = new List<Exception>();
            
            foreach (ConstructorInfo constructor in type.GetConstructors(binding))
            {
                try
                {
                    InterceptHttpClient(harmony, constructor, handler);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }
            
            foreach (MethodInfo method in type.GetMethods(binding))
            {
                try
                {
                    InterceptHttpClient(harmony, method, handler);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(params Type?[]? types)
        {
            InterceptHttpClient(NetExtender, types);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(this HarmonyLib.Harmony harmony, params Type?[]? types)
        {
            InterceptHttpClient(harmony, null, types);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(HttpInterceptEventHandler? response, params Type?[]? types)
        {
            InterceptHttpClient(NetExtender, response, types);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(this HarmonyLib.Harmony harmony, HttpInterceptEventHandler? intercept, params Type?[]? types)
        {
            InterceptHttpClient(harmony, intercept, intercept, types);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(HttpInterceptEventHandler? request, HttpInterceptEventHandler? response, params Type?[]? types)
        {
            InterceptHttpClient(NetExtender, request, response, types);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(this HarmonyLib.Harmony harmony, HttpInterceptEventHandler? request, HttpInterceptEventHandler? response, params Type?[]? types)
        {
            InterceptHttpClient(harmony, new HttpClient.Handler { Request = request, Response = response }, types);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InterceptHttpClient(HttpClient.Handler handler, params Type?[]? types)
        {
            InterceptHttpClient(NetExtender, handler, types);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void InterceptHttpClient(this HarmonyLib.Harmony harmony, HttpClient.Handler handler, params Type?[]? types)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (types is null)
            {
                return;
            }

            List<Exception> exceptions = new List<Exception>();
            foreach (Type? type in types)
            {
                if (type is null)
                {
                    continue;
                }

                try
                {
                    InterceptHttpClient(harmony, handler, type);
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}