using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using HarmonyLib;
using NetExtender.Types.Assemblies;
using NetExtender.Types.Assemblies.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Interception.Interfaces;
using NetExtender.Types.Network;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Intercept<T> where T : class, IInterceptIdentifierTarget<T>
        {
            internal static IDynamicAssemblyUnsafeStorage Assembly { get; } = new DynamicAssemblyStorage($"{typeof(T).Name}<{nameof(HarmonyLib.Harmony)}>", AssemblyBuilderAccess.Run);
            internal static ConcurrentDictionary<String, Type> SealStorage { get; } = new ConcurrentDictionary<String, Type>();
            internal static ConcurrentDictionary<String, ConcurrentWeakSet<T>> Storage { get; } = new ConcurrentDictionary<String, ConcurrentWeakSet<T>>();
            public static Regex? Regex { get; set; }

            public static IEnumerable<T> Get(String? name)
            {
                if (name is null)
                {
                    yield break;
                }

                if (!Storage.TryGetValue(name, out ConcurrentWeakSet<T>? set))
                {
                    yield break;
                }
                
                foreach (T instance in set)
                {
                    yield return instance;
                }
            }

            public static IEnumerable<KeyValuePair<String, T>> Get()
            {
                return Storage.SelectMany(static pair => Get(pair.Key), static (pair, instance) => new KeyValuePair<String, T>(pair.Key, instance));
            }

            internal static void Remove(T instance)
            {
                if (instance is null)
                {
                    throw new ArgumentNullException(nameof(instance));
                }

                if (Storage.TryGetValue(instance.Identifier, out ConcurrentWeakSet<T>? storage))
                {
                    storage.Remove(instance);
                }
            }

            internal static Type Seal(MethodBase method)
            {
                if (method is null)
                {
                    throw new ArgumentNullException(nameof(method));
                }

                static Type Factory(String name)
                {
                    return ReflectionUtilities.Seal(Assembly, typeof(T), (_, @namespace) => @namespace + name, null);
                }

                return SealStorage.GetOrAdd(GetSealName(method), Factory);
            }

            [return: NotNullIfNotNull("type")]
            internal static String? GetName(Type? type)
            {
                return type is not null ? Regex?.Replace(type.Name, String.Empty) ?? type.Name : null;
            }

            internal static String GetSealName(MethodBase method)
            {
                if (method is null)
                {
                    throw new ArgumentNullException(nameof(method));
                }

                return typeof(T).Name.Replace(nameof(HarmonyLib.Harmony), $"<{method.DeclarationName()}>");
            }
        }

        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        public static class Intercept<T, THandler> where T : class, IInterceptIdentifierTarget<T> where THandler : struct
        {
            internal static ConcurrentDictionary<String, THandler> Information { get; } = new ConcurrentDictionary<String, THandler>();
            
            internal static THandler Add(T instance)
            {
                if (instance is null)
                {
                    throw new ArgumentNullException(nameof(instance));
                }
                
                Intercept<T>.Storage.GetOrAdd(instance.Identifier, new ConcurrentWeakSet<T>()).Add(instance);
                Information.TryGetValue(instance.GetType().Name, out THandler info);
                return info;
            }

            private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                if (InstantiationMemory.New is not { } @new || @new != typeof(T) || InstantiationMemory.Old is not { } old || InstantiationMemory.Method is not { } method)
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
                    @new = Intercept<T>.Seal(method);
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

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static MethodInfo? Apply(HarmonyLib.Harmony harmony, MethodBase original, THandler handler)
            {
                if (harmony is null)
                {
                    throw new ArgumentNullException(nameof(harmony));
                }

                if (original is null)
                {
                    throw new ArgumentNullException(nameof(original));
                }

                Information[Intercept<T>.GetSealName(original)] = handler;
                return ChangeInstantiation(harmony, original, Transpilers.HttpClientTranspiler, typeof(T).BaseType.BaseType, typeof(T));
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static void Apply(HarmonyLib.Harmony harmony, THandler handler, Type type)
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
                        Apply(harmony, constructor, handler);
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
                        Apply(harmony, method, handler);
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

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static void Apply(HarmonyLib.Harmony harmony, THandler handler, params Type?[]? types)
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
                        Apply(harmony, handler, type);
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
}