// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.ComponentModel;
using NetExtender.Configuration;
using NetExtender.Configuration.Interfaces.Property.Common;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Localizations.Interfaces;

namespace NetExtender.Localizations.Sub
{
    internal sealed class SubLocalizationPropertyAdapter : IStringLocalizationProperty
    {
        private ILocalization _config;
        private ConfigProperty<String> Property { get; }

        public String Path
        {
            get
            {
                return Property.Path;
            }
        }

        public IPropertyConfigBase Config { get; }

        public String Key
        {
            get
            {
                return Property.Key;
            }
        }

        public IImmutableList<String> Sections
        {
            get
            {
                return Property.Sections;
            }
        }

        public CryptAction Crypt
        {
            get
            {
                return Property.Crypt;
            }
        }

        public ICryptKey CryptKey
        {
            get
            {
                return Property.CryptKey;
            }
        }

        public Boolean Caching
        {
            get
            {
                return Property.Caching;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Property.IsReadOnly;
            }
        }

        public Boolean AlwaysDefault
        {
            get
            {
                return Property.AlwaysDefault;
            }
        }

        public Boolean DisableSave
        {
            get
            {
                return Property.DisableSave;
            }
        }

        public ConfigPropertyOptions Options
        {
            get
            {
                return Property.Options;
            }
        }

        public void Read()
        {
            Property.Read();
        }

        public Boolean KeyExist()
        {
            return Property.KeyExist();
        }

        public Boolean ThrowOnInvalid
        {
            get
            {
                return Property.ThrowOnInvalid;
            }
        }
        
        public Boolean ThrowOnReadOnly
        {
            get
            {
                return Property.ThrowOnReadOnly;
            }
        }

        public String DefaultValue
        {
            get
            {
                return Property.DefaultValue;
            }
        }

        public String Value
        {
            get
            {
                return Property.Value;
            }
        }

        public Boolean IsValid
        {
            get
            {
                return Property.IsValid;
            }
        }

        public Func<String, Boolean> Validate
        {
            get
            {
                return Property.Validate;
            }
        }

        public TryConverter<String, String> Converter
        {
            get
            {
                return Property.Converter;
            }
        }

        public SubLocalizationPropertyAdapter(IConfigProperty<String> property)
        {
            Config = property.Config;
        }

        public String GetValue()
        {
            return Property.GetValue();
        }

        public String GetValue(Func<String, Boolean> validate)
        {
            return Property.GetValue(validate);
        }
        
        public event PropertyChangedEventHandler? PropertyChanged
        {
            add
            {
                Property.PropertyChanged += value;
            }
            remove
            {
                Property.PropertyChanged -= value;
            }
        }

        public event PropertyChangingEventHandler? PropertyChanging
        {
            add
            {
                Property.PropertyChanging += value;
            }
            remove
            {
                Property.PropertyChanging -= value;
            }
        }

        public void RaisePropertyChanging(PropertyChangingEventArgs args)
        {
            ((ReactiveUI.IReactiveObject) Property).RaisePropertyChanging(args);
        }

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            ((ReactiveUI.IReactiveObject) Property).RaisePropertyChanged(args);
        }

        public void Dispose()
        {
            Property.Dispose();
        }
    }
}