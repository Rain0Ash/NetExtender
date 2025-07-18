// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Cryptography.Common;
using NetExtender.Configuration.Cryptography.Common.Interfaces;
using NetExtender.Configuration.Cryptography.Interfaces;
using NetExtender.Configuration.Cryptography.Properties.Interfaces;
using NetExtender.Configuration.Cryptography.Utilities;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Properties;
using NetExtender.Cryptography.Keys.Interfaces;
using NetExtender.Types.Converters.Interfaces;
using NetExtender.Utilities.Cryptography;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Cryptography.Properties
{
    public sealed class ReadOnlyCryptographyConfigProperty<T> : ReadOnlyConfigProperty<T>, IReadOnlyCryptographyConfigProperty<T>
    {
        private new IReadOnlyCryptographyConfigProperty Internal { get; }

        public CryptAction Crypt
        {
            get
            {
                return Internal.Crypt;
            }
        }

        public IConfigurationCryptor Cryptor
        {
            get
            {
                return Internal.Cryptor;
            }
        }

        public CryptographyConfigOptions CryptographyOptions
        {
            get
            {
                return Internal.CryptographyOptions;
            }
        }

        public Boolean IsCryptDefault
        {
            get
            {
                return Internal.IsCryptDefault;
            }
        }

        public Boolean IsCryptKey
        {
            get
            {
                return Internal.IsCryptKey;
            }
        }

        public Boolean IsCryptValue
        {
            get
            {
                return Internal.IsCryptValue;
            }
        }

        public Boolean IsCryptSections
        {
            get
            {
                return Internal.IsCryptSections;
            }
        }

        public Boolean IsCryptConfig
        {
            get
            {
                return Internal.IsCryptConfig;
            }
        }

        public Boolean IsCryptAll
        {
            get
            {
                return Internal.IsCryptAll;
            }
        }

        internal ReadOnlyCryptographyConfigProperty(IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new ReadOnlyCryptographyConfigPropertyWrapper(config, key, null, cryptor, options, sections), alternate, validate, converter)
        {
        }

        internal ReadOnlyCryptographyConfigProperty(IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate, IOneWayConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new ReadOnlyCryptographyConfigPropertyWrapper(config, key, null, cryptor, options, sections), alternate, validate, converter)
        {
        }

        internal ReadOnlyCryptographyConfigProperty(IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new ReadOnlyCryptographyConfigProperty(config, key, null, cryptor, options, sections), alternate, validate, converter)
        {
        }

        internal ReadOnlyCryptographyConfigProperty(IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, Func<T, Boolean>? validate, IOneWayConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new ReadOnlyCryptographyConfigProperty(config, key, null, cryptor, options, sections), alternate, validate, converter)
        {
        }

        internal ReadOnlyCryptographyConfigProperty(IReadOnlyCryptographyConfigProperty property, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter)
            : base(property, alternate, validate, converter)
        {
            Internal = property ?? throw new ArgumentNullException(nameof(property));
        }

        internal ReadOnlyCryptographyConfigProperty(IReadOnlyCryptographyConfigProperty property, T alternate, Func<T, Boolean>? validate, IOneWayConverter<String?, T>? converter)
            : base(property, alternate, validate, converter)
        {
            Internal = property ?? throw new ArgumentNullException(nameof(property));
        }
    }

    public class ReadOnlyCryptographyConfigProperty : ReadOnlyConfigProperty, IReadOnlyCryptographyConfigProperty
    {
        protected IReadOnlyCryptographyConfig Cryptography { get; }

        public CryptAction Crypt
        {
            get
            {
                return Cryptor.Crypt;
            }
        }

        protected IConfigurationCryptor? InternalCryptor { get; }

        public IConfigurationCryptor Cryptor
        {
            get
            {
                return InternalCryptor ?? Cryptography.Cryptor;
            }
        }

        public CryptographyConfigOptions CryptographyOptions
        {
            get
            {
                return Cryptography.CryptographyOptions;
            }
        }

        public Boolean IsCryptDefault
        {
            get
            {
                return Cryptography.IsCryptDefault;
            }
        }

        public Boolean IsCryptKey
        {
            get
            {
                return Cryptography.IsCryptKey;
            }
        }

        public Boolean IsCryptValue
        {
            get
            {
                return Cryptography.IsCryptValue;
            }
        }

        public Boolean IsCryptSections
        {
            get
            {
                return Cryptography.IsCryptSections;
            }
        }

        public Boolean IsCryptConfig
        {
            get
            {
                return Cryptography.IsCryptConfig;
            }
        }

        public Boolean IsCryptAll
        {
            get
            {
                return Cryptography.IsCryptAll;
            }
        }

        protected internal ReadOnlyCryptographyConfigProperty(IReadOnlyCryptographyConfig config, String? key, String? alternate, IStringCryptor? cryptor, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(config, key, alternate, options, sections)
        {
            Cryptography = config ?? throw new ArgumentNullException(nameof(config));
            InternalCryptor = cryptor?.AsCryptor(Cryptography.CryptographyOptions);
            
            if (IsInitialize)
            {
                Internal.TryInitialize();
            }
        }

        protected override Boolean KeyExistCore()
        {
            return Cryptography.KeyExist(Key, Cryptor, Sections);
        }

        protected override Task<Boolean> KeyExistCoreAsync(CancellationToken token)
        {
            return Cryptography.KeyExistAsync(Key, Cryptor, Sections, token);
        }

        protected override String? GetValueCore()
        {
            return Cryptography.GetValue(Key, Alternate, Cryptor, Sections);
        }

        protected override Task<String?> GetValueCoreAsync(CancellationToken token)
        {
            return Cryptography.GetValueAsync(Key, Alternate, Cryptor, Sections, token);
        }
    }

    public class ReadOnlyCryptographyConfigPropertyWrapper : ReadOnlyConfigProperty, IReadOnlyCryptographyConfigProperty
    {
        public CryptAction Crypt
        {
            get
            {
                return Cryptor.Crypt;
            }
        }

        public IConfigurationCryptor Cryptor { get; }

        public CryptographyConfigOptions CryptographyOptions
        {
            get
            {
                return Cryptor.CryptographyOptions;
            }
        }

        public Boolean IsCryptDefault
        {
            get
            {
                return Cryptor.IsCryptDefault;
            }
        }

        public Boolean IsCryptKey
        {
            get
            {
                return Cryptor.IsCryptKey;
            }
        }

        public Boolean IsCryptValue
        {
            get
            {
                return Cryptor.IsCryptValue;
            }
        }

        public Boolean IsCryptSections
        {
            get
            {
                return Cryptor.IsCryptSections;
            }
        }

        public Boolean IsCryptConfig
        {
            get
            {
                return Cryptor.IsCryptConfig;
            }
        }

        public Boolean IsCryptAll
        {
            get
            {
                return Cryptor.IsCryptAll;
            }
        }

        protected internal ReadOnlyCryptographyConfigPropertyWrapper(IReadOnlyConfig config, String? key, String? alternate, IStringCryptor cryptor, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(config, key, alternate, options, sections)
        {
            if (cryptor is null)
            {
                throw new ArgumentNullException(nameof(cryptor));
            }

            Cryptor = cryptor.AsCryptor();
            
            if (IsInitialize)
            {
                Internal.TryInitialize();
            }
        }

        protected virtual Boolean TryEncryptKey(String? key, IStringEncryptor? encryptor, out String? result)
        {
            if (encryptor is null && !IsCryptDefault)
            {
                result = key;
                return true;
            }

            encryptor ??= Cryptor;
            return encryptor.TryEncrypt(key, out result);
        }

        protected virtual Boolean TryDecryptValue(String? value, IStringDecryptor? decryptor, out String? result)
        {
            if (decryptor is null && !IsCryptDefault)
            {
                result = value;
                return true;
            }

            decryptor ??= Cryptor;
            return decryptor.TryDecrypt(value, out result);
        }

        protected virtual Boolean TryEncryptSections(IEnumerable<String>? sections, IStringEncryptor? encryptor, out IEnumerable<String>? result)
        {
            if (encryptor is null && !IsCryptDefault)
            {
                result = sections;
                return true;
            }

            encryptor ??= Cryptor;
            return encryptor.TryEncrypt(sections, out result);
        }

        protected override Boolean KeyExistCore()
        {
            String? key = Key;
            if (IsCryptKey && !TryEncryptKey(Key, Cryptor, out key))
            {
                throw new CryptographicException();
            }

            IEnumerable<String>? sections = Sections;
            if (IsCryptSections && !TryEncryptSections(Sections, Cryptor, out sections))
            {
                throw new CryptographicException();
            }

            return Config.KeyExist(key, sections);
        }

        protected override Task<Boolean> KeyExistCoreAsync(CancellationToken token)
        {
            String? key = Key;
            if (IsCryptKey && !TryEncryptKey(Key, Cryptor, out key))
            {
                throw new CryptographicException();
            }

            IEnumerable<String>? sections = Sections;
            if (IsCryptSections && !TryEncryptSections(Sections, Cryptor, out sections))
            {
                throw new CryptographicException();
            }

            return Config.KeyExistAsync(key, sections, token);
        }

        protected override String? GetValueCore()
        {
            String? key = Key;
            if (IsCryptKey && !TryEncryptKey(Key, Cryptor, out key))
            {
                throw new CryptographicException();
            }

            IEnumerable<String>? sections = Sections;
            if (IsCryptSections && !TryEncryptSections(Sections, Cryptor, out sections))
            {
                throw new CryptographicException();
            }

            String? value = Config.GetValue(key, sections);

            if (value is not null && IsCryptValue)
            {
                return TryDecryptValue(value, Cryptor, out String? result) ? result : value;
            }

            return value;
        }

        protected override async Task<String?> GetValueCoreAsync(CancellationToken token)
        {
            String? key = Key;
            if (IsCryptKey && !TryEncryptKey(Key, Cryptor, out key))
            {
                throw new CryptographicException();
            }

            IEnumerable<String>? sections = Sections;
            if (IsCryptSections && !TryEncryptSections(Sections, Cryptor, out sections))
            {
                throw new CryptographicException();
            }

            String? value = await Config.GetValueAsync(key, sections, token).ConfigureAwait(false);

            if (value is not null && IsCryptValue)
            {
                return TryDecryptValue(value, Cryptor, out String? result) ? result : value;
            }

            return value;
        }
    }
}