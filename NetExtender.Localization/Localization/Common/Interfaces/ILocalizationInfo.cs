// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Comparers;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Common.Interfaces
{
    public interface ILocalizationInfo
    {
        public LocalizationIdentifier Default { get; }
        public LocalizationIdentifier System { get; }
        public LocalizationIdentifier Localization { get; set; }
        public LocalizationIdentifierBehaviorComparer Comparer { get; }
        public LocalizationOptions LocalizationOptions { get; }
        public Boolean ThreeLetterName { get; }
        public Boolean WithoutSystem { get; }
    }
}