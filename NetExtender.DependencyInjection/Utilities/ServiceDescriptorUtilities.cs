using System;
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
        
        private static String? GetString(ServiceDescriptor? value, EscapeType escape, IFormatProvider? provider)
        {
            // ReSharper disable once InvokeAsExtensionMethod
            return value is not null ? $"{value.Lifetime}({value.ServiceType}:[{value.ImplementationType}])" : ConvertUtilities.GetString((Object?) null, escape, provider);
        }
    }
}