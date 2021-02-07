// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using NetExtender.Configuration;
using NetExtender.Configuration.Interfaces.Property.Common;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Localizations.Interfaces;
using NetExtender.Localizations.Sub.Interfaces;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utils.Types;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NetExtender.Localizations
{
    public class LocalizationProperty : ReactiveObject, ILocalizationProperty
    {
        private static Boolean ConverterHandler(String value, out IString result)
        {
            if (value is null)
            {
                result = default;
                return false;
            }
            
            result = value.ToIString();
            return true;
        }
        
        private IReadOnlyDictionary<CultureInfo, ISubLocalization> Localizations { get; }
        private Dictionary<CultureInfo, IStringLocalizationProperty> _dictionary { get; }

        public String Path { get; }

        public ILocalization Config { get; }

        IPropertyConfigBase IReadOnlyConfigPropertyBase.Config
        {
            get
            {
                return Config;
            }
        }
        
        public String Key { get; }
        public String[] Sections { get; }

        public CryptAction Crypt
        {
            get
            {
                return CryptAction.None;
            }
        }

        public ICryptKey CryptKey
        {
            get
            {
                return null;
            }
        }

        public Boolean Caching
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.Caching);
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.ReadOnly);
            }
        }

        public Boolean AlwaysDefault
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.AlwaysDefault);
            }
        }

        public Boolean DisableSave
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.DisableSave);
            }
        }

        public ConfigPropertyOptions Options
        {
            get
            {
                return ConfigPropertyOptions.ReadOnly | ConfigPropertyOptions.DisableSave | ConfigPropertyOptions.Caching;
            }
        }

        public Boolean ThrowOnInvalid
        {
            get
            {
                return false;
            }
        }

        public IString DefaultValue { get; }

        [Reactive]
        public IString Value { get; private set; }
        
        public Boolean IsValid { get; private set; }

        public Func<IString, Boolean> Validate
        {
            get
            {
                return StringUtils.IsNotNullOrEmpty;
            }
        }

        public TryConverter<String, IString> Converter
        {
            get
            {
                return ConverterHandler;
            }
        }
        
        private protected IStringLocalizationProperty Current { get; private set; }

        internal LocalizationProperty(ILocalization config, IReadOnlyDictionary<CultureInfo, ISubLocalization> sublocale, String key, IString value, params String[] sections)
        {
            Config = config;
            Localizations = sublocale;
            Key = key;
            DefaultValue = value.IsNotNullOrEmpty() ? value : StringUtils.IStringEmpty;
            Sections = sections;
            
            Config.LanguageCultureChanged += OnChanged;
        }
        
        private void OnChanged([NotNull] CultureInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (_dictionary.TryGetValue(info, out IStringLocalizationProperty property))
            {
                Current = property;
                return;
            }

            if (Localizations.TryGetValue(info, out ISubLocalization config))
            {
                if (config is null)
                {
                    Current = null;
                    return;
                }

                Current = config.GetProperty(Key, Sections);
                return;
            }
            
            Current = null;
        }
        
        public void Read()
        {
            if (Current is null)
            {
                Value = DefaultValue;
                return;
            }
            
            IString value = GetValue();
            Value = Validate(value) ? value : DefaultValue;
        }

        public Boolean KeyExist()
        {
            return Current?.KeyExist() ?? false;
        }
        
        public IString GetValue()
        {
            return GetValue(Validate);
        }

        public IString GetValue(Func<IString, Boolean> validate)
        {
            return Current?.GetValue().ToIString() ?? DefaultValue;
        }

        public void Dispose()
        {
            Config.LanguageCultureChanged -= OnChanged;
        }
    }
}