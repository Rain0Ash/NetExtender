// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetExtender.NewtonSoft.Utilities
{
    public static class NewtonsoftUtilities
    {
        public static NamingStrategy? GetNamingStrategy(this JsonSerializer serializer)
        {
            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            return TryGetNamingStrategy(serializer, out NamingStrategy? result) ? result : default;
        }

        public static Boolean TryGetNamingStrategy(this JsonSerializer serializer, [MaybeNullWhen(false)] out NamingStrategy result)
        {
            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (serializer.ContractResolver is not DefaultContractResolver { NamingStrategy: NamingStrategy strategy })
            {
                result = default;
                return false;
            }

            result = strategy;
            return true;
        }
        
        [return: NotNullIfNotNull("property")]
        public static String? NamingStrategy(this JsonSerializer serializer, String? property, Boolean hasSpecifiedName)
        {
            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (property is null)
            {
                return null;
            }

            return TryGetNamingStrategy(serializer, out NamingStrategy? strategy) ? NamingStrategy(strategy, property, hasSpecifiedName) : property;
        }
        
        [return: NotNullIfNotNull("property")]
        public static String? NamingStrategy(this NamingStrategy? strategy, String? property, Boolean hasSpecifiedName)
        {
            return property is not null ? strategy?.GetPropertyName(property, hasSpecifiedName) ?? property : null;
        }
    }
}