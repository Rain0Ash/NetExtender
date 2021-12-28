// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Initializer
{
    public interface INetExtenderFrameworkInitializer
    {
        public Boolean IsInitialize { get; set; }
        public Boolean IsFullInitializeRequired { get; set; }
        public Boolean IsInitializeRequireAttribute { get; set; }
        public AssemblyHashInitialization AssemblyHashInitialization { get; set; }
    }
}