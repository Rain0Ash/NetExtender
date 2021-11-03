// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Crypto;
using NetExtender.Utilities.Types;

namespace NetExtender.Crypto.CryptKey.Deterministic
{
    public class DeterministicStringEncryptorWrapper : IStringEncryptor
    {
        public static ConditionalWeakTable<IStringEncryptor, DeterministicStringEncryptorWrapper> Registrations { get; } =
            new ConditionalWeakTable<IStringEncryptor, DeterministicStringEncryptorWrapper>();

        public static DeterministicStringEncryptorWrapper Register<T>(T encryptor) where T : IStringEncryptor
        {
            if (encryptor is null)
            {
                throw new ArgumentNullException(nameof(encryptor));
            }

            if (encryptor is DeterministicStringEncryptorWrapper wrapper)
            {
                return Registrations.GetValue(wrapper.Encryptor, _ => wrapper);
            }

            return Registrations.GetValue(encryptor, _ => encryptor.CreateDeterministic());
        }
        
        private Dictionary<String, String?>? Dictionary { get; }

        private Maybe<String?> Null { get; set; }

        protected IStringEncryptor Encryptor { get; }

        public Int32 KeySize
        {
            get
            {
                return Encryptor.KeySize;
            }
        }

        public Boolean IsEncrypt
        {
            get
            {
                return Encryptor.IsEncrypt;
            }
        }

        public Boolean IsDeterministic
        {
            get
            {
                return true;
            }
        }
        
        public DeterministicStringEncryptorWrapper(IStringEncryptor encryptor)
            : this(encryptor, null)
        {
        }

        public DeterministicStringEncryptorWrapper(IStringEncryptor encryptor, IEnumerable<KeyValuePair<String, String?>>? source)
        {
            Encryptor = encryptor ?? throw new ArgumentNullException(nameof(encryptor));

            if (!encryptor.IsDeterministic)
            {
                Dictionary = source?.ToDictionary() ?? new Dictionary<String, String?>();
            }
        }

        private String? EncryptNullInternal()
        {
            if (Null.HasValue)
            {
                return Null.Value;
            }

            String? result = Encryptor.Encrypt((String?) null!);
            Null = result;
            return result;
        }
        
        private String? EncryptNullStringInternal()
        {
            if (Null.HasValue)
            {
                return Null.Value;
            }

            String? result = Encryptor.EncryptString((String?) null!);
            Null = result;
            return result;
        }

        public String? Encrypt(String value)
        {
            if (Dictionary is null || Encryptor.IsDeterministic)
            {
                return Encryptor.Encrypt(value);
            }

            return value is not null! ? Dictionary.GetOrAdd(value, Encryptor.Encrypt) : EncryptNullInternal();
        }

        public String? EncryptString(String value)
        {
            if (Dictionary is null || Encryptor.IsDeterministic)
            {
                return Encryptor.Encrypt(value);
            }
            
            return value is not null! ? Dictionary.GetOrAdd(value, Encryptor.EncryptString) : EncryptNullStringInternal();
        }

        public IEnumerable<String?> Encrypt(IEnumerable<String> source)
        {
            if (source is null! || Dictionary is null ||  Encryptor.IsDeterministic)
            {
                return Encryptor.Encrypt(source!);
            }

            return source.Select(Encrypt).ToArray();
        }

        public IEnumerable<String?> EncryptString(IEnumerable<String> source)
        {
            if (source is null! || Dictionary is null || Encryptor.IsDeterministic)
            {
                return Encryptor.Encrypt(source!);
            }

            return source.Select(EncryptString).ToArray();
        }
    }
    
    public class DeterministicStringEncryptorWrapper<T> : DeterministicStringEncryptorWrapper where T : IStringEncryptor
    {
        public new T Encryptor { get; }
        
        public DeterministicStringEncryptorWrapper(T encryptor)
            : base(encryptor)
        {
            Encryptor = encryptor ?? throw new ArgumentNullException(nameof(encryptor));
        }
        
        public DeterministicStringEncryptorWrapper(T encryptor, IEnumerable<KeyValuePair<String, String?>>? source)
            : base(encryptor, source)
        {
            Encryptor = encryptor ?? throw new ArgumentNullException(nameof(encryptor));
        }
    }
}