// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Patch;

namespace NetExtender.Initializer
{
    /// <summary>
    /// Use for initialize <see cref="NetExtender"/> Framework.
    /// </summary>
    /// <remarks>
    /// <para>
    /// MUST BE USED IN <see cref="System.Runtime.CompilerServices.ModuleInitializerAttribute"/> METHOD!
    /// </para>
    /// <para>
    /// <see cref="System.Runtime.CompilerServices.ModuleInitializerAttribute"/> METHOD CAN'T USE <see cref="NetExtender"/> EXCEPT <see cref="NetExtenderFrameworkInitializer"/>
    /// </para>
    /// </remarks>
    public static partial class NetExtenderFrameworkInitializer
    {
        private static INetExtenderFrameworkInitializer Initializer { get; } = new NetExtenderFrameworkInitializerInfo();

        public static Boolean? Successful { get; internal set; }

        internal const String InitializerMethod = nameof(Initialize) + nameof(NetExtender);

        public static Boolean IsInitialize
        {
            get
            {
                return Initializer.IsInitialize;
            }
            set
            {
                Initializer.IsInitialize = value;
            }
        }

        public static Boolean IsFullInitializeRequired
        {
            get
            {
                return Initializer.IsFullInitializeRequired;
            }
            set
            {
                Initializer.IsFullInitializeRequired = value;
            }
        }

        public static Boolean IsInitializeRequireAttribute
        {
            get
            {
                return Initializer.IsInitializeRequireAttribute;
            }
            set
            {
                Initializer.IsInitializeRequireAttribute = value;
            }
        }

        public static AssemblySignInitialization AssemblySignInitialization
        {
            get
            {
                return Initializer.AssemblySignInitialization;
            }
        }

        public static IDictionary<String, AssemblyVerifyInfo?> Assemblies
        {
            get
            {
                return Initializer.Assemblies;
            }
        }

        public static IDictionary<String, AssemblyVerifyInfo?> Include
        {
            get
            {
                return Initializer.Include;
            }
        }

        public static ISet<String> Exclude
        {
            get
            {
                return Initializer.Exclude;
            }
        }
        
        public static ReflectionPatchCategory IncludePatchCategory
        {
            get
            {
                return Initializer.IncludePatchCategory;
            }
            set
            {
                Initializer.IncludePatchCategory = value;
            }
        }

        public static ISet<String> IncludePatch
        {
            get
            {
                return Initializer.IncludePatch;
            }
        }

        public static ISet<String> ExcludePatch
        {
            get
            {
                return Initializer.ExcludePatch;
            }
        }

        internal static Boolean IsReady { get; private set; }

        internal static Boolean IsFramework(String? name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            name = Path.GetFileName(name);
            return name.StartsWith("NetExtender.") && name.EndsWith(".dll");
        }

        internal static Boolean LoadFramework(Assembly core, out Exception? exception)
        {
            if (core is null)
            {
                throw new ArgumentNullException(nameof(core));
            }

            String? directory;
            try
            {
                directory = Path.GetDirectoryName(core.Location);
            }
            catch (Exception inner)
            {
                exception = inner;
                return false;
            }

            if (String.IsNullOrEmpty(directory))
            {
                exception = default;
                return false;
            }

            List<Exception> exceptions = new List<Exception>();

            foreach ((String? key, AssemblyVerifyInfo? value) in Include)
            {
                Assemblies.TryAdd(key, value);
            }

            foreach (String assembly in Directory.GetFiles(directory, "*", SearchOption.AllDirectories).Where(IsFramework).Concat(Include.Keys).Except(Exclude).Distinct(StringComparer.OrdinalIgnoreCase))
            {
                try
                {
                    LoadAssembly(assembly, AssemblySignInitialization);
                }
                catch (Exception inner)
                {
                    exceptions.Add(inner);
                }
            }

            exception = exceptions.Count switch
            {
                0 => null,
                1 => exceptions[0],
                _ => new AggregateException(exceptions)
            };

            return true;
        }

        private static Object? InvokeNetExtenderInitializerMethod()
        {
            Type? type = NetExtender.Initializer.Initializer.ReflectionUtilities.GetEntryAssembly(true)?.EntryPoint?.DeclaringType;

            if (type is null)
            {
                return null;
            }

            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo? entry = type.GetMethod(InitializerMethod, binding, new[] { typeof(INetExtenderFrameworkInitializer) });

            if (entry is null || entry.IsAbstract || entry.IsGenericMethod)
            {
                return null;
            }

            if (entry.IsStatic)
            {
                return entry.Invoke(null, new Object[] { Initializer });
            }

            if (type.IsAbstract || type.IsInterface || type.IsGenericType)
            {
                return null;
            }

            Object? instance = Activator.CreateInstance(type, true);
            return instance is not null ? entry.Invoke(instance, new Object[] { Initializer }) : null;
        }

#pragma warning disable CA2255
        [ModuleInitializer]
        internal static void Initialize()
        {
            try
            {
                InvokeNetExtenderInitializerMethod();
                IsReady = true;
            }
            catch (Exception)
            {
                //ignore
            }
        }
#pragma warning restore CA2255
    }
}