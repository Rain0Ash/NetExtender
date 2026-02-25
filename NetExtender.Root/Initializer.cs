// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Cecil;
using NetExtender.Newtonsoft;
using NetExtender.Serialization.Json;
using Newtonsoft.Json;

#if DEBUG
using System.Diagnostics;
using System.Threading;
using NetExtender.Utilities.Types;
#endif

[assembly: IgnoresAccessChecksTo("System.Private.CoreLib")]

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    internal sealed class IgnoresAccessChecksToAttribute : Attribute
    {
        public String AssemblyName { get; }

        public IgnoresAccessChecksToAttribute(String assemblyName)
        {
            AssemblyName = assemblyName;
        }
    }
}

namespace NetExtender.Initializer
{
    internal static class NetExtenderRootInitializer
    {
#if CECIL
        public static event EventHandler<TypeSet>? Assembly;
#else
        public static event EventHandler<TypeSet?>? Assembly;
#endif

#if DEBUG
        private static volatile Int32 count;

        static NetExtenderRootInitializer()
        {
            static String Count(TypeSet? set)
            {
                return set?.Count.ToString() ?? StringUtilities.NullString;
            }

            Assembly += static (sender, set) =>
            {
                Assembly? assembly = sender as Assembly;
                Debug.WriteLine($"Assembly {assembly?.GetName().Name} loaded. Types: '{Count(set)}'. Total assemblies: {Interlocked.Increment(ref count)}.");
            };
        }
#endif

#pragma warning disable CA2255
        [ModuleInitializer]
        public static void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyLoad += Assembly;

            void Assembly(Object? sender, AssemblyLoadEventArgs args)
            {
                LoadAssembly(args.LoadedAssembly);
            }

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                LoadAssembly(assembly);
            }

            JsonConvert.DefaultSettings = static () => DefaultJsonSerializerSettings.Settings;
            DefaultJsonSerializerOptions.Options = DefaultJsonSerializerOptions.Create();
        }
#pragma warning restore CA2255

        private static void LoadAssembly(Assembly? assembly)
        {
            if (assembly is null)
            {
                return;
            }

#if CECIL
            TypeSet set = MonoCecilType.From(assembly);
            Assembly?.Invoke(assembly, set);
#endif
            Assembly?.Invoke(assembly, null);
        }
    }
}