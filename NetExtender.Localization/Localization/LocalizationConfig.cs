// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Localization.Interfaces;

namespace NetExtender.Localization
{
    public class LocalizationConfig : Config, ILocalizationConfig
    {
        private ILocalizationBehavior Behavior { get; }

        public LocalizationConfig(ILocalizationBehavior behavior)
            : base(behavior)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }
    }
}