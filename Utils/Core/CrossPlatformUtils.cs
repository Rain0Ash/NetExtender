// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Workstation;

namespace NetExtender.Utils.Core
{
    public class ExternFunctionBuilder<T> where T : Delegate
    {
        private Dictionary<OSData, T> Builder { get; } = new Dictionary<OSData, T>();

        public ExternFunctionBuilder<T> With(T function, OSData type)
        {
            Builder.Add(type, function);
            return this;
        }

        public T Build()
        {
            return Builder.TryGetValue(Software.OperatingSystem, out T function) ? function : default;
        }
    }

    public static class CrossPlatformUtils
    {
    }
}