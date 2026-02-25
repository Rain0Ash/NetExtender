// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Cecil;
using NetExtender.Types.Attributes;
using NetExtender.Types.Comparers;
using NetExtender.Types.Concurrent.Observable;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    public readonly struct AssemblyScanInfo
    {
        public static implicit operator AssemblyScanInfo((Assembly Assembly, TypeSet Result, DateTime StartTime, TimeSpan ScanInit, TimeSpan? StaticInit) value)
        {
            return new AssemblyScanInfo { Assembly = value.Assembly, Result = value.Result, StartTime = value.StartTime, ScanInit = value.ScanInit, StaticInit = value.StaticInit };
        }

        public Assembly Assembly { get; private init; }
        public TypeSet Result { get; private init; }
        public DateTime StartTime { get; private init; }
        public TimeSpan ScanInit { get; private init; }
        public TimeSpan? StaticInit { get; private init; }

        public DateTime EndTime
        {
            get
            {
                return StartTime + TotalDuration;
            }
        }

        public TimeSpan TotalDuration
        {
            get
            {
                return ScanInit + (StaticInit ?? TimeSpan.Zero);
            }
        }

        public override String ToString()
        {
            return $"{Assembly.FullName} ({Result.Count}) | {StartTime} -> {EndTime} | {ScanInit} -> {StaticInit} ({TotalDuration})";
        }
    }

    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public static partial class ReflectionUtilities
    {
        public static Assembly CallingAssembly { get; } = Assembly.GetCallingAssembly();
        public static Assembly? EntryAssembly { get; } = GetEntryAssembly();

        private static ManualResetEventSlim ScanProcess
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return AssemblyLoadGcDebouncer.Instance.LoadGate;
            }
        }

        private static readonly IResettableLazy<InheritEvaluator> inherit = new ResettableLazy<InheritEvaluator>(InheritEvaluator.Create, LazyThreadSafetyMode.ExecutionAndPublication);
        public static Inherit.Result Inherit
        {
            get
            {
                if (!ScanProcess.IsSet)
                {
                    ScanProcess.Wait();
                }

                return inherit.Value;
            }
        }

        private static volatile Int32 scanning;
        public static Int32 Scanning
        {
            get
            {
                return scanning;
            }
        }

        public static ConcurrentObservableSortedDictionary<Assembly, AssemblyScanInfo> Assemblies { get; } = new ConcurrentObservableSortedDictionary<Assembly, AssemblyScanInfo>(AssemblyComparer.FullNameOrdinal);
        public static ConcurrentObservableSortedDictionary<Assembly, AssemblyScanInfo> CustomAssemblies { get; } = new ConcurrentObservableSortedDictionary<Assembly, AssemblyScanInfo>(CustomAssemblyComparer.Instance);

        public static Boolean AssemblyLoadCallStaticConstructor { get; set; }
        public static event EmptyHandler? Reset;

        static ReflectionUtilities()
        {
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;

            Interlocked.Increment(ref scanning);

            Task.Factory.StartNew(static () =>
            {
                try
                {
                    if (ScanProcess.IsSet)
                    {
                        ScanProcess.Reset();
                    }

                    Parallel.ForEach(AppDomain.CurrentDomain.GetAssemblies(), static assembly =>
                    {
                        if (ProcessAssembly(assembly) is not { } info)
                        {
                            return;
                        }

                        Assemblies.Add(info.Assembly, info);

                        if (!IsSystemAssembly(info.Assembly))
                        {
                            CustomAssemblies.Add(info.Assembly, info);
                        }
                    });
                }
                finally
                {
                    AssemblyLoadGcDebouncer.Instance.Release();

                    if (Interlocked.Decrement(ref scanning) == 0)
                    {
                        inherit.Reset(InheritEvaluator.Create);
                        Reset?.Invoke();
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        // ReSharper disable once CognitiveComplexity
        private static void OnAssemblyLoad(Object? sender, AssemblyLoadEventArgs args)
        {
            Assembly assembly = args.LoadedAssembly;

            Interlocked.Increment(ref scanning);

            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (ProcessAssembly(assembly) is not { } info)
                    {
                        return;
                    }

                    Assemblies.Add(info.Assembly, info);

                    if (!IsSystemAssembly(info.Assembly))
                    {
                        CustomAssemblies.Add(info.Assembly, info);
                    }
                }
                finally
                {
                    if (Interlocked.Decrement(ref scanning) == 0)
                    {
                        inherit.Reset(InheritEvaluator.Create);
                        Reset?.Invoke();
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        private static AssemblyScanInfo? ProcessAssembly(Assembly assembly)
        {
            try
            {
                DateTime start = DateTime.Now;
                System.Diagnostics.Debug.WriteLine($"Assembly '{assembly.FullName}' loading started at {start}.");

                Scan(assembly, out TypeSet result);

                DateTime end = DateTime.Now;
                System.Diagnostics.Debug.WriteLine($"Assembly '{assembly.FullName}' loading ended at {end} with elapsed time {end - start}.");

                DateTime? @static = null;

                try
                {
                    CallStaticInitializerAttribute<StaticInitializerRequiredAttribute>(assembly);

                    if (AssemblyLoadCallStaticConstructor)
                    {
                        CallStaticInitializerAttribute(assembly);
                    }

                    //TODO: optimize (only types with attribute)
                    EnumUtilities.Synchronization.Initialize(assembly);

                    @static = DateTime.Now;
                    System.Diagnostics.Debug.WriteLine($"Assembly '{assembly.FullName}' static constructors resolved at {@static} with elapsed time {@static - end}.");
                }
                catch (Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine($"Static Initializer Error for '{assembly.FullName}': {exception}.");
                }

                return (assembly, result, start, end - start, @static - end);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(ReflectionUtilities)} Scan Error: {exception}.");
                return null;
            }
        }

        /// <summary>
        /// Calls the static constructor of this type.
        /// </summary>
        /// <param name="type">The type of which to call the static constructor.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type CallStaticConstructor(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            CallStaticConstructor(type.TypeHandle);
            return type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RuntimeTypeHandle CallStaticConstructor(this RuntimeTypeHandle handle)
        {
            RuntimeHelpers.RunClassConstructor(handle);
            return handle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> CallStaticConstructor(this IEnumerable<Type> source)
        {
            return CallStaticConstructor(source, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> CallStaticConstructor(this IEnumerable<Type> source, Boolean lazy)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            static void Call(Type type)
            {
                CallStaticConstructor(type);
            }

            return source.ForEach(Call).MaterializeIfNot(lazy);
        }

        private static Assembly CallStaticInitializerAttribute<TAttribute>(this Assembly assembly) where TAttribute : StaticInitializerAttribute, new()
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            static IEnumerable<StaticInitializerAttribute> Handler(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                IEnumerable<StaticInitializerAttribute> attributes = type.GetCustomAttributes<TAttribute>();

                if (typeof(TAttribute) == typeof(StaticInitializerRequiredAttribute))
                {
                    attributes = type.GetCustomAttributes<StaticInitializerNetExtenderAttribute>().Concat(attributes);
                }

                foreach (StaticInitializerAttribute attribute in attributes)
                {
                    if (!attribute.Active || !attribute.Platform.IsOSPlatform())
                    {
                        continue;
                    }

                    if (attribute.Type is null)
                    {
                        yield return new TAttribute { Type = type, Priority = attribute.Priority, Active = attribute.Active, Platform = attribute.Platform };
                        continue;
                    }

                    yield return attribute;
                }
            }

            IEnumerable<Type> result = assembly.GetSafeTypesUnsafe()
                .SelectMany(Handler)
                .OrderByDescending(static attribute => attribute is StaticInitializerNetExtenderAttribute)
                .ThenByDescending(static attribute => attribute.Priority)
                .ThenBy(static attribute => attribute.Type?.FullName)
                .Select(static attribute => attribute.Type)
                .WhereNotNull()
                .Distinct();

            foreach (Type type in result)
            {
                type.CallStaticConstructor();
            }

            return assembly;
        }

        /// <summary>
        /// Calls the static constructor of types with <see cref="StaticInitializerAttribute"/> in assembly.
        /// </summary>
        /// <param name="assembly">The assembly of which to call the static constructor.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Assembly CallStaticInitializerAttribute(this Assembly assembly)
        {
            return CallStaticInitializerAttribute<StaticInitializerAttribute>(assembly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Assembly> CallStaticInitializerAttribute(this IEnumerable<Assembly> assemblies)
        {
            return CallStaticInitializerAttribute(assemblies, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Assembly> CallStaticInitializerAttribute(this IEnumerable<Assembly> assemblies, Boolean lazy)
        {
            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            static void Call(Assembly assembly)
            {
                CallStaticInitializerAttribute(assembly);
            }

            return assemblies.ForEach(Call).MaterializeIfNot(lazy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Assembly> CallStaticInitializerAttribute()
        {
            return CallStaticInitializerAttribute(CustomAssemblies.Keys);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<Assembly> CallStaticInitializerRequiredAttribute()
        {
            static void Call(Assembly assembly)
            {
                CallStaticInitializerAttribute<StaticInitializerRequiredAttribute>(assembly);
            }

            return CustomAssemblies.Keys.ForEach(Call).Materialize();
        }

        private sealed class CustomAssemblyComparer : IComparer<Assembly>
        {
            public static CustomAssemblyComparer Instance { get; } = new CustomAssemblyComparer();

            private Assembly Calling { get; }
            private Assembly Executing { get; }
            private Assembly? Entry { get; }

            private CustomAssemblyComparer()
                : this(CallingAssembly, EntryAssembly)
            {
            }

            public CustomAssemblyComparer(Assembly calling, Assembly? entry)
            {
                Calling = calling ?? throw new ArgumentNullException(nameof(calling));
                Executing = Assembly.GetExecutingAssembly();
                Entry = entry;
            }

            public Int32 Compare(Assembly? x, Assembly? y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }

                if (x is null)
                {
                    return 1;
                }

                if (y is null)
                {
                    return -1;
                }

                Boolean executing = x == Executing;
                if (executing != (y == Executing))
                {
                    return executing ? -1 : 1;
                }

                Boolean netextender = IsNetExtender(x);
                if (netextender != IsNetExtender(y))
                {
                    return netextender ? -1 : 1;
                }

                Boolean calling = x == Calling;
                if (calling != (y == Calling))
                {
                    return calling ? -1 : 1;
                }

                Boolean entry = x == Entry;
                if (entry != (y == Entry))
                {
                    return entry ? -1 : 1;
                }

                return AssemblyComparer.FullNameOrdinalIgnoreCase.Compare(x, y);
            }

            private static Boolean IsNetExtender(Assembly assembly)
            {
                return assembly.FullName?.StartsWith(nameof(NetExtender), StringComparison.Ordinal) is true;
            }
        }
    }
}