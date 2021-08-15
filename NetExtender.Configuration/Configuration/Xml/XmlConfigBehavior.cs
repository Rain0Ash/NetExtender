// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Json;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Types.Trees;

namespace NetExtender.Configuration.Xml
{
    public class XmlConfigBehavior : JsonConfigBehavior
    {
        public XmlConfigBehavior(String path = null, ConfigOptions options = ConfigOptions.None)
            : this(path, null, options)
        {
        }
        
        public XmlConfigBehavior(String path, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : base(ValidatePathOrGetDefault(path, "xml"), crypt, options)
        {
        }

        private static String Convert(String key)
        {
            return key.Replace(' ', '_');
        }
        
        private static IEnumerable<String> Convert(IEnumerable<String> sections)
        {
            return sections.Select(Convert);
        }

        public override String Get(String key, IEnumerable<String> sections)
        {
            return base.Get(Convert(key), Convert(sections));
        }

        public override Boolean Set(String key, String value, IEnumerable<String> sections)
        {
            return base.Set(Convert(key), value, Convert(sections));
        }

        protected override String SerializeConfig()
        {
            return XmlUtilities.ToXml(base.SerializeConfig(), "Config");
        }

        protected override DictionaryTree<String, String> DeserializeConfig(String config)
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