// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Json;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Trees;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Xml
{
    public class XmlConfigBehavior : JsonConfigBehavior
    {
        public XmlConfigBehavior()
            : this(ConfigOptions.None)
        {
        }
        
        public XmlConfigBehavior(ConfigOptions options)
            : this(null, null, options)
        {
        }
        
        public XmlConfigBehavior(ICryptKey? crypt)
            : this(crypt, ConfigOptions.None)
        {
        }
        
        public XmlConfigBehavior(ICryptKey? crypt, ConfigOptions options)
            : this(null, crypt, options)
        {
        }

        public XmlConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }

        public XmlConfigBehavior(String? path, ConfigOptions options)
            : this(path, null, options)
        {
        }
        
        public XmlConfigBehavior(String? path, ICryptKey? crypt)
            : this(path, crypt, ConfigOptions.None)
        {
        }
        
        public XmlConfigBehavior(String? path, ICryptKey? crypt, ConfigOptions options)
            : base(ValidatePathOrGetDefault(path, "xml"), crypt, options)
        {
        }

        [return: NotNullIfNotNull("key")]
        private static String? Convert(String? key)
        {
            return key?.Replace(' ', '_');
        }
        
        [return: NotNullIfNotNull("sections")]
        private static IEnumerable<String>? Convert(IEnumerable<String>? sections)
        {
            return sections?.Select(Convert).WhereNotNull();
        }

        public override String? Get(String? key, IEnumerable<String>? sections)
        {
            return base.Get(Convert(key), Convert(sections));
        }

        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            return base.Set(Convert(key), value, Convert(sections));
        }

        protected override String? SerializeConfig()
        {
            String? json = base.SerializeConfig();
            return json is not null ? XmlUtilities.ToXml(json, "Config") : json;
        }

        protected override DictionaryTree<String, String>? DeserializeConfig(String config)
        {
            if (String.IsNullOrWhiteSpace(config))
            {
                return new DictionaryTree<String, String>();
            }
            
            try
            {
                return base.DeserializeConfig(JsonUtilities.ToJson(config));
            }
            catch (Exception)
            {
                return new DictionaryTree<String, String>();
            }
        }
    }
}