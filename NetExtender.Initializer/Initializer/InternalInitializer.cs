// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Patch;

namespace NetExtender.Initializer
{
    public static partial class NetExtenderFrameworkInitializer
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private class NetExtenderFrameworkInitializerInfo : INetExtenderFrameworkInitializer
        {
            public Boolean IsInitialize { get; set; }
            public Boolean IsFullInitializeRequired { get; set; }
            public Boolean IsInitializeRequireAttribute { get; set; } = true;
            public AssemblySignInitialization AssemblySignInitialization { get; set; } = AssemblySignInitialization.Sign;
            public IDictionary<String, AssemblyVerifyInfo?> Assemblies { get; } = new Dictionary<String, AssemblyVerifyInfo?>();
            public IDictionary<String, AssemblyVerifyInfo?> Include { get; } = new Dictionary<String, AssemblyVerifyInfo?>();
            public ISet<String> Exclude { get; } = new HashSet<String>();
            public ReflectionPatchCategory IncludePatchCategory { get; set; } = ReflectionPatchCategory.Capability;
            public ISet<String> IncludePatch { get; } = new HashSet<String>();
            public ISet<String> ExcludePatch { get; } = new HashSet<String>();
        }
    }
}