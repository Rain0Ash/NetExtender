// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Types.Attributes;

namespace NetExtender.Utilities.Types
{
    [StaticInitializerRequired]
    public static class ServiceDescriptorUtilities
    {
        static ServiceDescriptorUtilities()
        {
            ConvertUtilities.RegisterStringHandler<ServiceDescriptor>(GetString);
        }
        
        [SuppressMessage("ReSharper", "InvokeAsExtensionMethod")]
        private static String? GetString(ServiceDescriptor? value, EscapeType escape, IFormatProvider? provider)
        {
            return value is not null ? $"{value.Lifetime}({value.ServiceType}:[{value.ImplementationType}])" : ConvertUtilities.GetString((Object?) null, escape, provider);
        }
    }
}