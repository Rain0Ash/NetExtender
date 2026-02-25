// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;
using NetExtender.Types.Strings;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.IO;

[assembly: IgnoresAccessChecksTo("System.Private.CoreLib")]

namespace NetExtender.Initializer
{
    internal static class NetExtenderInitializer
    {
#pragma warning disable CA2255
        [ModuleInitializer]
        public static void Initialize()
        {
            Assembly NetExtender = Assembly.GetExecutingAssembly();

            if (!NetExtenderFrameworkInitializer.IsReady)
            {
                throw new ModuleNotInitializedException(NetExtender.ManifestModule, $"You can't use {nameof(NetExtender)} in {NetExtenderFrameworkInitializer.InitializerMethod} method!");
            }

            if (NetExtenderFrameworkInitializer.IsInitialize)
            {
                if (!NetExtenderFrameworkInitializer.LoadFramework(NetExtender, out Exception? exception))
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

            IStringProvider.Default = new StringProvider();
            DirectoryUtilities.SetCurrentDirectoryHandler(static () => ApplicationUtilities.Directory);

            NetExtenderFrameworkInitializer.Successful = true;
        }
#pragma warning restore CA2255
    }
}