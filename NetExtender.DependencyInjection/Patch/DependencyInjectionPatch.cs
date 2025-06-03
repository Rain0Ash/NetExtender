// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Reflection;
using NetExtender.Types.Reflection;

namespace NetExtender.Patch
{
    public partial class DependencyInjectionPatch : AutoReflectionPatch<DependencyInjectionPatch>
    {
        protected static Func<Patch> Factory { get; set; }

        public static Boolean IgnoreSystem { get; set; } = true;
        public static ConcurrentHashSet<Type> IncludeType { get; } = new ConcurrentHashSet<Type>();
        public static ConcurrentHashSet<Assembly> Include { get; } = new ConcurrentHashSet<Assembly>();
        public static ConcurrentHashSet<Type> ExcludeType { get; } = new ConcurrentHashSet<Type>();
        public static ConcurrentHashSet<Assembly> Exclude { get; } = new ConcurrentHashSet<Assembly>();

        static DependencyInjectionPatch()
        {
            Factory = static () => new Patch();
        }

        protected sealed override Patch Create()
        {
            return Factory();
        }
    }
}