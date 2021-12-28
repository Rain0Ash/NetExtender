// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Core;

namespace NetExtender.Initializer
{
    internal static class Initializer
    {
#pragma warning disable CA2255
        [ModuleInitializer]
        public static void Initialize()
        {
            if (NetExtenderFrameworkInitializer.IsInitialize)
            {
                if (!NetExtenderFrameworkInitializer.LoadFramework(Assembly.GetExecutingAssembly(), out Exception? exception))
                {
                    NetExtenderFrameworkInitializer.Successful = false;
                }

                if (exception is not null && NetExtenderFrameworkInitializer.IsFullInitializeRequired)
                {
                    throw exception;
                }
            }

            if (NetExtenderFrameworkInitializer.IsInitializeRequireAttribute)
            {
                ReflectionUtilities.CallStaticInitializerRequiredAttribute();
            }

            NetExtenderFrameworkInitializer.Successful = true;
        }
#pragma warning restore CA2255
    }
}