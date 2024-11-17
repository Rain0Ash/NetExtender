// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Patch;

namespace NetExtender.Initializer
{
    public interface INetExtenderFrameworkInitializer
    {
        public Boolean IsInitialize { get; set; }
        public Boolean IsFullInitializeRequired { get; set; }
        public Boolean IsInitializeRequireAttribute { get; set; }
        public AssemblySignInitialization AssemblySignInitialization { get; set; }
        public IDictionary<String, AssemblyVerifyInfo?> Assemblies { get; }
        public IDictionary<String, AssemblyVerifyInfo?> Include { get; }
        public ISet<String> Exclude { get; }
        public ReflectionPatchCategory IncludePatchCategory { get; set; }
        public ISet<String> IncludePatch { get; }
        public ISet<String> ExcludePatch { get; }
    }
}