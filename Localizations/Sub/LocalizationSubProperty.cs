// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using JetBrains.Annotations;
using NetExtender.Configuration;
using NetExtender.Converters;
using NetExtender.Localizations.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Localizations.Sub
{
    internal sealed class LocalizationSubProperty : ConfigPropertyBase, IStringLocalizationProperty
    {
        private static Boolean ConverterHandler(String value, out String result)
        {
            if (value is null)
            {
                result = default;
                return false;
            }
            
            result = value;
            return true;
        }
        
        public new ILocalization Config { get; }

        public String DefaultValue
        {
            get
            {
                return null;
            }
        }

        public String Value
        {
            get
            {
                return GetValue();
            }
        }

        public Boolean ThrowOnInvalid
        {
            get
            {
                return true;
            }
        }
        
        public Boolean ThrowOnReadOnly
        {
            get
            {
                return true;
            }
        }

        public Boolean IsValid
        {
            get
            {
                return Validate(GetValue());
            }
        }
        
        public Func<String, Boolean> Validate
        {
            get
            {
                return StringUtils.IsNotNullOrEmpty;
            }
        }

        public TryConverter<String, String> Converter
        {
            get
            {
                return ConverterHandler;
            }
        }

        internal LocalizationSubProperty(ILocalization config, String key, [NotNull] CultureInfo info, params String[] sections)
            : base(config, info?.TwoLetterISOLanguageName ?? throw new ArgumentNullException(nameof(key)), null,
                ConfigPropertyOptions.ReadOnly | ConfigPropertyOptions.DisableSave, key.ParamsAppend(sections))
        {
            Config = config;
        }

        public override void Read()
        {
        }

        public override Boolean KeyExist()
        {
            return Config.KeyExist(this);
        }

        public override void Save()
        {
            throw new InvalidOperationException();
        }

        public override void Reset()
        {
        }

        public String GetValue()
        {
            return GetValue(Validate);
        }

        public String GetValue(Func<String, Boolean> validate)
        {
            String value = Config.GetValue(this);
            return validate(value) ? value : null;
        }

        public override String ToString()
        {
            return Value;
        }
    }
}