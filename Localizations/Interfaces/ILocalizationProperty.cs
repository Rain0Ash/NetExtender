// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Localizations.Interfaces
{
    public interface ILocalizationProperty : ILocalizationProperty<IString>
    {
        
    }

    public interface IStringLocalizationProperty : ILocalizationProperty<String>
    {
    }
    
    public interface ILocalizationProperty<T> : IReadOnlyConfigProperty<T>, IDisposable
    {
        
    }
}