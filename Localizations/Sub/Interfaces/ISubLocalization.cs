// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Interfaces.Property.Common;
using NetExtender.Localizations.Interfaces;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Localizations.Sub.Interfaces
{
    public interface ISubLocalization : IConfig, IPropertyConfigBase
    {
        public IStringLocalizationProperty GetProperty(String key, params String[] sections);
        public IStringLocalizationProperty GetProperty(String key, IString value, params String[] sections);

        public void ReadProperties();
        public void SaveProperties();
        public void ResetProperties();
        public void ClearProperties();
    }
}