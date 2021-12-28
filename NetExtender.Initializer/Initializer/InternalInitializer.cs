// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Initializer
{
    public static partial class NetExtenderFrameworkInitializer
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private class InternalInitializer : INetExtenderFrameworkInitializer
        {
            public Boolean IsInitialize { get; set; }
            public Boolean IsFullInitializeRequired { get; set; }
            public Boolean IsInitializeRequireAttribute { get; set; } = true;
            public AssemblyHashInitialization AssemblyHashInitialization { get; set; }
        }
    }
}