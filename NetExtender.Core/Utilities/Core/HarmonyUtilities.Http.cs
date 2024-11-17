using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using HarmonyLib;
using NetExtender.Types.Assemblies;
using NetExtender.Types.Assemblies.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Network;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        public static class Http
        {
            internal readonly struct InterceptInfo
            {
                public HttpInterceptEventHandler? Request { get; }
                public HttpInterceptEventHandler? Response { get; }
                
                public InterceptInfo(HttpInterceptEventHandler? request, HttpInterceptEventHandler? response)
                {
                    Request = request;
                    Response = response;
                }
            }
            
            internal static ConcurrentDictionary<String, ConcurrentWeakSet<HttpInterceptClient>> Storage { get; }
            internal static ConcurrentDictionary<String, InterceptInfo> Intercept { get; } = new ConcurrentDictionary<String, InterceptInfo>();
            
            static Http()
            {
                Storage = new ConcurrentDictionary<String, ConcurrentWeakSet<HttpInterceptClient>>();
            }

            public static IEnumerable<HttpInterceptClient> Get(String? name)
            {
                if (name is null)
                {
                    yield break;
                }

                if (!Storage.TryGetValue(name, out ConcurrentWeakSet<HttpInterceptClient>? set))
                {
                    yield break;
                }
                
                foreach (HttpInterceptClient client in set)
                {
                    yield return client;
                }
            }

            public static IEnumerable<KeyValuePair<String, HttpInterceptClient>> Get()
            {
                return Storage.SelectMany(static pair => Get(pair.Key), static (pair, client) => new KeyValuePair<String, HttpInterceptClient>(pair.Key, client));
            }
        }
        
        [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
        public abstract class HttpInterceptHarmonyClient : HttpInterceptClient
        {
            private static IDynamicAssemblyUnsafeStorage Assembly { get; } = new DynamicAssemblyStorage($"{nameof(HttpInterceptHarmonyClient)}<{nameof(Seal)}>", AssemblyBuilderAccess.Run);
            private static ConcurrentDictionary<String, Type> Storage { get; } = new ConcurrentDictionary<String, Type>();
            private static Regex Regex { get; } = new Regex("(Http|Intercept|Client)", RegexOptions.Compiled);
            
            public new static event HttpInterceptEventHandler? Intercept;
            public new static event HttpInterceptEventHandler? SendingRequest;
            public new static event HttpInterceptEventHandler? ResponseReceived;
            
            public sealed override String Name
            {
                get
                {
                    return _name ??= GetName(GetType());
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
                Http.Storage.GetOrAdd(Name, new ConcurrentWeakSet<HttpInterceptClient>()).Add(this);
                Http.Intercept.TryGetValue(GetType().Name, out Http.InterceptInfo intercept);

                if (intercept.Request is not null)
                {
                    base.SendingRequest += intercept.Request;
                }

                if (intercept.Response is not null)
                {
                    base.ResponseReceived += intercept.Response;
                }
                
                base.Intercept += OnIntercept;
                base.SendingRequest += OnSendingRequest;
                base.ResponseReceived += OnResponseReceived;
            }

            internal static Type Seal(MethodBase method)
            {
                if (method is null)
                {
                    throw new ArgumentNullException(nameof(method));
                }

                static Type Factory(String name)
                {
                    return ReflectionUtilities.Seal(Assembly, typeof(HttpInterceptHarmonyClient), (_, @namespace) => @namespace + name, null);
                }

                return Storage.GetOrAdd(GetSealName(method), Factory);
            }

            [return: NotNullIfNotNull("type")]
            internal static String? GetName(Type? type)
            {
                return type is not null ? Regex.Replace(type.Name, String.Empty) : null;
            }

            internal static String GetSealName(MethodBase method)
            {
                if (method is null)
                {
                    throw new ArgumentNullException(nameof(method));
                }

                return nameof(HttpInterceptHarmonyClient).Replace(nameof(HarmonyLib.Harmony), $"<{method.DeclarationName()}>");
            }

            private void Remove()
            {
                if (Http.Storage.TryGetValue(Name, out ConcurrentWeakSet<HttpInterceptClient>? storage))
                {
                    storage.Remove(this);
                }
            }

            protected override void Dispose(Boolean disposing)
            {
                base.Dispose(disposing);
                Intercept -= OnIntercept;
                SendingRequest -= OnSendingRequest;
                ResponseReceived -= OnResponseReceived;
                Remove();
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
                @new = HttpInterceptHarmonyClient.Seal(method);
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

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static MethodInfo? InterceptHttpClient(this HarmonyLib.Harmony harmony, MethodBase original, HttpInterceptEventHandler? request, HttpInterceptEventHandler? response)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }

            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            Http.Intercept[HttpInterceptHarmonyClient.GetSealName(original)] = new Http.InterceptInfo(request, response);
            return ChangeInstantiation(harmony, original, Transpilers.HttpClientTranspiler, typeof(HttpClient), typeof(HttpInterceptHarmonyClient));
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

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void InterceptHttpClient(this HarmonyLib.Harmony harmony, HttpInterceptEventHandler? request, HttpInterceptEventHandler? response, Type type)
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
                    InterceptHttpClient(harmony, constructor, request, response);
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
                    InterceptHttpClient(harmony, method, request, response);
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

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static void InterceptHttpClient(this HarmonyLib.Harmony harmony, HttpInterceptEventHandler? request, HttpInterceptEventHandler? response, params Type?[]? types)
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
                    InterceptHttpClient(harmony, request, response, type);
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