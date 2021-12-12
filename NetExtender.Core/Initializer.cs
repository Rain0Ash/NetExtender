// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;
using NetExtender.Utilities.Core;

namespace NetExtender
{
    internal static class Initializer
    {
#pragma warning disable CA2255
        [ModuleInitializer]
        public static void Initialize()
        {
            ReflectionUtilities.CallStaticInitializerRequiredAttribute();
        }
#pragma warning restore CA2255
    }
}