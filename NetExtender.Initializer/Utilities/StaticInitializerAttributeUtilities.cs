// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Types.Attributes;

namespace NetExtender.Utilities
{
    public static class StaticInitializerAttributeUtilities
    {
        private static SortedDictionary<StaticInitializerAttributePlatform, OSPlatform> Platforms { get; } = new SortedDictionary<StaticInitializerAttributePlatform, OSPlatform>
        {
            [StaticInitializerAttributePlatform.Windows] = OSPlatform.Windows,
            [StaticInitializerAttributePlatform.Linux] = OSPlatform.Linux,
            [StaticInitializerAttributePlatform.FreeBSD] = OSPlatform.FreeBSD,
            [StaticInitializerAttributePlatform.OSX] = OSPlatform.OSX
        };
        
        public static Boolean RegisterOSPlatform(UInt64 value, OSPlatform platform)
        {
            return RegisterOSPlatform((StaticInitializerAttributePlatform) value, platform);
        }

        public static Boolean RegisterOSPlatform(this StaticInitializerAttributePlatform value, OSPlatform platform)
        {
            return value > 0 && (value & (value - 1)) == 0 && Platforms.TryAdd(value, platform);
        }

        public static Boolean IsOSPlatform(this StaticInitializerAttributePlatform value)
        {
            return IsOSPlatform(value, out _);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsOSPlatform(this StaticInitializerAttributePlatform value, out OSPlatform platform)
        {
            if (value == StaticInitializerAttributePlatform.None)
            {
                platform = default;
                return false;
            }

            Boolean IsPlatform(KeyValuePair<StaticInitializerAttributePlatform, OSPlatform> item)
            {
                return value.HasFlag(item.Key) && RuntimeInformation.IsOSPlatform(item.Value);
            }

            platform = Platforms.FirstOrDefault(IsPlatform).Value;
            return !platform.Equals(default);
        }

        public static Boolean IsOSPlatform(this StaticInitializerAttribute attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return attribute.Platform == StaticInitializerAttributePlatform.All || attribute.Platform.IsOSPlatform();
        }
    }
}