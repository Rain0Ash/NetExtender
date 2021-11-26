// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Localization.Behavior;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Localization.Interfaces;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Utilities
{
    public static class LocalizationUtilities
    {
        public static ILocalizationBehavior ToLocalization(this IConfigBehavior behavior, LocalizationOptions options)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new LocalizationBehavior(behavior, options);
        }

        public static ILocalizationConfig Create(this ILocalizationBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new LocalizationConfig(behavior);
        }
    }
}